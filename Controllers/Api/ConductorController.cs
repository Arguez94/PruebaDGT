using PruebaInnovation.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PruebaInnovation.Controllers
{
    public class ConductorController : ApiController
    {
        private OperacionesConductor operaciones = new OperacionesConductor(new DgtContext());

        //Función para obtener conductor a partir de su DNI
        [HttpGet]
        public object Get(string dni)
        {
            try
            {
                Conductor conductor = operaciones.GetConductor(dni);
                return Request.CreateResponse(HttpStatusCode.OK, conductor);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.InnerException);
            }
        }

        //Funcion para crear un nuevo conductor
        [HttpPost]
        public object Post(dynamic body)
        {
            try
            {
                //TODO: Método para procesar el body
                string nombre = body.nombre ?? null;
                string apellidos = body.apellidos ?? null;
                string dni = body.dni ?? null;
                //Si no se le indican puntos, se le ponen 8 por defecto
                int puntos = body.puntos ?? Consts.PUNTOS_INICIALES;
                operaciones.AgregarConductor(dni, nombre, apellidos, puntos);
                return Request.CreateResponse(HttpStatusCode.OK, "El conductor con dni: " + dni + " se ha creado correctamente");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //Función para obtener el top N de conductores ordenados por puntos
        [HttpGet]
        public object GetTopNConductores(int n)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, operaciones.ObtenerTopNConductores(n));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.InnerException);
            }
        }
    }
}
