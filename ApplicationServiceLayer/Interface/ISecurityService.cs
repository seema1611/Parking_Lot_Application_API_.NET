// <copyright file="ISecurityService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApplicationServiceLayer
{
    using ApplicationModelLayer;

    /// <summary>
    /// Interface for Owner service.
    /// </summary>
    public interface ISecurityService
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
    }
}
