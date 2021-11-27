# Changes
v0.0.1
 - Moved the directory for the json files to the config folder. This will allow the TS mod manager to place the json files in a location so others can distribute them.
 - renamed the console command from `buildpiecetweaks` to `bpc`
 - Improved the logging.

This mod lets you edit various values for individual build pieces and has global toggles for build pieces in general.


## Setting Individual Values

Build piece values can be saved to json files in BepInEx\config\BuildPieceCustomizer. To create a template json file for a specific build piece, type the following command in the console (F5):

`bpc save <PieceSpawnName>`

E.g. `bpc save piece_sharpstakes` will create a file called piece_sharpstakes.json with the default values for Sharp Stakes.  Edit that file as you
like, save it, then reload the game.

The variables are mostly self-explanatory, except for the following:

## station

Station is the name tag of the crafting station required to build the piece. You can remove the requirement by setting this to "" or changing it to the name tag of a crafting station, e.g.

`"station": "$piece_stonecutter",`

I don't have a list of the name tags of all the crafting stations.

## DamageModifiers

Damage modifiers are a list of colon-separated pairs, e.g. for the sharp stakes:
```
"damageModifiers": [
    "Blunt:Normal",
    "Slash:Normal",
    "Pierce:Resistant",
    "Chop:Weak",
    "Pickaxe:Normal",
    "Fire:Normal",
    "Frost:Normal",
    "Lightning:Normal",
    "Poison:Immune",
    "Spirit:Immune"
]
```
The first value is the damage type, the second value is the resistance level.

Valid resistance levels include:
```
Normal
Resistant
Weak
Immune
Ignore
VeryResistant
VeryWeak
```
You can also dump this list using

`bpc damage`


## Category

Valid categories are:
```
Misc 0
Crafting 1
Building 2
Furniture 3
Max 4
All 100
```
Use the number in the json file. You can also dump this list using

bpc cats


## ComfortGroup

Valid comfort groups are:
```
None 0
Fire 1
Bed 2
Banner 3
Chair 4
```
Use the number in the json file. You can also dump this list using

`bpc comfort`


## Global Multipliers

The mod also provides the following global armor modifiers via the config file:
```
GlobalPieceClipEverything
GlobalAllowedInDungeons
GlobalRepairPiece
GlobalCanBeRemoved
```

## Config

A config file BepInEx/config/thedefside.BuildPieceCustomizer.cfg is created after running the game once with this mod.

You can adjust the config values by editing this file using a text editor or in-game using the Config Manager﻿.

To reload the config from the config file, type `bpc reset` into the game's console (F5).
