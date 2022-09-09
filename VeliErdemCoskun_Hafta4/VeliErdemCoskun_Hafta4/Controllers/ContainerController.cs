using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using VeliErdemCoskun_Hafta3.Context;
using VeliErdemCoskun_Hafta3.Models;

namespace VeliErdemCoskun_Hafta3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        private readonly IMapperSession session;

        public ContainerController(IMapperSession session)
        {
            this.session = session;
        }

        // Get all containers
        [HttpGet]
        public IEnumerable<Container> Get()
        {
            return session.Containers;
        }

        //Get containers by vehicle id
        [HttpGet("GetContainerByVehicleId")]
        public List<Container> GetContainerByVehicleId(int id)
        {
            List<Container> result = session.Containers.Where(x => x.VehicleId == id).ToList();
            return result;
        }

        //Get method that aggregates the vehicleid list as many as n entries and responds
        [HttpGet("ClusteringByVehicleId")]
        public IActionResult GetByNContainer(int vehicleid, int n)
        {

            List<Container> container = session.Containers.Where(x => x.VehicleId == vehicleid).ToList();

            IEnumerable<IEnumerable<Container>> parts = container.Split(n);

            return Ok(parts);

        }

        //Post method for add a new container
        [HttpPost]
        public void Post([FromBody] Container container)
        {
            try
            {
                session.BeginTransaction();
                session.Save(container);
                session.Commit();
            }
            catch (Exception ex)
            {
                session.Rollback();
                Log.Error(ex, "Container Insert Error!");
            }
            finally
            {
                session.CloseTransaction();
            }
        }

        //Put method for uptading the data of container
        [HttpPut]
        public ActionResult<Container> Put([FromBody] Container request)
        {
            Container container = session.Containers.Where(x => x.Id == request.Id).FirstOrDefault();
            if (container == null)
            {
                return NotFound();
            }

            try
            {
                session.BeginTransaction();
                //Update all attribute but not vehicle id
                container.ContainerName = request.ContainerName;
                container.Longitude = request.Longitude;
                container.Latitude = request.Latitude;



                session.Update(container);

                session.Commit();
            }
            catch (Exception ex)
            {
                session.Rollback();
                Log.Error(ex, "Container Update Error!");
            }
            finally
            {
                session.CloseTransaction();
            }


            return Ok();
        }

        //Delete existing contanier
        [HttpDelete("{id}")]
        public ActionResult<Container> Delete(int id)
        {
            Container container = session.Containers.Where(x => x.Id == id).FirstOrDefault();
            if (container == null)
            {
                return NotFound();
            }

            try
            {
                session.BeginTransaction();
                session.Delete(container);
                session.Commit();
            }
            catch (Exception ex)
            {
                session.Rollback();
                Log.Error(ex, "Container Delete Error!");
            }
            finally
            {
                session.CloseTransaction();
            }

            return Ok();
        }
    }
}