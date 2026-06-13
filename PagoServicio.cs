using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_ATM
{
    internal class PagoServicio
    {
        public string tipoServicio;
        public string codigoSuministro;
        public decimal monto;

        public PagoServicio sgte;

        public PagoServicio(string tipoServicio, string codigoSuministro, decimal monto)
        {
            this.tipoServicio = tipoServicio;
            this.codigoSuministro = codigoSuministro;
            this.monto = monto;
            this.sgte = null;
        }
    }
}
