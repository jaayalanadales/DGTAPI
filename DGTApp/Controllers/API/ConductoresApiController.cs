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
    [Route("api/ConductoresAPI/{action}", Name = "ConductoresAPI")]
    public class ConductoresApiController : ApiController
    {
        [HttpGet]

        public IHttpActionResult Get()
        {
            using (Model1Container context = new Model1Container())
            {
                List<Conductor> conductores = context.Conductores.ToList();

                return Json(conductores, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    
                });
            }


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
