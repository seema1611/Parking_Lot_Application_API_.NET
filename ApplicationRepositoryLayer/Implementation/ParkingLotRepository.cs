// <copyright file="ParkingLotRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace ApplicationRepositoryLayer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using ApplicationModelLayer;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Parking Lot Repository.
    /// </summary>
    public class ParkingLotRepository : IParkingLotRepository
    {
        private readonly string connectionString;
        private readonly SqlConnection con;
        private readonly IConfiguration configuration;

        public ParkingLotRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.connectionString = configuration.GetSection("ConnectionStrings").GetSection("ParkingLotDBConnection").Value;
            this.con = new SqlConnection(this.connectionString);
        }

        /// <summary>
        /// To Add vehicle to Parking.
        /// </summary>
        /// <param name="vehicleDetails"></param>
        /// <returns></returns>
        public Parking AddVehicleToParking(VehicleDetails vehicleDetails)
        {
            try
            {
                using (this.con)
                {
                    SqlCommand cmd = new SqlCommand("spPark", this.con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@VehicleNumber", vehicleDetails.VehicleNumber);
                    cmd.Parameters.AddWithValue("@ParkingType", vehicleDetails.ParkingType);
                    cmd.Parameters.AddWithValue("@DriverType", vehicleDetails.DriverType);
                    cmd.Parameters.AddWithValue("@VehicleType", vehicleDetails.VehicleType);
                    cmd.Parameters.AddWithValue("@SlotNumber", vehicleDetails.SlotNumber);

                    this.con.Open();
                    var result = cmd.ExecuteNonQuery();
                    this.con.Close();
                    return this.FindVehicleByVehicleNumber(vehicleDetails.VehicleNumber);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Parking FindVehicleByVehicleNumber(string vehicleNumber)
        {
            Parking parking = new Parking();
            try
            {
                using (this.con)
                {
                    SqlCommand cmd = new SqlCommand("spSearchVehicleNumber", this.con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@VehicleNumber", vehicleNumber);
                    this.con.Open();
                    var result = cmd.ExecuteReader();
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            parking.ParkingId = Convert.ToInt32(result["PARKING_ID"]);
                            parking.ParkingType = Convert.ToInt32(result["PARKING_TYPE"]);
                            parking.VehicleNumber = result["VEHICLE_NUMBER"].ToString();
                            parking.VehicleType = Convert.ToInt32(result["VEHICLE_TYPE"]);
                            parking.DriverType = Convert.ToInt32(result["DRIVER_TYPE"]);
                            parking.EntryTime = result["ENTRY_TIME"].ToString();
                            parking.ExitTime = (result["EXIT_TIME"] is null) ? "NULL" : result["EXIT_TIME"].ToString();
                            parking.ParkingCharge = (result["CHARGE"] is 0) ? 0 : Convert.ToInt32(result["CHARGE"]);
                            parking.SlotNumber = Convert.ToInt32(result["SLOT_NUMBER"]);
                        }
                    }

                    this.con.Close();
                }

                return parking;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }

        public Parking UnParkVehicle(int slotNumber)
        {
            try
            {
                using (this.con)
                {
                    SqlCommand cmd = new SqlCommand("spUnpark", this.con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SlotNumber", slotNumber);

                    this.con.Open();
                    int result = cmd.ExecuteNonQuery();
                    this.con.Close();
                    return this.FindVehicleBySlotNumber(slotNumber);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Parking FindVehicleBySlotNumber(int slotNumber)
        {
            Parking parking = new Parking();
            try
            {
                using (this.con)
                {
                    SqlCommand cmd = new SqlCommand("spSearchSlotID", this.con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SlotNumber", slotNumber);
                    this.con.Open();
                    var result = cmd.ExecuteReader();
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            parking.ParkingId = Convert.ToInt32(result["PARKING_ID"]);
                            parking.ParkingType = Convert.ToInt32(result["PARKING_TYPE"]);
                            parking.VehicleNumber = result["VEHICLE_NUMBER"].ToString();
                            parking.VehicleType = Convert.ToInt32(result["VEHICLE_TYPE"]);
                            parking.DriverType = Convert.ToInt32(result["DRIVER_TYPE"]);
                            parking.EntryTime = result["ENTRY_TIME"].ToString();
                            parking.ExitTime = (result["EXIT_TIME"] is null) ? "NULL" : result["EXIT_TIME"].ToString();
                            parking.ParkingCharge = Convert.ToInt32(result["CHARGE"]);
                            parking.SlotNumber = Convert.ToInt32(result["SLOT_NUMBER"]);
                        }
                    }

                    this.con.Close();
                }

                return parking;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }

        public List<Parking> GetAllVehicles()
        {
            List<Parking> parkedVehicleList = new List<Parking>();
            try
            {
                using (this.con)
                {
                    SqlCommand cmd = new SqlCommand("spGetAllParkingData", this.con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    this.con.Open();
                    var result = cmd.ExecuteReader();
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            Parking parking = new Parking();
                            parking.ParkingId = Convert.ToInt32(result["PARKING_ID"]);
                            parking.ParkingType = Convert.ToInt32(result["PARKING_TYPE"]);
                            parking.VehicleNumber = result["VEHICLE_NUMBER"].ToString();
                            parking.VehicleType = Convert.ToInt32(result["VEHICLE_TYPE"]);
                            parking.DriverType = Convert.ToInt32(result["DRIVER_TYPE"]);
                            parking.EntryTime = result["ENTRY_TIME"].ToString();
                            parking.ExitTime = (result["EXIT_TIME"] is null) ? "NULL" : result["EXIT_TIME"].ToString();
                            parking.ParkingCharge = Convert.ToInt32(result["CHARGE"]);
                            parking.SlotNumber = Convert.ToInt32(result["SLOT_NUMBER"]);
                            parkedVehicleList.Add(parking);
                        }
                    }

                    this.con.Close();
                }

                return parkedVehicleList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }

        public bool DeleteVehicleByParkingId(int parkingId)
        {
            try
            {
                using (this.con)
                {
                    SqlCommand cmd = new SqlCommand("spDeleteParkingData", this.con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ParkingId", parkingId);

                    this.con.Open();
                    int result = cmd.ExecuteNonQuery();
                    this.con.Close();

                    if (result != 0)
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.con.Close();
            }
        }

        public IEnumerable<Parking> GetAllEmptySlots()
        {
            try
            {
                List<Parking> listEmptySlots = new List<Parking>();
                using (this.con)
                {
                    SqlCommand cmd = new SqlCommand("spGetEmptySLots", this.con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    this.con.Open();
                    var result = cmd.ExecuteReader();
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            Parking parking = new Parking();
                            parking.SlotNumber = Convert.ToInt32(result["SLOT_NUMBER"]);
                            listEmptySlots.Add(parking);
                        }
                    }

                    this.con.Close();
                    return listEmptySlots;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
