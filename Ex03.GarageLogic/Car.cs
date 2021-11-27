using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private CarDetails? m_CarDetails;

        internal Car(string i_LicenseNumber, Engine i_VehicleEngine)
            : base(i_LicenseNumber, i_VehicleEngine)
        {           
        }

        internal bool DetailsWasInit
        {
            get
            {
                return m_CarDetails.HasValue;
            }
        }

        internal CarDetails? Details
        {
            get
            {
                if (!m_CarDetails.HasValue)
                {
                    throw new ArgumentNullException("Initializing error, car details was`nt initialized");
                }

                return m_CarDetails;
            }
        }

        public override string ToString()
        {
            if(!DetailsWasInit)
            {
                throw new ArgumentNullException("Initializing error, car details wasn`t initialized");
            }

            return string.Format(
                "{0}{1}",
                base.ToString(),
                Details.ToString());
        }

        private void findIfChoiseExistInEcolor(string i_StrToCheck, ref CarDetails.eColor io_Color)
        {
            bool v_IsExistInEnum = false;
            short choiseConvert;

            if (short.TryParse(i_StrToCheck, out choiseConvert))
            {
                foreach (CarDetails.eColor color in Enum.GetValues(typeof(CarDetails.eColor)))
                {
                    if ((short)color == choiseConvert)
                    {
                        v_IsExistInEnum = true;
                        io_Color = color;
                        break;
                    }
                }
            }

            if (!v_IsExistInEnum)
            {
                throw new ArgumentException("Invalid car color input");
            }
        }

        private void findIfChoiseExistInEdoors(string i_StrToCheck, ref CarDetails.eDoors io_Doors)
        {
            bool v_IsExistInEnum = false;
            short choiseConvert;

            if (short.TryParse(i_StrToCheck, out choiseConvert))
            {
                choiseConvert += (short)CarDetails.eDoors.Two;
                foreach (CarDetails.eDoors doors in Enum.GetValues(typeof(CarDetails.eDoors)))
                {
                    if ((short)doors == choiseConvert)
                    {
                        v_IsExistInEnum = true;
                        io_Doors = doors;
                        break;
                    }
                }
            }

            if (!v_IsExistInEnum)
            {
                throw new ArgumentException("Unvalid doors choise");
            }
        }

        public override void UpdateDetailsByVehicleType(List<string> i_AllDetailsToUpdate)
        {
            if (!DetailsWasInit)
            {
                CarDetails.eColor color = CarDetails.eColor.Black;    ////defult to parameters
                CarDetails.eDoors doors = CarDetails.eDoors.Four;

                if (!string.IsNullOrEmpty(i_AllDetailsToUpdate[0]) && !string.IsNullOrEmpty(i_AllDetailsToUpdate[1]))
                {
                    findIfChoiseExistInEcolor(i_AllDetailsToUpdate[0], ref color);
                    findIfChoiseExistInEdoors(i_AllDetailsToUpdate[1], ref doors);
                }
                else
                {
                    throw new ArgumentNullException();
                }

                m_CarDetails = new CarDetails(color, doors);
            }
            else
            {
                throw new ArgumentException("Car details allready initialize");
            }
        }

        public override List<string> GetVehicleMessages()
        {
            StringBuilder carMessegeToReturn = new StringBuilder();
            List<string> messagesToReturn = new List<string>();
            short choiseIndex = 0;

            carMessegeToReturn.AppendLine("Please choose color number from those options : ");
            foreach (CarDetails.eColor color in Enum.GetValues(typeof(CarDetails.eColor)))
            {
                carMessegeToReturn.AppendFormat("{0}.{1}{2}", choiseIndex++, color.ToString(), Environment.NewLine);
            }

            messagesToReturn.Add(carMessegeToReturn.ToString());
            carMessegeToReturn.Length = 0;
            choiseIndex = 0;
            carMessegeToReturn.AppendFormat("{0}Please choose the number of doors from thouse numbers options : {0}", Environment.NewLine);
            foreach (CarDetails.eDoors doors in Enum.GetValues(typeof(CarDetails.eDoors)))
            {
                carMessegeToReturn.AppendFormat("{0}.{1}{2}", choiseIndex++, doors.ToString(), Environment.NewLine);
            }

            messagesToReturn.Add(carMessegeToReturn.ToString());

            return messagesToReturn;
        }

        public override void UpdateVehicleWheels(string i_ManufacturerName, string i_CurrentAirPressure)
        {
            UpdateWheel(i_ManufacturerName, i_CurrentAirPressure, CarDetails.k_MaxCarAirPressure, CarDetails.k_CarWheels);                
        }
    }
}
