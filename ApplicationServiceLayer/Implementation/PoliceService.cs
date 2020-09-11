// <copyright file="PoliceService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApplicationServiceLayer
{
    using ApplicationModelLayer;
    using ApplicationRepositoryLayer;

    /// <summary>
    /// Service Layer for Owner.
    /// </summary>
    public class PoliceService : IPoliceService
    {
        private readonly IParkingLotRepository parkingLotRepository;

        public PoliceService(IParkingLotRepository parkingLotRepository)
        {
            this.parkingLotRepository = parkingLotRepository;
        }

        public Parking FindVehicleBySlotNumber(int slotNumber)
        {
            return this.parkingLotRepository.FindVehicleBySlotNumber(slotNumber);
        }

        public Parking FindVehicleByVehicleNumber(string vehicleNumber)
        {
            return this.parkingLotRepository.FindVehicleByVehicleNumber(vehicleNumber);
        }

        public Parking ParkVehicle(VehicleDetails parking)
        {
            return this.parkingLotRepository.AddVehicleToParking(parking);
        }

        public Parking UnParkVehicle(int slotNumber)
        {
            return this.parkingLotRepository.UnParkVehicle(slotNumber);
        }
    }
}
