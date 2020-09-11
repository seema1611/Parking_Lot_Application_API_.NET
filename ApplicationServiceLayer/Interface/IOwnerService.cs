// <copyright file="IOwnerService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace ApplicationServiceLayer
{
    using System.Collections.Generic;
    using ApplicationModelLayer;

    /// <summary>
    /// Interface for Owner service.
    /// </summary>
    public interface IOwnerService
    {
        /// <summary>
        /// Method to add vehicle to Parking.
        /// </summary>
        /// <param name="parking"></param>
        /// <returns></returns>
        Parking ParkVehicle(VehicleDetails parking);

        /// <summary>
        /// Method to remove vehicle from Parking.
        /// </summary>
        /// <param name="slotId"></param>
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
        /// Method to Find All Vehicle from Parking.
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
        /// Method to Find Empty slots from Parking.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Parking> GetAllEmptySlots();
    }
}
