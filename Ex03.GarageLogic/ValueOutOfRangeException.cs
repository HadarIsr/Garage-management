using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MinValue;
        private float m_MaxValue;

        public ValueOutOfRangeException(float i_MinValue, float i_MaxVlue) :
            base(string.Format("An error occured rang must be between {0} to {1}", i_MinValue, i_MaxVlue))
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxVlue;
        }

        public float Min
        {
            get
            {
                return m_MinValue;
            }
        }

        public float Max
        {
            get
            {
                return m_MaxValue;
            }
        }
    }
}
