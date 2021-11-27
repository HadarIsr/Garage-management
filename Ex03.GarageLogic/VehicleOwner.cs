using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class VehicleOwner
    {
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;

        internal VehicleOwner(string i_Name, string i_PhoneNumber)
        {
            m_OwnerName = i_Name;
            m_OwnerPhoneNumber = i_PhoneNumber;
        }

        internal string Name
        {
            get
            {
                return m_OwnerName;
            }
        }

        internal string Phone
        {
            get
            {
                return m_OwnerPhoneNumber;
            }

            set
            {
                m_OwnerPhoneNumber = value;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", Name, Environment.NewLine);
        }
    }
}
