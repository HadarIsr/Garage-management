using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class GarageVehicleDetails
    {
        public enum eVehicleStatus
        {
            InRepair,
            Repaired,
            Paid
        }

        private VehicleOwner m_Owner = null;
        private eVehicleStatus m_VehicleStatus;
        private Vehicle m_Vehicle;

        public GarageVehicleDetails(Vehicle i_Vehicle)
        {
            m_VehicleStatus = eVehicleStatus.InRepair;  ////insert defult value to vehicle in the garage
            m_Vehicle = i_Vehicle;
        }

        public bool DoesOwnerDetailInit
        {
            get
            {
                bool v_InitAnswer = false;

                v_InitAnswer = m_Owner != null;

                return v_InitAnswer;
            }
        }

        public string OwnerName
        {
            get
            {
                if (m_Owner != null)
                {
                    return m_Owner.Name;
                }
                else
                {
                    throw new NullReferenceException("Owner details initializaion error");
                }
            }
        }

        public string OwnerPhoneNnmber
        {
            get
            {
                if (m_Owner != null)
                {
                    return m_Owner.Phone;
                }
                else
                {
                    throw new NullReferenceException("Owner details initializaion error");
                }
            }

            set
            {
                if (m_Owner != null)
                {
                    m_Owner.Phone = value;
                }
                else
                {
                    throw new NullReferenceException("Owner details wasn`t initialize");
                }
            }
        }

        public eVehicleStatus Status
        {
            get
            {
                return m_VehicleStatus;
            }

            set
            {
                m_VehicleStatus = value;
            }
        }

        public Vehicle VehicleInGarage
        {
            get
            {
                return m_Vehicle;
            }
        }

        public void ChangeStatusToRepair()
        {
            m_VehicleStatus = eVehicleStatus.InRepair;
        }

        public override string ToString()
        {
            if(!DoesOwnerDetailInit)
            {
                throw new NullReferenceException("Details was not initialed");
            }

            string strToReturn = string.Format(
                "Owner name: {1}{0}{2}Vehicle status : {3}{0}{4}{0}",
                Environment.NewLine,
                OwnerName.ToString(),
                VehicleInGarage.ToString(),
                Status.ToString(),
                VehicleInGarage.VehicleEngine.ToString());

            return strToReturn;
        }

        private bool isContactNameValid(string i_NameToCheck)
        {
            bool v_ValidName = true;

            if (string.IsNullOrEmpty(i_NameToCheck))
            {
                v_ValidName = false;
            }

            for (int index = 0; index < i_NameToCheck.Length && v_ValidName; index++)
            {
                if (!char.IsLetter(i_NameToCheck[index]) && (!char.IsWhiteSpace(i_NameToCheck[index])))
                {
                    v_ValidName = false;
                }
            }

            return v_ValidName;
        }

        private bool isPhoneNumberValid(string i_PhoneToCheck)
        {
            bool v_ValidPhone = true;

            if (string.IsNullOrEmpty(i_PhoneToCheck))
            {
                v_ValidPhone = false;
            }

            for (int index = 0; index < i_PhoneToCheck.Length && v_ValidPhone; index++)
            {
                if (!char.IsDigit(i_PhoneToCheck[index]))
                {
                    v_ValidPhone = false;
                }
            }

            return v_ValidPhone;
        }

        public void UpdateOwnerDetails(string i_OnwerName, string i_PhoneNumber)
        {
            bool v_NameValide = false, v_PhoneValide = false;

            if (!DoesOwnerDetailInit)
            {
                v_NameValide = isContactNameValid(i_OnwerName);
                v_PhoneValide = isPhoneNumberValid(i_PhoneNumber);

                if (!v_NameValide || !v_PhoneValide)
                {
                    throw new ArgumentException("Unvalid owner details");
                }

                m_Owner = new VehicleOwner(i_OnwerName, i_PhoneNumber);
            }
            else
            {
                throw new ArgumentException("Vehicle owner details was initialized");
            }
        }
    }
}
