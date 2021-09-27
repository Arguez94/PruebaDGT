
using System.Collections.Generic;


namespace PruebaInnovation.Classes
{
    public static class DataStorage
    {
        public static List<Conductor> Conductores { get; set; }
        public static List<Vehiculo> Vehiculos { get; set; }
        public static List<Infraccion> Infracciones { get; set; }
        public static List<TipoInfraccion> TiposInfraccion { get; set; }
    }
}