
using System.Collections.Generic;


namespace PruebaInnovation.Interfaces
{
    interface IOperacionesConductor
    {
        //Operación para el apartado 1: Añadir un nuevo conductor al sistema
        void AgregarConductor(string dni, string nombre, string apellidos, int puntos);

        //Operación para el apartado 7: Consultar top n conductores
        List<Conductor> ObtenerTopNConductores(int n);

        Conductor GetConductor(string dni);
    }
}
