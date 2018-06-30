using System;
using System.Collections.Generic;
using System.Linq;

using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using Logger = Rocket.Core.Logging.Logger;

using UnityEngine;

using SDG.Unturned;


namespace LehGogh.AutoVehicleClearExtended
{
    public class SaveVehicleCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "v_save";

        public string Help => "Prevents the targeted vehicle from being automatically cleared.";

        public string Syntax => "";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "autovehicleclear.can_save_unsave_vehicles" };


        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;

            Transform raycast = Raycast(player);
            if (raycast != null)
            {
                InteractableVehicle vehicle = raycast.GetComponent<InteractableVehicle>();
                if (vehicle != null)
                {
                    if (AutoVehicleClear.Instance.Configuration.Instance.VehiclesToSave.Contains(vehicle.instanceID))
                    {
                        UnturnedChat.Say(player, AutoVehicleClear.Instance.Translate("vehicle_already_saved"));
                        return;
                    }
                    else
                    {
                        UnturnedChat.Say(player, AutoVehicleClear.Instance.Translate("vehicle_save"));
                        Logger.Log(String.Format("Player {0} (CSteamID {1}) saved vehicle with ID {2}",
                            player.DisplayName, player.CSteamID, vehicle.instanceID.ToString()));
                        AutoVehicleClear.Instance.Configuration.Instance.VehiclesToSave.Add(vehicle.instanceID);
                        AutoVehicleClear.Instance.Configuration.Save();
                        return;
                    }
                }
            }
        }


        // Taken from https://github.com/cheesynuggets/DoorPlugin69/blob/master/DoorPlugin.cs#L158
        public static Transform Raycast(IRocketPlayer rocketPlayer)
        {
            UnturnedPlayer player = (UnturnedPlayer)rocketPlayer;
            if (Physics.Raycast(player.Player.look.aim.position, player.Player.look.aim.forward, out RaycastHit hit, float.MaxValue, RayMasks.BARRICADE_INTERACT))
            {
                Transform transform = hit.transform;
                return transform;
            }
            return null;
        }
    }
}
