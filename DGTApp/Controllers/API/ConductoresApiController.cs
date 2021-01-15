using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DGTApp.Controllers.API
{
    [Route("conductores")]
    public class ConductoresApiController : ApiController
    {

        [HttpGet]
        [Route("conductores/{dni}/puntos")]
        public IHttpActionResult GetPuntosConductor(string dni)
        {
            using (Model1Container context = new Model1Container())
            {
                Conductor conductor = context.Conductores.Find(dni);
                int puntosConductor = conductor.Puntos;

                return Ok("El conductor " + dni + " tiene " + puntosConductor + " puntos.");
            }
        }

        [HttpGet]
        [Route("conductores/{dni}/infracciones")]
        public IHttpActionResult GetInfraccionesConductor(string dni)
        {
            Model1Container context = new Model1Container();
            List<Infraccion> infraccionesConductor = context.Conductores.Find(dni).Infraccion.ToList();
                
            return Json(infraccionesConductor, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,

            });
        }

        [HttpPost]
        public IHttpActionResult Post(JObject data)
        {
            using (Model1Container context = new Model1Container())
            {
                string dniConductor = data["Dni"].ToString();

                if (context.Conductores.Find(dniConductor) != null)
                    return BadRequest("Ya existe un conductor con DNI = " + dniConductor);

                Conductor conductor = new Conductor
                {
                    Nombre = data["Nombre"].ToString(),
                    Apellidos = data["Apellidos"].ToString(),
                    Dni = data["Dni"].ToString(),
                    Puntos = data["Puntos"].ToObject<int>()
                };

                context.Conductores.Add(conductor);

                context.SaveChanges();

                return Ok("Conductor añadido satisfactoriamente");
            }
        }
    }
}
