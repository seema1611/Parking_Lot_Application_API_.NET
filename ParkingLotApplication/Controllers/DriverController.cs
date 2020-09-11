// <copyright file="DriverController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace ParkingLotApplication.Controllers
{
    using System;
    using System.Net;
    using ApplicationBussinessLayer.Implementation;
    using ApplicationModelLayer;
    using ApplicationServiceLayer;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller for Police.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        public readonly IDriverService driverService;
        public Parking parkingDetails;

        public DriverController(IDriverService driverService)
        {
            this.driverService = driverService;
        }

        [Route("park")]
        [HttpPost]
        public ActionResult Park([FromBody] VehicleDetails vehicleDetails)
        {
            try
            {
                this.parkingDetails = driverService.ParkVehicle(vehicleDetails);
                MessageSenderService.AddMessageToQueue("Vehicle Number " + this.parkingDetails.VehicleNumber + " has been Parked at Slot Id " + this.parkingDetails.SlotNumber + " at time: " + this.parkingDetails.EntryTime);
            }
            catch (Exception e)
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, e.Message));
            }

            return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Vehicle Parked Successfully", this.parkingDetails));
        }

        [Route("unpark")]
        [HttpPut]
        public ActionResult Unpark(int slotNumber)
        {
            try
            {
                this.parkingDetails = this.driverService.UnParkVehicle(slotNumber);
                MessageSenderService.AddMessageToQueue("Vehicle Number " + this.parkingDetails.VehicleNumber + " has been UnParked at Slot Id " + this.parkingDetails.SlotNumber);
            }
            catch (Exception e)
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, e.Message));
            }

            return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Vehicle Unparked Successfully", this.parkingDetails));
        }
    }
}
