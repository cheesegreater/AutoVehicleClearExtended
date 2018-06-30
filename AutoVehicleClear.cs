using SDG.Unturned;
using System.Linq;

using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.API.Collections;
using Logger = Rocket.Core.Logging.Logger;

using UnityEngine;


namespace LehGogh.AutoVehicleClearExtended
{
	public class AutoVehicleClear : RocketPlugin<AutoVehicleClearConfiguration>
	{
		public const string version = "v1.0.1";
        public static AutoVehicleClear Instance;

		private AutoVehicleClearConfiguration config;

		protected override void Load()
		{
			config = Configuration.Instance;

            Instance = this;

			Logger.Log("Starting AutoVehicleClear Extended " + version + "!");
            Logger.Log("Made by PhaserArray, extended by LehGogh");
			InvokeRepeating("ClearVehicles", config.ClearInterval, config.ClearInterval);
		}

		protected override void Unload()
		{
			CancelInvoke("ClearVehicles");
		}

		public void ClearVehicles()
		{
			Logger.Log("Clearing vehicles!");

			var cleared = 0;
			var vehicles = VehicleManager.vehicles;
			for (int i = vehicles.Count - 1; i >= 0; i--)
			{
				var vehicle = vehicles[i];
				if (CanClearVehicle(vehicle))
				{
                    if (!this.config.VehiclesToSave.Contains(vehicles[i].instanceID))
                    {
                        VehicleManager.instance.channel.send("tellVehicleDestroy", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, vehicle.instanceID);
                        cleared++;
                    }
				}
			}

			Logger.Log($"Cleared {cleared} vehicles!");
			if (config.SendClearMessage && cleared > 0)
			{
				UnturnedChat.Say(Translate("autovehicleclear_cleared_vehicles", cleared), Color.green);
			}
		}

		public bool CanClearVehicle(InteractableVehicle vehicle)
		{
			if (vehicle.passengers.Any(p => p.player != null) || vehicle.asset.engine == EEngine.TRAIN)
			{
				return false;
			}
			if (config.ClearAll)
			{
				return true;
			}
			if (config.ClearExploded && vehicle.isExploded)
			{
				return true;
			}
			if (config.ClearDrowned)
			{
				if (vehicle.isDrowned && vehicle.transform.FindChild("Buoyancy") == null)
				{
					return true;
				}
			}
			if (config.ClearNoTires)
			{
				var tires = vehicle.transform.FindChild("Tires");
				if (tires != null)
				{
					var totalTires = vehicle.transform.FindChild("Tires").childCount;
					if (totalTires == 0)
					{
						return false;
					}

					var aliveTires = 0;
					for (var i = 0; i < totalTires; i++)
					{
						if (tires.GetChild(i).gameObject.activeSelf)
						{
							aliveTires++;
						}
					}
					if (aliveTires == 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		public override TranslationList DefaultTranslations
		{
			get
			{
                return new TranslationList()
                {
                    {"cleared_vehicles", "Cleared {0} vehicles!"},
                    {"vehicle_already_saved", "This vehicle has already been saved." },
                    {"vehicle_save", "This vehicle has been saved." },
                    {"vehicle_not_saved", "This vehicle has not been saved yet." },
                    {"vehicle_unsave", "This vehicle has been unsaved." },
                    {"vehicle_is_saved_true", "This vehicle is saved." },
                    {"vehicle_is_saved_false", "This vehicle is not saved." }
				};
			}
		}
	}
}
