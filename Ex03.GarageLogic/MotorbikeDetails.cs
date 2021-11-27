using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal struct MotorbikeDetails
    {
        internal enum eLicense : short
        {
            A,
            A1,
            AA,
            B
        }

        internal const VehicleGasEngine.eFuelModel k_MotorFuel = VehicleGasEngine.eFuelModel.Octan95;
        internal const float k_MaxMotorFuelCapacity = 7f;
        internal const float k_MaxBatteryCapacity = 1.2f;
        internal const float k_MaxMotorAirPressure = 30f;
        internal const short k_MotorbikeWheels = 2;
        private eLicense m_LicenseType;
        private int m_EngineBlacksmith;

        public MotorbikeDetails(eLicense i_LicenseType, int i_EngineBlacksmith)
        {
            m_LicenseType = i_LicenseType;
            m_EngineBlacksmith = i_EngineBlacksmith;
        }

        public eLicense License
        {
            get
            {
                return m_LicenseType;
            }
        }

        public int EngineBlacksmith
        {
            get
            {
                return m_EngineBlacksmith;
            }
        }

        public override string ToString()
        {
            return string.Format(
                "License type : {0}{2}Engine blacksmith : {1}{2}", 
                License.ToString(), 
                EngineBlacksmith,
                Environment.NewLine);
        }
    }
}
