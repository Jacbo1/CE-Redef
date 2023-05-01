# CE Redef
Now rewritten in C#.  
[.NET 4.7.2](https://dotnet.microsoft.com/en-us/download/dotnet-framework/thank-you/net472-web-installer) is required.  
Redefine [Combat Extended](https://steamcommunity.com/workshop/filedetails/?id=1631756268) ([GitHub](https://github.com/CombatExtended-Continued/CombatExtended)) statistics with global multipliers. The multipliers are
* Bulk (\<bulk>)
* Carry bulk (\<CarryBulk>)
* Worn bulk (\<WornBulk>)
* Reload speed (\<ReloadSpeed>)
* Reload time (\<reloadTime>)
* Ranged cooldown (\<RangedWeapon_Cooldown>)
* Melee cooldown (\<cooldownTime>)
* Warmup time (\<warmupTime>)

![Screenshot](https://github.com/Jacbo1/CE-Redef/blob/main/screenshot.png?raw=true)  

Note for modders: This will ignore values within \<equippedStatOffsets> tags for example the tactical vest and backpacks use this to apply the +30 bulk capacity.

## How to use
Safe to use mid-save but make sure to restore the values before deleting.
1. Set the Combat Extended directory to the location of the mod. It will typically be C:\Program Files (x86)\Steam\steamapps\workshop\content\294100\1631756268\ if it was downloaded from the workshop or C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\CombatExtended\ if was installed manually.
2. Set the location of the backup file. This is required to both restore and redefine.
3. Once you have your multipliers set, check "Redefine" and click the button at the bottom.

If you use [RimPy](https://steamcommunity.com/sharedfiles/filedetails/?id=1847679158), you can find Combat Extended there, right click it, and click "Open Folder" to get the CE directory.  
If you want to change the multipliers again, you can just redefine it again without restoring first.  
Restoring reverts all multiplied values.
If set to backup and there is an existing backup file, it will warn you if it is for the same version of Combat Extended because it may be modified values that would be saved and overwrite the original backup.  
  
If the stats for some items/weapons don't change, they most likely do not have the tags this looks for.
