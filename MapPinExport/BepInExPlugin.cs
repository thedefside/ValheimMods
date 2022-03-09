﻿using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MapPinExport
{
    [BepInPlugin("aedenthorn.MapPinExport", "Map Pin Export", "0.2.1")]
    public class BepInExPlugin: BaseUnityPlugin
    {
        public static ConfigEntry<bool> isDebug;
        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<int> nexusID;

        private static BepInExPlugin context;

        public static void Dbgl(string str = "", bool pref = true)
        {
            if (isDebug.Value)
                Debug.Log((pref ? typeof(BepInExPlugin).Namespace + " " : "") + str);
        }
        private void Awake()
        {
            context = this;
            
            modEnabled = Config.Bind<bool>("General", "Enabled", true, "Enable this mod");
            isDebug = Config.Bind<bool>("General", "IsDebug", true, "Enable debug logs");
            nexusID = Config.Bind<int>("General", "NexusID", 1596, "Nexus mod ID for updates");

            if (!modEnabled.Value)
                return;

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);
        }

        [HarmonyPatch(typeof(Terminal), "InputText")]
        static class InputText_Patch
        {
            static bool Prefix(Terminal __instance)
            {
                if (!modEnabled.Value)
                    return true;
                string text = __instance.m_input.text;
                if (Minimap.instance && text.ToLower().StartsWith($"{typeof(BepInExPlugin).Namespace.ToLower()} export"))
                {
                    string file = text.Length > $"{typeof(BepInExPlugin).Namespace.ToLower()} export ".Length ? text.Substring($"{typeof(BepInExPlugin).Namespace.ToLower()} export ".Length) + ".txt" : "pindump.txt";

                    var pinList = new List<Minimap.PinData>((List<Minimap.PinData>)AccessTools.DeclaredField(typeof(Minimap), "m_pins").GetValue(Minimap.instance));
                    if (text.StartsWith($"{typeof(BepInExPlugin).Namespace.ToLower()} export ") && int.TryParse(text.Split(' ')[2], out int radius))
                    {
                        pinList = pinList.Where(p => Vector3.Distance(Player.m_localPlayer.transform.position, p.m_pos) <= radius).ToList();
                    }

                    pinList.Sort(delegate (Minimap.PinData x, Minimap.PinData y)
                    {
                        return Vector2.Distance(new Vector2(-100000, -100000), new Vector2(x.m_pos.x, x.m_pos.z)).CompareTo(Vector2.Distance(new Vector2(-100000, -100000), new Vector2(y.m_pos.x, y.m_pos.z)));
                    });


                    List<string> output = new List<string>();
                    foreach(var pin in pinList)
                    {
                        if(IsCustomPin(pin))
                            output.Add(JsonUtility.ToJson(new MyPinData(pin)));
                    }
                    File.WriteAllText(Path.Combine(AedenthornUtils.GetAssetPath(context, true), file), $"{string.Join("\n", output)}");
                    __instance.AddString(text);
                    __instance.AddString($"{context.Info.Metadata.Name} {output.Count} pins exported");
                    return false;
                }
                if (Minimap.instance && text.ToLower().Equals($"{typeof(BepInExPlugin).Namespace.ToLower()} clear"))
                {
                    __instance.AddString(text);
                    List<Minimap.PinData>pinList = (AccessTools.DeclaredField(typeof(Minimap), "m_pins").GetValue(Minimap.instance) as List<Minimap.PinData>);
                    for(int i = pinList.Count - 1; i >= 0; i--)
                    {
                        var pin = pinList[i];
                        if (IsCustomPin(pin))
                            pinList.RemoveAt(i);
                    }
                    __instance.AddString($"{context.Info.Metadata.Name} all pins cleared from map");
                    return false;
                }
                if (Minimap.instance && text.ToLower().StartsWith($"{typeof(BepInExPlugin).Namespace.ToLower()} import"))
                {
                    string file = text.Length > $"{typeof(BepInExPlugin).Namespace.ToLower()} import ".Length ? text.Substring($"{typeof(BepInExPlugin).Namespace.ToLower()} import ".Length) + ".txt" : "pindump.txt";
                    __instance.AddString(text);
                    string path = Path.Combine(AedenthornUtils.GetAssetPath(context, true), file);
                    if(!File.Exists(path))
                    {
                        __instance.AddString($"file {path} not found");
                        return false;
                    }
                    string[] pinStrings = File.ReadAllLines(path);

                    var count = 0;
                    foreach(var str in pinStrings)
                    {
                        try
                        {
                            var pin = JsonUtility.FromJson<MyPinData>(str);
                            Minimap.instance.AddPin(pin.position, (Minimap.PinType)pin.type, pin.name, true, false, 0L);
                            count++;
                        }
                        catch(Exception ex)
                        {
                            context.Logger.LogError(ex);
                        }
                    }
                    __instance.AddString($"{context.Info.Metadata.Name} {count} pins imported");
                    return false;
                }
                return true;
            }

            private static bool IsCustomPin(Minimap.PinData pin)
            {
                return pin.m_save && pin.m_type != Minimap.PinType.Death && pin.m_type != Minimap.PinType.Bed && pin.m_type != Minimap.PinType.Icon4 && pin.m_type != Minimap.PinType.Shout && pin.m_type != Minimap.PinType.None && pin.m_type != Minimap.PinType.Boss && pin.m_type != Minimap.PinType.Player && pin.m_type != Minimap.PinType.RandomEvent && pin.m_type != Minimap.PinType.Ping && pin.m_type != Minimap.PinType.EventArea;
            }
        }
    }
}
