using System;

namespace Proyecto_ATM.Modelos
{
    internal class Movimiento
    {
        public string tipoOperacion { get; set; }
        public decimal monto { get; set; }
        public DateTime fechaHora { get; set; }
        public string detalles { get; set; }

        public Movimiento sgte;

        public Movimiento(string tipoOperacion, decimal monto, string detalles)
        {
            this.tipoOperacion = tipoOperacion;
            this.monto = monto;
            this.fechaHora = DateTime.Now;
            this.detalles = detalles;
            this.sgte = null;
        }
    }
}
