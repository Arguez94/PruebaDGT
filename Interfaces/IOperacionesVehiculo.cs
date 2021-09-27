using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaInnovation.Interfaces
{
    interface IOperacionesVehiculo
    {
        //Operación para el apartado 2: Añadir nuevo vehículo al sistema
        void AgregarVehiculo(string matricula, string marca, string modelo, string dniConductor);
    }
}
