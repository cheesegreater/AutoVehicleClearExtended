using System;
using System.Collections.Generic;
using System.Linq;

using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;

using UnityEngine;

using SDG.Unturned;


namespace LehGogh.AutoVehicleClearExtended
{
    public class IsSavedCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "is_saved";

        public string Help => "Checks if the targeted vehicle is currently saved.";

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
                        UnturnedChat.Say(player, AutoVehicleClear.Instance.Translate("vehicle_is_saved_true"));
                        return;
                    }
                    else
                    {
                        UnturnedChat.Say(player, AutoVehicleClear.Instance.Translate("vehicle_is_saved_false"));
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
