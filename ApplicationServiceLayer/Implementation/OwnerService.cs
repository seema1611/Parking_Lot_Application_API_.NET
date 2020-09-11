// <copyright file="OwnerService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace ApplicationServiceLayer
{
    using System.Collections.Generic;
    using ApplicationModelLayer;
    using ApplicationRepositoryLayer;

    /// <summary>
    /// Service Layer for Owner.
    /// </summary>
    public class OwnerService : IOwnerService
    {
        private readonly IParkingLotRepository parkingLotRepository;

        public OwnerService(IParkingLotRepository parkingLotRepository)
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

        public List<Parking> GetAllVehicles()
        {
            return this.parkingLotRepository.GetAllVehicles();
        }

        public Parking ParkVehicle(VehicleDetails parking)
        {
            return this.parkingLotRepository.AddVehicleToParking(parking);
        }

        public Parking UnParkVehicle(int slotNumber)
        {
            return this.parkingLotRepository.UnParkVehicle(slotNumber);
        }

        public bool DeleteVehicleByParkingId(int parkingId)
        {
            return this.parkingLotRepository.DeleteVehicleByParkingId(parkingId);
        }

        public IEnumerable<Parking> GetAllEmptySlots()
        {
            return this.parkingLotRepository.GetAllEmptySlots();
        }
    }
}
