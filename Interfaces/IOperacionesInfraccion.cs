
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaInnovation.Interfaces
{
    interface IOperacionesInfraccion
    {
        //Operación para el apartado 4: registrar infracción a un vehículo
        void RegistrarInfraccion(string identificador, string identificadorTipoInfraccion, DateTime fechaYHora, Vehiculo vehiculo, string dniConductor);

        //Operación para el apartado 5: Obtener infracciones de un conductor.
        //Pongo el DNI como parámetro para facilitar el desarrollo de la funcionalidad ya que en el enunciado no especificaba el parámetro.
        //Podría utilizarse un objeto conductor, su nombre, apellidos, etc. dependiendo de los requerimientos.
        List<Infraccion> ObtenerInfraccionesConductor(string dni);

        //Operación para el apartado 6: Obtener top 5 infracciones mas cometidas
        List<TipoInfraccion> ObtenerTop5Infracciones();
    }
}
