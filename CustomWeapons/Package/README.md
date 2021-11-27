# Changes
v0.0.1
 - Moved the directory for the json files to the config folder. This will allow the TS mod manager to place the json files in a location so others can distrubute them.
 - renamed the console command from `customweaponstats` to `wc`
 - Added 'MovementSpeed' field to the files so you can adjust the movement speed modifier of the weapons.
 - Improved the logging.

This mod lets you edit various stats for individual weapons and has a number of global weapon multipliers as well.

## Setting Weapon Stats

Custom weapon stats are stored in json files in BepInEx\config\WeaponCustomizer. To create a template json file for a specific weapon, type the following command in the console (F5):

`wc save <ItemSpawnName>`

E.g. `wc save SwordBronze` will create a file called SwordBronze.json with the default values for a bronze sword.  Edit that file as you like, save it, then type the following command in the console:

`wc reload`

This will reload the file, applying the new values when someone attacks with a bronze sword.

The variables are mostly self-explanatory.

For status effects, you can get a complete list by typing the following command in the console:

`wc se`

Only one status effect can be applied to a weapon, afaics.

For skills, you can get a complete list by typing the following command in the console:

`wc skills`

Put the number for the skill in your json file.

## Global Multipliers

The mod also provides the following global weapon modifiers via the config file:
```
GlobalDamageMultiplier
GlobalUseDurabilityMultiplier
GlobalAttackForceMultiplier
GlobalBackstabBonusMultiplier
GlobalHoldDurationMinMultiplier
```

## Config

A config file BepInEx/config/thedefside.WeaponCustomizer.cfg is created after running the game once with this mod.

You can adjust the config values by editing this file using a text editor or in-game using the Config Manager﻿.

To reload the config from the config file, type `wc reset` into the game's console (F5).


Technical

To install this mod, the easiest way is to just use r2Modman, the Thunderstore mod manager. It should take care of all dependencies.

To install manually, place the dll file in the BepInEx/plugins folder. You will need BepInEx.

Code is at [https://github.com/thedefside/ValheimMods/tree/master/CustomWeapons](https://github.com/thedefside/ValheimMods/tree/master/CustomWeapons).
