// <copyright file="IParkingLotRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace ApplicationRepositoryLayer
{
    using ApplicationModelLayer;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for Parking Lot Repository.
    /// </summary>
    public interface IParkingLotRepository
    {
        /// <summary>
        /// Method to add vehicle to Parking.
        /// </summary>
        /// <param name="parking"></param>
        /// <returns></returns>
        Parking AddVehicleToParking(VehicleDetails parking);

        /// <summary>
        /// Method to remove vehicle from Parking.
        /// </summary>
        /// <param name="slotNumber"></param>
        /// <returns></returns>
        Parking UnParkVehicle(int slotNumber);

        /// <summary>
        /// Method to Find vehicle from Parking By Vehicle Number.
        /// </summary>
        /// <param name="vehicleNumber"></param>
        /// <returns></returns>
        public Parking FindVehicleByVehicleNumber(string vehicleNumber);

        /// <summary>
        /// Method to Find vehicle from Parking By slotNumber.
        /// </summary>
        /// <param name="slotNumber"></param>
        /// <returns></returns>
        public Parking FindVehicleBySlotNumber(int slotNumber);

        /// <summary>
        /// Method to Get All vehicle from Parking.
        /// </summary>
        /// <returns></returns>
        public List<Parking> GetAllVehicles();

        /// <summary>
        /// Method to delete vehicle from Parking By parking id.
        /// </summary>
        /// <param name="parkingId"></param>
        /// <returns></returns>
        public bool DeleteVehicleByParkingId(int parkingId);

        /// <summary>
        /// Method to Get All Empty slots from Parking.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Parking> GetAllEmptySlots();
    }
}
