using System.Collections.Generic;

using Rocket.API;


namespace LehGogh.AutoVehicleClearExtended
{
	public class AutoVehicleClearConfiguration : IRocketPluginConfiguration
	{
		public bool SendClearMessage;
		public bool ClearAll;
		public bool ClearExploded;
		public bool ClearDrowned;
		public bool ClearNoTires;
		
		public float ClearInterval;

        public List<uint> VehiclesToSave;


		public void LoadDefaults()
		{
			SendClearMessage = true;
			ClearAll = false;
			ClearExploded = true;
			ClearDrowned = true;
			ClearNoTires = true;

			ClearInterval = 600f;

            VehiclesToSave = new List<uint>();
		}
	}
}
