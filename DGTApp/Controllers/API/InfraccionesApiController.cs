using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DGTApp.Controllers.API
{
    [Route("api/InfraccionAPI/{action}", Name = "InfraccionAPI")]
    public class InfraccionesApiController : ApiController
    {
        [HttpPost]
        public IHttpActionResult AddTipoInfraccion(JObject data)
        {
            using (Model1Container context = new Model1Container())
            {
                string descripcion = data["Descripcion"].ToString();
                int puntosDescontar = data["Puntos"].ToObject<int>();

                context.TiposInfraccion.Add(new TipoInfraccion() { Descripcion = descripcion, Puntos = puntosDescontar });

                context.SaveChanges();

                return Ok();
            }


        }
    }
}
