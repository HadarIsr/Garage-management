using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class VehicleElectricEngine : Vehicle.Engine
    {
        private const byte k_MinBattaryTime = 0;
        private readonly float r_MaxBatteryTime;
        private float m_TimeLeftForBattery;

        internal VehicleElectricEngine(float i_MaxBatteryTime)
        {
            r_MaxBatteryTime = i_MaxBatteryTime;
            m_TimeLeftForBattery = k_MinBattaryTime;
        }

        internal void HoursToLoadTheBattery(float i_HourToCharge)
        {
            float finalBatteryHours = m_TimeLeftForBattery + i_HourToCharge;

            if (finalBatteryHours > r_MaxBatteryTime || finalBatteryHours < k_MinBattaryTime)
            {
                throw new ValueOutOfRangeException(k_MinBattaryTime, r_MaxBatteryTime);
            }
            else
            {
                m_TimeLeftForBattery += i_HourToCharge;
                m_EnergyPrecent = m_TimeLeftForBattery / r_MaxBatteryTime * 100;
            }
        }

        internal float Battery
        {
            get
            {
                return m_TimeLeftForBattery;
            }
        }

        public override void InitCurrentEnergyCapacity(float i_BatteryOrFuelEnergy)
        {
            if (Battery == k_MinBattaryTime)
            {
                HoursToLoadTheBattery(i_BatteryOrFuelEnergy);
            }
            else
            {
                throw new ArgumentException("Changing battery energy error");
            }
        }

        public override string ToString()
        {
            return string.Format("{0}{2}Battery left : {1}{2}", base.ToString(), Battery, Environment.NewLine);
        }
    }
}
