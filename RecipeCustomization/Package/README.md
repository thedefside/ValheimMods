# Changes
v0.0.1
 - Moved the directory for the json files to the config folder. This will allow the TS mod manager to place the json files in a location so others can distrubute them.
 - renamed the console command from `recipecustomization` to `rc`
 - Improved the logging.



This mod lets you customize existing item and piece recipes using json files.

Recipe data is stored as json files in BepInEx\config\RecipeCustomizer. To create a template json file for a specific recipe, type the following command in the console (F5):

`rc save <ItemSpawnName>`

E.g. `rc save <ShieldWood>` will create a file called ShieldWood.json with the default recipe data for a wooden shield.  Edit that file as you
like, save it, then type the following command in the console:

`rc reload`

This will reload the file, applying the new values to the existing recipe.

The variables are mostly self-explanatory.

## Reqs

reqs is a list of the actual recipe requirements. Each entry is a string separated with colons,e.g.:
```
"reqs": [
    "FineWood:30:1:True",
    "DeerHide:10:1:True",
    "Resin:20:1:True",
    "BronzeNails:80:1:True"
]
```

The first part is the material's spawn name, the second is the amount, the third is the amount per level if the item being created is upgradeable, and the fourth is whether that material can be recovered on destroying the created item.

# CraftingStation

craftingStation is the name tag of the crafting station required to build the piece. You can remove the requirement by setting this to "", or you can change it to the name tag of a different crafting station, e.g.

`"craftingStation": "$piece_stonecutter",`

I don't have a list of the name tags of all the crafting stations.


## Disabled

If you set `"disabled": true`, it will remove this recipe from the game.

## Config

A config file BepInEx/config/thedefside.RecipeCustomizer.cfg is created after running the game once with this mod.

You can adjust the config values by editing this file using a text editor or in-game using the Config Manager﻿.

To reload the config from the config file, type `rc reset` into the game's console (F5).


Technical

To install this mod, the easiest way is to just use r2Modman, the Thunderstore mod manager. It should take care of all dependencies.

To install manually, place the dll file in the BepInEx/plugins folder. You will need BepInEx.

Code is at https://github.com/thedefside/ValheimMods/tree/master/RecipeCustomization.