
using PruebaInnovation.Classes;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PruebaInnovation.Controllers
{
    [RoutePrefix("Api/Infraccion")]
    public class InfraccionController : ApiController
    {
        private OperacionesInfraccion operaciones = new OperacionesInfraccion(new DgtContext());

        //Función para crear un nuevo Tipo de infraccion
        [HttpPost]
        [Route("PostTipoInfraccion")]
        public object PostTipoInfraccion(dynamic body)
        {
            try
            {
                //TODO: Método para procesar el body
                string identificador = body.identificador ?? null;
                string descripcion = body.descripcion ?? null;
                int puntosDescuento = body.puntosDescuento ?? 0;

                operaciones.AgregarTipoInfraccion(identificador, descripcion, puntosDescuento);
                return Request.CreateResponse(HttpStatusCode.OK, "El tipo infraccion con identificador: " + identificador + " se ha creado correctamente");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //Función para registrar una nueva infracción
        [HttpPost]
        [Route("RegistrarInfraccion")]
        public object RegistrarInfraccion(dynamic body)
        {
            try
            {
                //TODO: Método para procesar el body
                string identificador = body.identificador ?? null;
                string identificadorTipoInfraccion = body.identificadorTipoInfraccion ?? null;
                DateTime fechaYHora = body.fechaYHora ?? DateTime.Now;
                string matricula = body.matricula;
                string dniConductor = body.dni;

                operaciones.RegistrarInfraccion(identificador, identificadorTipoInfraccion, fechaYHora, matricula, dniConductor);
                return Request.CreateResponse(HttpStatusCode.OK, "El tipo infraccion con identificador: " + identificador + " se ha creado correctamente");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //Función para obtener las infracciones de un conductor
        [HttpGet]
        public object GetInfraccionesConductor(string dni)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, operaciones.ObtenerInfraccionesConductor(dni));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.InnerException);
            }
        }

        //Función para obtener el top 5 de infracciones cometidas
        [HttpGet]
        public object GetTop5Infracciones()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, operaciones.ObtenerTop5Infracciones());
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.InnerException);
            }
        }
    }
}
