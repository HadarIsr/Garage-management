using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleGasEngine : Vehicle.Engine
    {
        public enum eFuelModel
        {
            Soler,
            Octan95,
            Octan96,
            Octan98
        }

        protected const byte k_MinFuelQuantity = 0;
        protected readonly float r_MaxFuelCapacity;
        protected readonly eFuelModel r_FuelModel;
        protected float m_CurrentFuelQuantity;

        internal VehicleGasEngine(float i_MaxFuelCapacity, eFuelModel i_FuelType)
        {
            r_FuelModel = i_FuelType;
            r_MaxFuelCapacity = i_MaxFuelCapacity;
            m_CurrentFuelQuantity = k_MinFuelQuantity;
        }

        internal static void FindIfChoiseExistInEfuelType(string i_StrToCheck, ref eFuelModel io_FuelType)
        {
            bool v_IsExistInEnum = false;
            short choiseConvert;

            if (short.TryParse(i_StrToCheck, out choiseConvert))
            {
                foreach (eFuelModel fuelModel in Enum.GetValues(typeof(eFuelModel)))
                {
                    if ((short)fuelModel == choiseConvert)
                    {
                        v_IsExistInEnum = true;
                        io_FuelType = fuelModel;
                        break;
                    }
                }
            }

            if (!v_IsExistInEnum)
            {
                throw new ArgumentException("Invalid fuel type");
            }
        }

        internal void AddFuel(float i_FuelLitersToAdd, eFuelModel i_FuelModel)
        {
            float finalFuelQuantity = m_CurrentFuelQuantity + i_FuelLitersToAdd;
            if (i_FuelModel != FuelModel)
            {
                throw new ArgumentException("Fuel type error");
            }
            else
            {
                if (finalFuelQuantity > r_MaxFuelCapacity || finalFuelQuantity < k_MinFuelQuantity)
                {
                    throw new ValueOutOfRangeException(k_MinFuelQuantity, r_MaxFuelCapacity);
                }
                else
                {
                    m_CurrentFuelQuantity += i_FuelLitersToAdd;
                    m_EnergyPrecent = m_CurrentFuelQuantity / r_MaxFuelCapacity * 100;
                }
            }
        }

        public eFuelModel FuelModel
        {
            get
            {
                return r_FuelModel;
            }
        }

        public float FuelQuantity
        {
            get
            {
                return m_CurrentFuelQuantity;
            }
        }

        public static List<string> GetGasFuelTypeMesseges()
        {
            StringBuilder VehicleGasEngineMessegeToReturn = new StringBuilder();
            List<string> messagesToReturn = new List<string>();

            short choiseIndex = 0;

            VehicleGasEngineMessegeToReturn.AppendLine("Please choose fuel type from those options : ");
            foreach (eFuelModel fuel in Enum.GetValues(typeof(eFuelModel)))
            {
                VehicleGasEngineMessegeToReturn.AppendFormat("{0}.{1}{2}", choiseIndex++, fuel.ToString(), Environment.NewLine);
            }

            VehicleGasEngineMessegeToReturn.AppendLine();
            messagesToReturn.Add(VehicleGasEngineMessegeToReturn.ToString());
            messagesToReturn.Add("Please insert the quantity to fill : ");

            return messagesToReturn;
        }

        public override void InitCurrentEnergyCapacity(float i_BatteryOrFuelEnergy)
        {
            if (FuelQuantity == k_MinFuelQuantity)
            {
                AddFuel(i_BatteryOrFuelEnergy, r_FuelModel);
            }
            else
            {
                throw new ArgumentException("Filling fuel error");
            }
        }

        public override string ToString()
        {
            return string.Format(
                "{0}Fuel model : {1}{3}Fuel left : {2}{3}", 
                base.ToString(), 
                r_FuelModel.ToString(), 
                m_CurrentFuelQuantity,
                Environment.NewLine);
        }
    }
}
