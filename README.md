# AutoVehicleClearExtended
Based on PhaserArray's AutoVehicleClear, but now with more features!

Installation is simple, just download the DLL and move it to your Server's `/Rocket/Plugins` directory. Upon starting the server, a default config wil be generated.

## Commands

`/save_vehicle` - Prevents the targeted vehicle from being automatically cleared.
- Requires permission `autovehicleclear.can_save_unsave_vehicles`
- Vehicles are targeted via line-of-sight

`/unsave_vehicle` - Allows the targeted vehicle to be automatically cleared.
- Requires permission `autovehicleclear.can_save_unsave_vehicles`
- Vehicles are targeted via line-of-sight

`/is_saved` - Checks if the targeted vehicle is currently saved.
- Requires permission `autovehicleclear.can_save_unsave_vehicles`
- Vehicles are targeted via line-of-sight

## Permissions

This plugin only has one permission: `autovehicleclear.can_save_unsave_vehicles`. Players with this permission can use `/save_vehicle`, `/unsave_vehicle`, and `/is_saved`.

## Configuration

The config contains seven values:  
`ClearInterval` - 600 by default, seconds between vehicle clears.  
`SendClearMessage` - True by default, if false, a clear message will not be broadcasted.  
`ClearAll` - False by default, if true, all empty vehicles will be cleared, enabling this overrides the three options below. NOT RECOMMENDED as there is currently no warning for players to get in vehicles they do not want to lose.  
`ClearExploded` - True by default, clears vehicle corpses.  
`ClearDrowned` - True by default, clears underwater vehicles that do not have a Bouyancy object (will not clear vehicles like boats, seaplanes, amphibious cars, etc)  
`ClearNoTires` - True by default, clears vehicles that have tires in a healthy state but no longer have tires.
`VehiclesToSave` - A list of vehicle instance IDs to save (prevent from behing auto-cleared). You shouldn't normally set IDs manually, but by using the `/save_vehicle` and `/unsave_vehicle` commands.
