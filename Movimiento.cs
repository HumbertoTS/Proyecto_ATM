using System;

namespace Proyecto_ATM
{
    internal class Movimiento
    {
        public string tipo;
        public decimal monto;
        public DateTime fecha;
        public string detalle;

        public Movimiento sgte;
        public Movimiento ant;

        // Constructor para registrar un movimiento
        public Movimiento(string tipo, decimal monto, string detalle)
        {
            this.tipo = tipo;
            this.monto = monto;
            this.fecha = DateTime.Now;
            this.detalle = detalle;
            this.sgte = null;
            this.ant = null;
        }
    }
}
