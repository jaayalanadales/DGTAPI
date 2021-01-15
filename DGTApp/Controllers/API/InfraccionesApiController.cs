using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DGTApp.Controllers.API
{
    [Route("infracciones")]
    public class InfraccionesApiController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post(JObject data)
        {
            using (Model1Container context = new Model1Container())
            {
                string matriculaVehiculo = data["Matricula"].ToString();
                string dniConductor = data.ContainsKey("Dni") ? data["Dni"].ToString() : null;
                int tipoInfraccion = data["Tipo"].ToObject<int>();
                int puntosADescontar = context.TiposInfraccion.Find(tipoInfraccion).Puntos;
                DateTime fecha = data["Fecha"].ToObject<DateTime>();

                if(context.TiposInfraccion.Find(tipoInfraccion) == null) return BadRequest("No existe el tipo de infracción.")

                if (dniConductor != null)
                {
                    context.Infracciones.Add(new Infraccion { Conductor = context.Conductores.Find(dniConductor), Tipo = tipoInfraccion, Fecha = fecha, ConductorDni = dniConductor });
                    
                }
                else
                {
                    Vehiculo vehiculo = context.Vehiculos.Find(matriculaVehiculo);
                    if (vehiculo.Conductor.Count() == 1)
                    {
                        dniConductor = vehiculo.Conductor.First().Dni;
                        context.Infracciones.Add(new Infraccion { Conductor = context.Conductores.Find(dniConductor), Tipo = tipoInfraccion, Fecha = fecha, ConductorDni = dniConductor });
                    }
                    else
                    {
                        return BadRequest("El vehículo " + matriculaVehiculo + " tiene más de un conductor asignado.");
                    }
                }
                
                if(dniConductor != null)
                {
                    var puntosConductor = context.Conductores.Find(dniConductor).Puntos;
                    puntosConductor -= puntosADescontar;
                    if (puntosConductor < 0) puntosConductor = 0;
                    context.Conductores.Find(dniConductor).Puntos = puntosConductor;
                    
                }
                

                context.SaveChanges();

                return Ok("Infracción añadida al vehículo " + matriculaVehiculo + " y conductor con DNI " + dniConductor);


            }
        }

        [HttpGet]

        public IHttpActionResult Get()
        {
            Model1Container context = new Model1Container();

            var infraccionesMasFrecuentes = context.Infracciones.GroupBy(i => i.Tipo).OrderByDescending(gp => gp.Count()).Take(5).Select(g => g.Key).ToList();
            List<TipoInfraccion> tiposInfraccion = context.TiposInfraccion.Where(t => infraccionesMasFrecuentes.Contains(t.Id)).ToList();

            return Json(tiposInfraccion, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,

            });
        }
    }
}
