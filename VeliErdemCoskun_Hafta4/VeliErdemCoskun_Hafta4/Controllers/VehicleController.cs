using Microsoft.AspNetCore.Mvc;
using NHibernate.Mapping;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using VeliErdemCoskun_Hafta3.Context;
using VeliErdemCoskun_Hafta3.Models;

namespace VeliErdemCoskun_Hafta3.Controllers
{
    [ApiController]
    [Route("api/vehicle/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IMapperSession session;
        public VehicleController(IMapperSession session)
        {
            this.session = session;
        }

        //Gets all vehicles in saved database
        [HttpGet]
        public List<Vehicle> Get()
        {
            List<Vehicle> result = session.Vehicles.ToList();
            return result;
        }

        //Add a new vehicle
        [HttpPost]
        public void Post([FromBody] Vehicle vehicle)
        {
            try
            {
                session.BeginTransaction();
                session.Save(vehicle);
                session.Commit();
            }
            catch (Exception ex)
            {
                session.Rollback();
                Log.Error(ex, "Vehicle Insert Error!");
            }
            finally
            {
                session.CloseTransaction();
            }
        }

        //Updates the vehicle in database
        [HttpPut]
        public ActionResult<Vehicle> Put([FromBody] Vehicle request)
        {
            Vehicle vehicle = session.Vehicles.Where(x => x.Id == request.Id).FirstOrDefault();
            if (vehicle == null)
            {
                return NotFound();
            }
            try
            {
                session.BeginTransaction();

                vehicle.VehicleName = request.VehicleName;
                vehicle.VehiclePlate = request.VehiclePlate;


                session.Update(vehicle);

                session.Commit();
            }
            catch (Exception ex)
            {
                session.Rollback();
                Log.Error(ex, "Vehicle Update Error!");
            }
            finally
            {
                session.CloseTransaction();
            }


            return Ok();
        }

        //Delete existing vehicle
        [HttpDelete("{id}")]
        public ActionResult<Vehicle> Delete(long id)
        {
            Vehicle vehicle = session.Vehicles.Where(x => x.Id == id).FirstOrDefault();
            if (vehicle == null)
            {
                return NotFound();
            }

            try
            {
                session.BeginTransaction();
                session.Delete(vehicle);
                session.Commit();
            }
            catch (Exception ex)
            {
                session.Rollback();
                Log.Error(ex, "Container Insert Error");
            }
            finally
            {
                session.CloseTransaction();
            }

            return Ok();
        }
    }
}

