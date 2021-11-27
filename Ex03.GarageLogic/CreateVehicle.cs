using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public static class CreateVehicle
    {
        public enum eVehicleType
        {
            GasMotorbike,
            ElectricMotorbike,
            GasCar,
            ElectricCar,
            Truck,
            Vehicle   
        }

        public static Vehicle CreateVehicleByType(string i_LicenseNumber, eVehicleType i_VehicleType)
        {
            Vehicle vehicleToCreate;
            Vehicle.Engine vehicleEngine;

            switch (i_VehicleType)
            {
                case eVehicleType.GasMotorbike:
                    vehicleEngine = new VehicleGasEngine(MotorbikeDetails.k_MaxMotorFuelCapacity, MotorbikeDetails.k_MotorFuel);
                    vehicleToCreate = new Motorbike(i_LicenseNumber, vehicleEngine);
                    break;

                case eVehicleType.ElectricMotorbike:
                    vehicleEngine = new VehicleElectricEngine(MotorbikeDetails.k_MaxBatteryCapacity);
                    vehicleToCreate = new Motorbike(i_LicenseNumber, vehicleEngine);
                    break;

                case eVehicleType.GasCar:
                    vehicleEngine = new VehicleGasEngine(CarDetails.k_MaxCarFuelCapacity, CarDetails.k_CarFuel);
                    vehicleToCreate = new Car(i_LicenseNumber, vehicleEngine);
                    break;

                case eVehicleType.ElectricCar:
                    vehicleEngine = new VehicleElectricEngine(CarDetails.k_MaxCarBatteryCapacity);
                    vehicleToCreate = new Car(i_LicenseNumber, vehicleEngine);
                    break;

                case eVehicleType.Truck:
                    vehicleEngine = new VehicleGasEngine(TruckDetails.k_MaxTruckFuelCapacity, TruckDetails.k_TruckFuel);
                    vehicleToCreate = new Truck(i_LicenseNumber, vehicleEngine);
                    break;

                default:
                    throw new ArgumentException("Unvalid vehicle type");
            }

            return vehicleToCreate;
        }
    }
}
