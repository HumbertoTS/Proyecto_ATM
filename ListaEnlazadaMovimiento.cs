using System;

namespace Proyecto_ATM
{
    internal class ListaEnlazadaMovimiento
    {
        public Movimiento lista;

        public ListaEnlazadaMovimiento()
        {
            lista = null;
        }

        // Método para registrar un movimiento insertándolo al inicio de la lista
        public void registrarMovimiento(string tipo, decimal monto, string detalle)
        {
            Movimiento nuevo = new Movimiento(tipo, monto, detalle);
            nuevo.sgte = lista;
            lista = nuevo;
        }

        // Método para mostrar el historial de movimientos de la cuenta
        public void mostrarHistorial()
        {
            Movimiento movimiento = lista;

            if (movimiento == null)
            {
                Console.WriteLine("No hay movimientos registrados en esta cuenta.");
                return;
            }

            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.WriteLine($"| {"Fecha y Hora",-20} | {"Operación",-22} | {"Monto",-12} | {"Detalle",-28} |");
            Console.WriteLine("-----------------------------------------------------------------------------------------");

            while (movimiento != null)
            {
                Console.WriteLine($"| {movimiento.fecha.ToString("dd/MM/yyyy HH:mm:ss"),-20} | {movimiento.tipo,-22} | S/ {movimiento.monto,-9:F2} | {movimiento.detalle,-28} |");
                movimiento = movimiento.sgte;
            }

            Console.WriteLine("-----------------------------------------------------------------------------------------");
        }
    }
}
