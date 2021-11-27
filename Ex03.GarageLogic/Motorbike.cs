using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorbike : Vehicle
    {
        private MotorbikeDetails? m_MotorbikeDetails;

        internal Motorbike(string i_LicenseNumber, Engine i_VehicleEngine)
             : base(i_LicenseNumber, i_VehicleEngine)
        {
        }

        internal bool DetailsWasInit
        {
            get
            {
                return m_MotorbikeDetails.HasValue;
            }
        }

        internal MotorbikeDetails? Details
        {
            get
            {
                if (!m_MotorbikeDetails.HasValue)
                {
                    throw new ArgumentNullException("Initializing error, Motorcycle details dosn`t exists");
                }

                return m_MotorbikeDetails;
            }
        }

        private void findIfChoiseExistInElicense(string i_StrToCheck, ref MotorbikeDetails.eLicense io_LicenseType)
        {
            bool v_IsExistInEnum = false;
            short choiseConvert;

            if (short.TryParse(i_StrToCheck, out choiseConvert))
            {
                foreach (MotorbikeDetails.eLicense license in Enum.GetValues(typeof(MotorbikeDetails.eLicense)))
                {
                    if ((short)license == choiseConvert)
                    {
                        v_IsExistInEnum = true;
                        io_LicenseType = license;
                        break;
                    }
                }
            }

            if (!v_IsExistInEnum)
            {
                throw new ValueOutOfRangeException(0, Enum.GetNames(typeof(MotorbikeDetails.eLicense)).Length - 1);
            }
        }

        public void convertBlacksmithInput(string i_StrToConvert, out int io_Blacksmith)
        {
            if (!int.TryParse(i_StrToConvert, out io_Blacksmith))
            {
                throw new ArgumentException("Blacksmith must be a number");
            }
        }

        public override string ToString()
        {
            if(!DetailsWasInit)
            {
                throw new ArgumentNullException("Initializing error, Motorcycle details dosn`t exists");
            }

            return string.Format(
                "{0}{1}",
                base.ToString(),
                Details.ToString());
        }

        public override List<string> GetVehicleMessages()
        {
            StringBuilder motobikeMessegeToReturn = new StringBuilder();
            List<string> messagesToReturn = new List<string>();
            short choiseIndex = 0;

            motobikeMessegeToReturn.AppendLine("Please choose your license from those options by choose number from those options : ");
            foreach (MotorbikeDetails.eLicense license in Enum.GetValues(typeof(MotorbikeDetails.eLicense)))
            {
                motobikeMessegeToReturn.AppendFormat("{1}.{2}{0}", Environment.NewLine, choiseIndex++, license.ToString());
            }

            messagesToReturn.Add(motobikeMessegeToReturn.ToString());
            messagesToReturn.Add(string.Format("{0}Please enter the engine blacksmith :", Environment.NewLine));

            return messagesToReturn;
        }

        public override void UpdateDetailsByVehicleType(List<string> i_AllDetailsToUpdate)
        {
            if (!DetailsWasInit)
            {
                MotorbikeDetails.eLicense license = MotorbikeDetails.eLicense.A;    ////defult to parameters
                int blackSmith = -1;

                if (!string.IsNullOrEmpty(i_AllDetailsToUpdate[0]) && !string.IsNullOrEmpty(i_AllDetailsToUpdate[1]))
                {
                    findIfChoiseExistInElicense(i_AllDetailsToUpdate[0], ref license);
                    convertBlacksmithInput(i_AllDetailsToUpdate[1], out blackSmith);
                }
                else
                {
                    throw new ArgumentNullException("One or more argument empty in motorbike details");
                }

                m_MotorbikeDetails = new MotorbikeDetails(license, blackSmith);
            }
            else
            {
                throw new ArgumentNullException("Motorbike details allready initialized");
            }
        }

        public override void UpdateVehicleWheels(string i_ManufacturerName, string i_CurrentAirPressure)
        {
            UpdateWheel(i_ManufacturerName, i_CurrentAirPressure, MotorbikeDetails.k_MaxMotorAirPressure, MotorbikeDetails.k_MotorbikeWheels);
        }
    }
}
