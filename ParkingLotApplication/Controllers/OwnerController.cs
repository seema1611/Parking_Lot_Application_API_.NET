// <copyright file="OwnerController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace ParkingLotApplication.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using ApplicationBussinessLayer.Implementation;
    using ApplicationModelLayer;
    using ApplicationServiceLayer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller for Owner.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService ownerService;
        private readonly object parkingDetails;
        private readonly object getAllEmptySlots;

        public OwnerController(IOwnerService ownerService)
        {
            this.ownerService = ownerService;
        }

        [Route("park")]
        [HttpPost]
        public ActionResult Park([FromBody] VehicleDetails vehicleDetails)
        {
            Parking parkingDetails;
            try
            {
                parkingDetails = this.ownerService.ParkVehicle(vehicleDetails);
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
                parkingDetails = this.ownerService.UnParkVehicle(slotNumber);
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
                parkingDetails = this.ownerService.FindVehicleByVehicleNumber(vehicleNumber);
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
                parkingDetails = this.ownerService.FindVehicleBySlotNumber(slotNumber);
            }
            catch (Exception e)
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.NotFound, e.Message));
            }

            return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Vehicle Found Successfully", parkingDetails));
        }

        [Route("search/getallvehicles")]
        [HttpGet]
        public ActionResult GetAllVehicles()
        {
            List<Parking> parkingDetails;
            try
            {
                parkingDetails = this.ownerService.GetAllVehicles();
            }
            catch (Exception e)
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.NotFound, e.Message));
            }

            return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Vehicle Data Fetched Successfully", parkingDetails));
        }

        [Route("delete/{parkingId}")]
        [HttpDelete]
        public ActionResult DeleteVehicleByParkingId(int parkingId)
        {
            try
            {
                bool parkingDetails = this.ownerService.DeleteVehicleByParkingId(parkingId);
            }
            catch (Exception e)
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.NotFound, e.Message));
            }

            return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Vehicle delete Successfully", this.parkingDetails));
        }

        [Route("getallEmptySlots")]
        [HttpGet]
        public ActionResult GetAllEmptySlots()
        {
            try
            {
                var getAllEmptySlots = this.ownerService.GetAllEmptySlots();
                if (getAllEmptySlots == null)
                {
                    return this.NotFound(new ResponseEntity(HttpStatusCode.NotFound, "No Recods Found", null));
                }

                return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Empty Slots Data Fetched Successfully", getAllEmptySlots));
            }
            catch (Exception e)
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.NotFound, e.Message));
            }
        }
    }
}
