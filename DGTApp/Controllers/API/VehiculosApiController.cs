using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DGTApp.Controllers.API
{
    [Route("vehiculos")]
    public class VehiculosApiController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post(JObject data)
        {
            using (Model1Container context = new Model1Container())
            {
                string matriculaVehiculo = data["Matricula"].ToString();

                if (context.Vehiculos.Find(matriculaVehiculo) != null)
                    return BadRequest("Ya existe un vehículo con la matrícula " + matriculaVehiculo);

                Vehiculo vehiculo = new Vehiculo
                {
                    Matricula = matriculaVehiculo,
                    Marca = data["Marca"].ToString(),
                    Modelo = data["Modelo"].ToString()
                };

                context.Vehiculos.Add(vehiculo);

                context.SaveChanges();

                return Ok("Vehículo añadido satisfactoriamente");
            }
        }       

        
    }
}
