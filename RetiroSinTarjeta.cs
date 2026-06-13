using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_ATM
{
    internal class RetiroSinTarjeta
    {
        public string codigo;
        public decimal monto;
        public string estado;

        public RetiroSinTarjeta sgte;

        public RetiroSinTarjeta(string codigo, decimal monto)
        {
            this.codigo = codigo;
            this.monto = monto;
            this.estado = "Generado";
            this.sgte = null;
        }
    }
}
