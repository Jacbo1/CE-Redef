# CE Redef
Redefine [Combat Extended](https://steamcommunity.com/workshop/filedetails/?id=1631756268) ([GitHub](https://github.com/CombatExtended-Continued/CombatExtended)) statistics with global multipliers. The multipliers are
* Bulk (\<bulk>)
* Carry bulk (\<CarryBulk>)
* Worn bulk (\<WornBulk>)
* Reload speed (\<ReloadSpeed>)
* Reload time (\<reloadTime>)
* Ranged cooldown (\<RangedWeapon_Cooldown>)
* Melee cooldown (\<cooldownTime>)
* Warmup time (\<warmupTime>)

![Screenshot](https://github.com/Jacbo1/CE-Redef/blob/main/Screenshot.png)  

## How to use
[Java](https://www.java.com/en/) is required.
1. Set the Combat Extended directory to the location of the mod. It will typically be C:\Program Files (x86)\Steam\steamapps\workshop\content\294100\1631756268\
2. Set the location of the backup file. This is required to both restore and redefine.
3. Once you have your multipliers set, check "Redefine" and click the button at the bottom.

If you want to change the multipliers again, you can just redefine it again without restoring first.  
Restoring reverts all multiplied values.
If set to backup and there is an existing backup file, it will warn you if it is for the same version of Combat Extended because it may be modified values that would be saved and overwrite the original backup.  
  
If the stats for some items/weapons don't change, they most likely do not have the tags this looks for.

This program uses [ini4j](http://ini4j.sourceforge.net/)
