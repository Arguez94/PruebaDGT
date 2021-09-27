using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaInnovation.Classes
{
    public class Utils
    {
        //Método para comprobar que ya existe el vehículo o no. 
        public static bool ExisteVehiculo(string matricula, DgtContext db)
        {
            return db.Vehiculo.Any(w => w.Matricula == matricula);
        }

        //Método para comprobar si ya existe el conductor.
        public static bool ExisteConductor(string dni, DgtContext db)
        {
            return db.Conductor.Any(w => w.DNI == dni);
        }

        //Función para comprobar si la infracción ya existe, en base a su id
        public static bool ExisteInfraccion(string identificador, DgtContext db)
        {
            return DataStorage.Infracciones.Any(a => a.Identificador == identificador);
        }

        //Función para comprobar si el tipo de infracción ya existe, en base a su id
        public static bool ExisteTipoInfraccion(string identificador, DgtContext db)
        {
            return DataStorage.TiposInfraccion.Any(a => a.Identificador == identificador);
        }


    }
}