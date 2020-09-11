// <copyright file="VehicleDetails.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApplicationModelLayer
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Parking Model for Parking Table.
    /// </summary>
    public class VehicleDetails
    {
        /// <summary>
        /// Gets or sets vehicle Number.
        /// </summary>
        [Required(ErrorMessage = "Vehicle Number is requrired is required")]
        public string VehicleNumber { get; set; }

        /// <summary>
        /// Gets or sets Parking Type.
        /// </summary>
        [Required(ErrorMessage = "Parking type is required")]
        [RegularExpression(@"^[1-9]{1,}$", ErrorMessage = "Please enter a valid Parking type")]
        public int ParkingType { get; set; }

        /// <summary>
        /// Gets or sets Driver Type.
        /// </summary>
        [Required(ErrorMessage = "Driver type is required")]
        [RegularExpression(@"^[1-9]{1,}$", ErrorMessage = "Please enter a valid Driver type")]
        public int DriverType { get; set; }

        /// <summary>
        /// Gets or sets Vehicle Type.
        /// </summary>
        [Required(ErrorMessage = "Vehicle type is required")]
        [RegularExpression(@"^[1-9]{1,}$", ErrorMessage = "Please enter a valid vehicle type")]
        public int VehicleType { get; set; }

        /// <summary>
        /// Gets or sets Slot Number.
        /// </summary>
        [Required(ErrorMessage = "Slot Number is required")]
        [RegularExpression(@"^[0-9]{1,}$", ErrorMessage = "Please enter a valid slot number")]
        public int SlotNumber { get; set; }
    }
}
