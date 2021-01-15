using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DGTApp.Controllers.API
{
    [Route("api/VehiculosAPI/{action}", Name = "VehiculosAPI")]
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


        [HttpPost]
        public IHttpActionResult AddConductorVehiculo(JObject data)
        {
            using (Model1Container context = new Model1Container())
            {
                string matriculaVehiculo = data["Matricula"].ToString();
                string dniConductor = data["Dni"].ToString();

                Vehiculo vehiculo = context.Vehiculos.Find(matriculaVehiculo);
                Conductor conductor = context.Conductores.Find(dniConductor);

                if(vehiculo.Conductor.Count() < 5)
                {
                    vehiculo.Conductor.Add(conductor);

                    context.SaveChanges();

                    return Ok();
                    
                }

                return BadRequest();


            }
        }

        [HttpPost]
        public IHttpActionResult AddInfraccion(JObject data)
        {
            using (Model1Container context = new Model1Container())
            {
                string matriculaVehiculo = data["Matricula"].ToString();
                string dniConductor = data.ContainsKey("Dni") ? data["Dni"].ToString() : null;
                int tipoInfraccion = data["Tipo"].ToObject<int>();
                DateTime fecha = data["Fecha"].ToObject<DateTime>();

                if(dniConductor != null)
                {
                    context.Infracciones.Add(new Infraccion { Dni = dniConductor, Tipo = tipoInfraccion });
                }
                else
                {
                    Vehiculo vehiculo = context.Vehiculos.Find(matriculaVehiculo);
                    if(vehiculo.Conductor.Count() == 1)
                    {
                        context.Infracciones.Add(new Infraccion { Dni = vehiculo.Conductor.First().Dni, Tipo = tipoInfraccion, Fecha = fecha });
                    }
                }

                context.SaveChanges();
            
                return Ok();


            }
        }
    }
}
