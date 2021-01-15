using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DGTApp.Controllers.API
{
    [Route("vehiculos/conductores")]
    public class ConductoresVehiculosApiController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post(JObject data)
        {
            using (Model1Container context = new Model1Container())
            {
                string matriculaVehiculo = data["Matricula"].ToString();
                string dniConductor = data["Dni"].ToString();

                Vehiculo vehiculo = context.Vehiculos.Find(matriculaVehiculo);
                Conductor conductor = context.Conductores.Find(dniConductor);

                if (vehiculo.Conductor.Count() < 5)
                {
                    vehiculo.Conductor.Add(conductor);

                    context.SaveChanges();

                    return Ok("Vehículo añadido satisfactoriamente.");

                }

                return BadRequest("Ya existe un vehiculo con matricula " + matriculaVehiculo);


            }
        }
    }
}
