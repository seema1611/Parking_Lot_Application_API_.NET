// <copyright file="SecurityController.cs" company="PlaceholderCompany">
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
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService securityService;

        public SecurityController(ISecurityService securityService)
        {
            this.securityService = securityService;
        }

        [Route("park")]
        [HttpPost]
        public ActionResult Park([FromBody] VehicleDetails vehicleDetails)
        {
            Parking parkingDetails;
            try
            {
                parkingDetails = this.securityService.ParkVehicle(vehicleDetails);
                MessageSenderService.AddMessageToQueue("Vehicle Number " + parkingDetails.VehicleNumber + " has been Parked at Slot Id " + parkingDetails.SlotNumber + " at time: " + parkingDetails.EntryTime);
            }
            catch (Exception e)
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, e.Message));
            }

            return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Vehicle Parked Successfully", parkingDetails));
        }

        [Route("unpark")]
        [HttpPut]
        public ActionResult Unpark(int slotNumber)
        {
            Parking parkingDetails;
            try
            {
                parkingDetails = this.securityService.UnParkVehicle(slotNumber);
                MessageSenderService.AddMessageToQueue("Vehicle Number " + parkingDetails.VehicleNumber + " has been UnParked at Slot Id " + parkingDetails.SlotNumber);
            }
            catch (Exception e)
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, e.Message));
            }

            return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Vehicle Unparked Successfully", parkingDetails));
        }

        [Route("search/&vehicle={vehicleNumber}")]
        [HttpGet]
        public ActionResult FindVehicleByVehicleNumber(string vehicleNumber)
        {
            Parking parkingDetails;
            try
            {
                parkingDetails = this.securityService.FindVehicleByVehicleNumber(vehicleNumber);
            }
            catch (Exception e)
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.NotFound, e.Message));
            }

            return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Vehicle Found Successfully", parkingDetails));
        }

        [Route("search/{slotNumber}")]
        [HttpGet]
        public ActionResult FindVehicleBySlotId(int slotNumber)
        {
            Parking parkingDetails;
            try
            {
                parkingDetails = this.securityService.FindVehicleBySlotNumber(slotNumber);
            }
            catch (Exception e)
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.NotFound, e.Message));
            }

            return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Vehicle Found Successfully", parkingDetails));
        }
    }
}
