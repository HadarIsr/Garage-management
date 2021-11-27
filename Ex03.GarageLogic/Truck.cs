using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const byte k_True = 1;
        private const byte k_Flase = 0;
        private TruckDetails? m_TruckDetails;

        internal Truck(string i_LicenseNumber, Engine i_VehicleEngine)
            : base(i_LicenseNumber, i_VehicleEngine)
        {
        }

        internal bool DetailsWasInit
        {
            get
            {
                return m_TruckDetails.HasValue;
            }
        }

        internal TruckDetails? Details
        {
            get
            {
                if(!DetailsWasInit)
                {
                    throw new ArgumentNullException("Initializing error, Truck details was`nt initialize");
                }

                return m_TruckDetails;
            }
        }

        private void getIfDangerousTruck(string i_InputToCheck, ref bool io_IfDangerous)
        {
            short inputConvert;

            if (short.TryParse(i_InputToCheck, out inputConvert))
            {
                if (inputConvert == k_Flase)
                {
                    io_IfDangerous = false;
                }
                else if (inputConvert == k_True)
                {
                    io_IfDangerous = true;
                }
                else
                {
                    throw new ValueOutOfRangeException(k_Flase, k_True);
                }
            }
            else
            {
                throw new ArgumentNullException("Please insert a number");
            }
        }

        public override string ToString()
        {
            if(!DetailsWasInit)
            {
                throw new ArgumentNullException("Initializing error, Truck details was`nt initialize");
            }

            return string.Format(
                "{0}{1}",
                base.ToString(),
                Details.ToString());
        }

        public override List<string> GetVehicleMessages()
        {
            List<string> messagesToReturn = new List<string>();
            bool v_IsDangerous = true;

            messagesToReturn.Add(string.Format("Please insert the cargo volume : {0}", Environment.NewLine));
            messagesToReturn.Add(string.Format("Please type the number 0.{1} or 1.{0} if the track as dangerous material", v_IsDangerous, !v_IsDangerous));

            return messagesToReturn;
        }

        private void getCargoVolume(string i_InputToConvert, ref float io_CargoVolume)
        {
            if (!float.TryParse(i_InputToConvert, out io_CargoVolume))
            {
                throw new ArgumentException("Cargo must be a number ERROR");
            }
        }

        public override void UpdateDetailsByVehicleType(List<string> i_AllDetailsToUpdate)
        {
            if (!DetailsWasInit)
            {
                bool v_IfDangerous = false;   ////defult to parameters
                float cargoVolume = -1;

                if (!string.IsNullOrEmpty(i_AllDetailsToUpdate[0]) && !string.IsNullOrEmpty(i_AllDetailsToUpdate[1]))
                {
                    getCargoVolume(i_AllDetailsToUpdate[0], ref cargoVolume);
                    getIfDangerousTruck(i_AllDetailsToUpdate[1], ref v_IfDangerous);
                }
                else
                {
                    throw new ArgumentNullException("One or more values empty");
                }

                m_TruckDetails = new TruckDetails(v_IfDangerous, cargoVolume);
            }
            else
            {
                throw new ArgumentException("Truck details allreadywas initialized");
            }
        }

        public override void UpdateVehicleWheels(string i_ManufacturerName, string i_CurrentAirPressure)
        {
            UpdateWheel(i_ManufacturerName, i_CurrentAirPressure, TruckDetails.k_MaxTruckAirPressure, TruckDetails.k_TruckWheels);           
        }
    }
}
