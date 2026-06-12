using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_ATM
{
    internal class SolicitudCredito
    {
        public string tipo;
        public decimal monto;
        public int plazo;
        public string estado;

        public SolicitudCredito sgte;

        public SolicitudCredito(string tipo, decimal monto, int plazo)
        {
            this.tipo = tipo;
            this.monto = monto;
            this.plazo = plazo;
            this.estado = "Pendiente";
            this.sgte = null;
        }
    }
}
