using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public class Wheel
        {
            private const float k_MinAirPressure = 0;
            private readonly float r_MaxAirPressure;
            private string m_ManufacturerName;
            private float m_AirPressure;

            public Wheel(string i_ManufacturerName, float i_AirPressure, float i_MaxAirPressure)
            {
                m_ManufacturerName = i_ManufacturerName;
                m_AirPressure = i_AirPressure;
                r_MaxAirPressure = i_MaxAirPressure;
            }

            public string Manufacturer
            {
                get
                {
                    return m_ManufacturerName;
                }
            }

            public float AirPressure
            {
                get
                {
                    return m_AirPressure;
                }
            }

            public void WheelAirFill(float i_AirPressureToAdd)
            {
                float finalPressure = m_AirPressure + i_AirPressureToAdd;

                if (finalPressure > r_MaxAirPressure || finalPressure < k_MinAirPressure)
                {
                    throw new ValueOutOfRangeException(k_MinAirPressure, r_MaxAirPressure);
                }
                else
                {
                    m_AirPressure = finalPressure;
                }
            }

            public void FillAirPressureToMax()
            {
                m_AirPressure = r_MaxAirPressure;
            }

            public override string ToString()
            {
                return string.Format("manufacturer : {0}, air pressure : {1}", Manufacturer, AirPressure);
            }

            public Wheel ShalowClone()
            {
                return this.MemberwiseClone() as Wheel;
            }
        }

        public abstract class Engine
        { 
            private const float k_DefultValueToEnginePrecent = -1f;
            protected float m_EnergyPrecent;

            public Engine()
            {
                m_EnergyPrecent = k_DefultValueToEnginePrecent;
            }

            public float Energy
            {
                get
                {
                    if (m_EnergyPrecent == k_DefultValueToEnginePrecent)
                    {
                        throw new ArgumentNullException();
                    }

                    return m_EnergyPrecent;
                }
            }

            public override string ToString()
            {
                return string.Format("Energy precent left : {0}{1}", Energy, Environment.NewLine);
            }

            public abstract void InitCurrentEnergyCapacity(float i_BatteryOrFuelEnergy);
        }

        protected readonly string m_LicenseNumber;
        protected string m_ModelName = null;
        protected Engine m_VehicleEngine;
        protected List<Wheel> m_VehacleWheels;

        public Vehicle(string i_LicenseNumber, Engine i_VehicleEngine)
        {
            m_LicenseNumber = i_LicenseNumber;
            m_VehacleWheels = null;
            m_VehicleEngine = i_VehicleEngine;
        }

        private void initModelName(string i_ModelName)
        {
            if (m_ModelName == null)
            {
                m_ModelName = i_ModelName;
            }
            else
            {
                throw new NullReferenceException("Model name allready initialized");
            }
        }

        public Engine VehicleEngine
        {
            get
            {
                return m_VehicleEngine;
            }
        }

        public string Model
        {
            get
            {
                if (m_ModelName == null)
                {
                    throw new NullReferenceException("Initializing error, model name didn`t initialize");
                }

                return m_ModelName;
            }
        }

        public string License
        {
            get
            {
                return m_LicenseNumber;
            }
        }

        internal bool IfWheelsWasInit
        {
            get
            {
                bool v_WasInit = false;

                if (m_VehacleWheels != null)
                {
                    v_WasInit = true;
                }

                return v_WasInit;
            }
        }

        private void initVehicleWheels(string i_ManufacturerName, float i_AirPressure, float i_MaxAirPressure, short i_WheelsNumber)
        {
            if (!IfWheelsWasInit || m_VehacleWheels.Count < i_WheelsNumber)
            {
                m_VehacleWheels = new List<Wheel>(i_WheelsNumber);
                if (i_AirPressure > i_MaxAirPressure)
                {
                    throw new ArgumentException("Current air pressure is bigger than the wheels max air pressure");
                }

                for (int loopIndex = 0; loopIndex < i_WheelsNumber; loopIndex++)
                {
                    m_VehacleWheels.Add(new Wheel(i_ManufacturerName, i_AirPressure, i_MaxAirPressure));
                }
            }
            else
            {
                throw new ArgumentException("Wheels allready initialized");
            }
        }

        public override string ToString()
        {
            if (!IfWheelsWasInit)
            {
                throw new NullReferenceException();
            }

            return string.Format(
                "License number: {0}{4}Model name: {1}{4}Wheels manufacturer : {2}{4}Wheels air pressure : {3}{4}",
                License,
                Model, 
                m_VehacleWheels[0].Manufacturer,
                m_VehacleWheels[0].AirPressure,
                Environment.NewLine);
        }

        public void ChargeElectricEngine(float i_HourToCharge)
        {
            if (m_VehicleEngine is VehicleElectricEngine)
            {
                (m_VehicleEngine as VehicleElectricEngine).HoursToLoadTheBattery(i_HourToCharge);
            }
            else
            {
                throw new InvalidCastException("Vehicle engine type unmatch to electric engine");
            }
        }

        public void ChargeGazEngine(float i_FuelLitersToAdd, VehicleGasEngine.eFuelModel i_FuelModel)
        {
            if (m_VehicleEngine is VehicleGasEngine)
            {
                (m_VehicleEngine as VehicleGasEngine).AddFuel(i_FuelLitersToAdd, i_FuelModel);
            }
            else
            {
                throw new InvalidCastException("Vehicle engine type unmatch to gas engine");
            }
        }

        public void FillAllWheelsToMaxPressure()
        {
            if (IfWheelsWasInit)
            {
                foreach (Wheel wheel in m_VehacleWheels)
                {
                    wheel.FillAirPressureToMax();
                }
            }
            else
            {
                throw new NullReferenceException("Wheels wasn`t initialized yet");
            }
        }

        private bool checkIfManufacturerNameValide(string i_StrToCheck)
        {
            bool v_IsValide = true;

            for (int index = 0; index < i_StrToCheck.Length; index++)
            {
                if (!char.IsLetter(i_StrToCheck[index]))
                {
                    v_IsValide = false;
                }
            }

            return v_IsValide;
        }

        public bool convertAirPressureToFloat(string i_StrToConvert, out float o_AirPressure)
        {
            bool v_ConvertWasSuccess = false;

            if (float.TryParse(i_StrToConvert, out o_AirPressure))
            {
                v_ConvertWasSuccess = true;
            }

            return v_ConvertWasSuccess;
        }

        protected void UpdateWheel(string i_ManufacturerName, string i_CurrentAirPressure, float i_MaxAirPressure, short i_WheelsNumber)
        {
            bool v_ValidManufacturer = false, v_ValidAirPressure = false;
            float airPressure = 0;

            if (!string.IsNullOrEmpty(i_ManufacturerName) && !string.IsNullOrEmpty(i_CurrentAirPressure))
            {
                v_ValidManufacturer = checkIfManufacturerNameValide(i_ManufacturerName);
                if (!v_ValidManufacturer)
                {
                    throw new ArgumentException("Wheels Manufecturer unvalid name");
                }

                v_ValidAirPressure = convertAirPressureToFloat(i_CurrentAirPressure, out airPressure);
                if (!v_ValidAirPressure)
                {
                    throw new ArgumentException("Wheels air pressure unvalid");
                }

                if (v_ValidAirPressure && v_ValidManufacturer)
                {
                    initVehicleWheels(i_ManufacturerName, airPressure, i_MaxAirPressure, i_WheelsNumber);
                }
            }
            else
            {
                throw new NullReferenceException("Empty value error");
            }
        }

        public void UpdateModelName(string i_ModelName)
        {
            if (!string.IsNullOrEmpty(i_ModelName))
            {
                for (int index = 0; index < i_ModelName.Length; index++)
                {
                    if (!char.IsLetter(i_ModelName[index]))
                    {
                        throw new ArgumentException("Unvalid model name input");
                    }
                }
            }
            else
            {
                throw new NullReferenceException("Null Model Name");
            }

            initModelName(i_ModelName);
        }

        public abstract void UpdateVehicleWheels(string i_ManufacturerName, string i_CurrentAirPressure);

        public abstract void UpdateDetailsByVehicleType(List<string> i_AllDetailsToUpdate);

        public abstract List<string> GetVehicleMessages();
    }
}
