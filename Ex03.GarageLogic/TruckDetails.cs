using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal struct TruckDetails
    {
        internal const VehicleGasEngine.eFuelModel k_TruckFuel = VehicleGasEngine.eFuelModel.Soler;
        internal const float k_MaxTruckFuelCapacity = 120f;
        internal const float k_MaxTruckAirPressure = 28f;
        internal const short k_TruckWheels = 16;
        private bool m_HasDangerousSubstances;
        private float m_CargoVolume;

        internal TruckDetails(bool i_IsDangerous, float i_CargoVolume)
        {
            m_HasDangerousSubstances = i_IsDangerous;
            m_CargoVolume = i_CargoVolume;
        }

        public bool Dangerous
        {
            get
            {
                return m_HasDangerousSubstances;
            }
        }

        public float Cargo
        {
            get
            {
                return m_CargoVolume;
            }
        }

        public override string ToString()
        {
            return string.Format(
                "Dangerous truck : {0}{2}Cargo volume : {1}{2}",
                Dangerous.ToString(),
                Cargo,
                Environment.NewLine);
        }
    }
}
