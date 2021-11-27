using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal struct CarDetails
    {
        internal enum eColor : short
        {
            Red,
            White,
            Black,
            Silver
        }

        internal enum eDoors : short
        {
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5
        }

        internal const VehicleGasEngine.eFuelModel k_CarFuel = VehicleGasEngine.eFuelModel.Octan96;
        internal const float k_MaxCarFuelCapacity = 60f;
        internal const float k_MaxCarBatteryCapacity = 2.1f;
        internal const float k_MaxCarAirPressure = 32f;
        internal const short k_CarWheels = 4;
        private eColor m_CarColor;
        private eDoors m_CarDoorsQuantity;

        public CarDetails(eColor i_CarColor, eDoors i_CarDoors)
        {
            m_CarColor = i_CarColor;
            m_CarDoorsQuantity = i_CarDoors;
        }

        public eColor Color
        {
            get
            {
                return m_CarColor;
            }
        }

        public eDoors Doors
        {
            get
            {
                return m_CarDoorsQuantity;
            }
        }

        public override string ToString()
        {
            return string.Format(
                "Car Color : {0}{2}Car doors number : {1}{2}", 
                Color.ToString(), 
                Doors.ToString(),
                Environment.NewLine);
        }
    }
}
