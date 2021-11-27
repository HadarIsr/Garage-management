using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class GarageSystem
    {
        private const byte k_WheelsQuentionsQuantity = 2;
        private const byte k_EngineQuestionsQuantity = 1;
        private const byte k_FillFuelQuestionsQuantity = 2;
        private const byte k_FillBatteryQuestionsQuantity = 1;
        private const byte k_ModelNameQuestionsQuantity = 1;
        private const short k_MinutesInHour = 60;
        public const byte k_LicenseNumberMinLenght = 7;
        private readonly Dictionary<string, GarageVehicleDetails> m_AllGarageVehiclesByLicenseNumber;

        public GarageSystem()
        {
            m_AllGarageVehiclesByLicenseNumber = new Dictionary<string, GarageVehicleDetails>();
        }

        public bool IsEmptyDB
        {
            get
            {
                bool v_IfEmptyDB = false;

                v_IfEmptyDB = m_AllGarageVehiclesByLicenseNumber.Count == 0;

                return v_IfEmptyDB;
            }
        }

        public List<string> GetWheelsQuestions()
        {
            List<string> wheelsMessage = new List<string>(k_WheelsQuentionsQuantity);

            wheelsMessage.Add("Please enter wheel`s manufacturer");
            wheelsMessage.Add("Please enter wheel`s air pressure");

            return wheelsMessage;
        }

        public List<string> GetElectricEngineuestions()
        {
            List<string> engineMessage = new List<string>(k_EngineQuestionsQuantity);

            engineMessage.Add("Please enter the amount of minutes to add to the battery : ");

            return engineMessage;
        }

        public List<string> GetEngineQuestions()
        {
            List<string> engineMessage = new List<string>(k_EngineQuestionsQuantity);

            engineMessage.Add("Please enter the fuel litters\\battery hours current capacity");

            return engineMessage;
        }

        public string GetVehicleStatusQuestion()
        {
            short index = 0;
            StringBuilder strToReturn = new StringBuilder();

            strToReturn.AppendLine("Please choose one of those option to vehicle status by index number : ");
            foreach(GarageVehicleDetails.eVehicleStatus status in Enum.GetValues(typeof(GarageVehicleDetails.eVehicleStatus))) 
            {
                strToReturn.AppendFormat("{0}.{1}{2}", index++, status.ToString(), Environment.NewLine);
            }

            return strToReturn.ToString();
        }

        public bool CheckIfVehicleExistsByLicenseNumber(string i_LicenseToFind)
        {
            bool v_IfFound = false;

            if (!IsEmptyDB)
            {
                if (m_AllGarageVehiclesByLicenseNumber.ContainsKey(i_LicenseToFind))
                {
                    v_IfFound = true;
                }
            }

            return v_IfFound;
        }

        public void CreateNewVehicleInTheSyatem(string i_License, CreateVehicle.eVehicleType i_VehicleType)
        {
            Vehicle vehicleToCreate = null;

            if (!CheckIfVehicleExistsByLicenseNumber(i_License))
            {
                vehicleToCreate = CreateVehicle.CreateVehicleByType(i_License, i_VehicleType);
                m_AllGarageVehiclesByLicenseNumber.Add(i_License, new GarageVehicleDetails(vehicleToCreate));
            }         
        }
        
        public void ChangeStatusToInRepairByLicenseNumber(string i_LicenseNumber)
        {
            if (!IsEmptyDB)
            {
                ifLicenseNumberIsValid(i_LicenseNumber);
                if (CheckIfVehicleExistsByLicenseNumber(i_LicenseNumber))
                {
                    m_AllGarageVehiclesByLicenseNumber[i_LicenseNumber].Status = GarageVehicleDetails.eVehicleStatus.InRepair;
                }
                else
                {
                    throw new ArgumentException("License number is not exist in the system");
                }
            }
            else
            {
                throw new NullReferenceException("Garage data base empty");
            }
        }

        public string PrintAllLicenseNumber()
        {
            StringBuilder strToAppendLicense = new StringBuilder();

            strToAppendLicense.AppendLine("These are all the license numbers in the garage : ");
            foreach(KeyValuePair<string, GarageVehicleDetails> vehicle in m_AllGarageVehiclesByLicenseNumber)
            {
                strToAppendLicense.AppendLine(vehicle.Key.ToString());
            }

            return strToAppendLicense.ToString();
        }

        private void convertToEstatus(short i_StatusUserChoise, ref GarageVehicleDetails.eVehicleStatus io_Status)
        {
            bool v_IfConvertWasSuccess = false;

            foreach (GarageVehicleDetails.eVehicleStatus status in Enum.GetValues(typeof(GarageVehicleDetails.eVehicleStatus)))
            {
                if(i_StatusUserChoise == (short)status)
                {
                    v_IfConvertWasSuccess = true;
                    io_Status = status;
                    break;
                }
            }

            if(!v_IfConvertWasSuccess)
            {
                throw new ValueOutOfRangeException(0, Enum.GetNames(typeof(GarageVehicleDetails.eVehicleStatus)).Length - 1);
            }
        }

        public int CountVehicleByStatus(string i_Status)
        {
            GarageVehicleDetails.eVehicleStatus statusConverted = GarageVehicleDetails.eVehicleStatus.InRepair;
            short statusIndexChoise;
            int vehicleCounter = 0;

            if (!IsEmptyDB)
            {
                if (short.TryParse(i_Status, out statusIndexChoise))
                {
                    convertToEstatus(statusIndexChoise, ref statusConverted);
                    foreach (KeyValuePair<string, GarageVehicleDetails> vehicle in m_AllGarageVehiclesByLicenseNumber)
                    {
                        if (vehicle.Value.Status == statusConverted)
                        {
                            vehicleCounter++;
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("Input must be a number");
                }
            }
            else
            {
                throw new NullReferenceException("Garage data base empty");
            }

            return vehicleCounter;
        }

        public string PrintLicenseByStatus(string i_Status)
        {
            StringBuilder strToAppendLicense = new StringBuilder();
            GarageVehicleDetails.eVehicleStatus statusConverted = GarageVehicleDetails.eVehicleStatus.InRepair;
            short statusIndexChoise;

            if (!IsEmptyDB)
            {
                if (short.TryParse(i_Status, out statusIndexChoise))
                {
                    convertToEstatus(statusIndexChoise, ref statusConverted);
                    strToAppendLicense.AppendFormat("Vehicle by status : {0}, are :{1}", statusConverted, Environment.NewLine);
                    foreach (KeyValuePair<string, GarageVehicleDetails> vehicle in m_AllGarageVehiclesByLicenseNumber)
                    {
                        if (vehicle.Value.Status == statusConverted)
                        {
                            strToAppendLicense.AppendLine(vehicle.Key.ToString());
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("Input must be a number");
                }
            }
            else
            {
                throw new NullReferenceException("Grarage data base empty");
            }

            return strToAppendLicense.ToString();
        }

        public void ChangeStatusByLicense(string i_License, GarageVehicleDetails.eVehicleStatus i_NewStatus)
        {
             if (CheckIfVehicleExistsByLicenseNumber(i_License))
             {
                 m_AllGarageVehiclesByLicenseNumber[i_License].Status = i_NewStatus;
             }
             else
             {
                 throw new ArgumentException("The licensing number does not belong to any vehicle in the system");
             }
        }

        public void FillWheelsToMaxByLicenseNumber(string i_License)
        {
            if(CheckIfVehicleExistsByLicenseNumber(i_License))
            {
                m_AllGarageVehiclesByLicenseNumber[i_License].VehicleInGarage.FillAllWheelsToMaxPressure();
            }
            else
            {
                throw new ArgumentException("The licensing number does not belong to any vehicle in the system");
            }
        }

        public GarageVehicleDetails GetVehicleByLicenseNumber(string i_License)
        {
            GarageVehicleDetails vehicleToReturnByLicense = null;

            if(CheckIfVehicleExistsByLicenseNumber(i_License))
            {
                vehicleToReturnByLicense = m_AllGarageVehiclesByLicenseNumber[i_License];
            }
            else
            {
                throw new ArgumentException("The licensing number does not belong to any vehicle in the system");
            }

            return vehicleToReturnByLicense;
        }

        private void ifLicenseNumberIsValid(string i_LicenseNumberToCheck)
        {
            if (!string.IsNullOrEmpty(i_LicenseNumberToCheck))
            {
                if (i_LicenseNumberToCheck.Length >= k_LicenseNumberMinLenght)
                {
                    for (int index = 0; index < i_LicenseNumberToCheck.Length; index++)
                    {
                        if (!char.IsLetterOrDigit(i_LicenseNumberToCheck[index]))
                        {
                            throw new ArgumentException("Unvalid license input");
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("Unvalid license length");
                }
            }
            else
            {
                throw new NullReferenceException("LICENSE NUMBER IS EMPTY");
            }
        }

        internal void CheckTheFuelInputByTheVehicleType(string i_LicenseNumberToCheck, string i_FuelModelToCheck, string i_FuelLitersToAddToCheck)
        {
            VehicleGasEngine.eFuelModel fuelType = 0;
            float fuelLitersToAdd = 0;

            ifLicenseNumberIsValid(i_LicenseNumberToCheck);
            VehicleGasEngine.FindIfChoiseExistInEfuelType(i_FuelModelToCheck, ref fuelType);
            if (!float.TryParse(i_FuelLitersToAddToCheck, out fuelLitersToAdd))
            {
               throw new ArgumentException("Please insert  number in fuel quantity");
            }
         
            refuelGazVehicle(i_LicenseNumberToCheck, fuelType, fuelLitersToAdd);
        }

        private void checkIfVehicleTypeIsValidAndConvert(string i_StrInput, ref CreateVehicle.eVehicleType o_Type)
        {
            short typeAfterConvert;
            bool v_IfValidInput = false;

            if(short.TryParse(i_StrInput, out typeAfterConvert))
            {
                foreach(CreateVehicle.eVehicleType type in Enum.GetValues(typeof(CreateVehicle.eVehicleType)))
                {
                    if((short)type == typeAfterConvert)
                    {
                        o_Type = type;
                        v_IfValidInput = true;
                        break;
                    }
                }
            }

            if(!v_IfValidInput)
            {
                throw new ArgumentException("Unvalid vehicle type choise");
            }
        }

        public void UpdateVehicleOwnerDetailsByLicense(string i_License, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            if(!CheckIfVehicleExistsByLicenseNumber(i_License))
            {
                throw new ArgumentException("Vehicle licese don`t match in the system");
            }
            else if(m_AllGarageVehiclesByLicenseNumber[i_License].DoesOwnerDetailInit)
            {
                throw new ArgumentException("Owner Details allready initialized");
            }

            m_AllGarageVehiclesByLicenseNumber[i_License].UpdateOwnerDetails(i_OwnerName, i_OwnerPhoneNumber);
        }

        public void UpdateCurrentEnergyQuantityByVehicleLicese(string i_LicenseNumber, List<string> i_InputsToUpdate)
        {
            float capacityToUpdate;

            if(i_InputsToUpdate.Count != k_EngineQuestionsQuantity)
            {
                throw new ArgumentException("Engine initialize data quantity array is out of limit");
            }

            if(!float.TryParse(i_InputsToUpdate[0], out capacityToUpdate))
            {
                throw new ArgumentException("Current engenie capacity must be a number");
            }

            m_AllGarageVehiclesByLicenseNumber[i_LicenseNumber].VehicleInGarage.VehicleEngine.InitCurrentEnergyCapacity(capacityToUpdate);
        }

        public void ChargeElectricVehicle(string i_LicenseNumberToCheck, List<string> i_MinutesToChargeToCheck)
        {
            float minutesToCharge;

            if (i_MinutesToChargeToCheck.Count != k_FillBatteryQuestionsQuantity) 
            {
                throw new ArgumentException("Electric data quantity values unvalid");
            }

            ifLicenseNumberIsValid(i_LicenseNumberToCheck);
            if (!float.TryParse(i_MinutesToChargeToCheck[0], out minutesToCharge))
            {
                throw new ArgumentException("Battery charge quantity must be a number");
            }

            checkElectricChargeInutAndCharge(i_LicenseNumberToCheck, minutesToCharge);
        }

        private void checkElectricChargeInutAndCharge(string i_LicenseNumber, float i_MinutesToCharge)
        {
            Vehicle electricvehicleInGarage;
            float i_HourToCharge = i_MinutesToCharge / k_MinutesInHour;

            if (CheckIfVehicleExistsByLicenseNumber(i_LicenseNumber))
            {
                electricvehicleInGarage = m_AllGarageVehiclesByLicenseNumber[i_LicenseNumber].VehicleInGarage;
                electricvehicleInGarage.ChargeElectricEngine(i_HourToCharge);
            }
            else
            {
                throw new ArgumentException("The licensing number does not belong to any vehicle in the system");
            }
        }

        private void refuelGazVehicle(string i_LicenseNumber, VehicleGasEngine.eFuelModel i_FuelModel, float i_FuelLitersToAdd)
        {
            Vehicle gazvehicleInGarage;

            if (CheckIfVehicleExistsByLicenseNumber(i_LicenseNumber))
            {
                gazvehicleInGarage = m_AllGarageVehiclesByLicenseNumber[i_LicenseNumber].VehicleInGarage;
                gazvehicleInGarage.ChargeGazEngine(i_FuelLitersToAdd, i_FuelModel);
            }
            else
            {
                throw new ArgumentException("The licensing number does not belong to any vehicle in the system");
            }
        }

        public void UpdateWheelsDataByLicenseNumber(string i_LicenseNumber, List<string> i_WheelsInputs)
        {
            if(i_WheelsInputs.Count != k_WheelsQuentionsQuantity)
            {
                throw new ArgumentException("Inputs data quantity unvalid");
            }

            m_AllGarageVehiclesByLicenseNumber[i_LicenseNumber].VehicleInGarage.UpdateVehicleWheels(i_WheelsInputs[0], i_WheelsInputs[1]);
        }

        public List<string> GetVehicleQuestionByLicenseNumber(string i_LicenseNumber)
        {
            ifLicenseNumberIsValid(i_LicenseNumber);
            if(!CheckIfVehicleExistsByLicenseNumber(i_LicenseNumber))
            {
                throw new ArgumentException("License number dosen`t exist in the system");
            }

            return m_AllGarageVehiclesByLicenseNumber[i_LicenseNumber].VehicleInGarage.GetVehicleMessages();
        }

        public void UpdateTheVehicleDetails(string i_LicenseNumber, List<string> i_InputsFromUser)
        {
            ifLicenseNumberIsValid(i_LicenseNumber);
            if (!CheckIfVehicleExistsByLicenseNumber(i_LicenseNumber))
            {
                throw new ArgumentException("License number dosen`t exist in the system");
            }

            m_AllGarageVehiclesByLicenseNumber[i_LicenseNumber].VehicleInGarage.UpdateDetailsByVehicleType(i_InputsFromUser);
        }

        public void ChargeFuelByLicenseNumber(string i_LicenseNumber, List<string> i_InputsToFuel)
        {
            VehicleGasEngine.eFuelModel fuelType = 0;
            float fuelLitersToAdd = 0;

            if (i_InputsToFuel.Count == k_FillFuelQuestionsQuantity)
            {
                ifLicenseNumberIsValid(i_LicenseNumber);
                VehicleGasEngine.FindIfChoiseExistInEfuelType(i_InputsToFuel[0], ref fuelType);
                if (!float.TryParse(i_InputsToFuel[1], out fuelLitersToAdd))
                {
                    throw new ArgumentException("Fuel input must be a number");
                }

                refuelGazVehicle(i_LicenseNumber, fuelType, fuelLitersToAdd);
            }
            else
            {
                throw new ArgumentException("Unvalid inputs quantity to this vehicle type");
            }
        }

        private bool ifModelNameValid(string i_ModelName)
        {
            bool v_ValidName = true;

            if (string.IsNullOrEmpty(i_ModelName))
            {
                v_ValidName = false;
            }

            for (int index = 0; index < i_ModelName.Length && v_ValidName; index++)
            {
                if (!char.IsLetter(i_ModelName[index]) && (!char.IsWhiteSpace(i_ModelName[index])))
                {
                    v_ValidName = false;
                }
            }

            return v_ValidName;
        }

        public void InitVehicleModelName(string i_LicenseNumber, List<string> i_DataToUpdate)
        {
            if (i_DataToUpdate.Count == k_ModelNameQuestionsQuantity)
            {
                if (CheckIfVehicleExistsByLicenseNumber(i_LicenseNumber))
                {
                    if (ifModelNameValid(i_DataToUpdate[0]))
                    {
                        m_AllGarageVehiclesByLicenseNumber[i_LicenseNumber].VehicleInGarage.UpdateModelName(i_DataToUpdate[0]);
                    }
                    else
                    {
                        throw new ArgumentException("Unvalid model name");
                    }
                }
                else
                {
                    throw new ArgumentException("License number does not exist in the system");
                }
            }
            else
            {
                throw new ArgumentException("Unvalid inputs quantity to this vehicle type");
            }
        }

        public List<string> GetModelNameQuestions()
        {
            List<string> messageToReturn = new List<string>(k_ModelNameQuestionsQuantity);

            messageToReturn.Add("Please enter vehicle model name");

            return messageToReturn;
        }
    }
}
