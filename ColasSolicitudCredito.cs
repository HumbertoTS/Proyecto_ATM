using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_ATM
{
    internal class ColaSolicitudCredito
    {
        public SolicitudCredito frente;
        public SolicitudCredito final;

        public ColaSolicitudCredito()
        {
            frente = null;
            final = null;
        }

        // Método para registrar una solicitud insertándola al final de la cola (FIFO).
        public void encolarSolicitud(Cliente cliente, Cuenta cuenta, string tipo, decimal monto, int plazo)
        {
            SolicitudCredito nueva = new SolicitudCredito(cliente, cuenta, tipo, monto, plazo);

            if (frente == null)
            {
                frente = nueva;
                final = nueva;
            }
            else
            {
                final.sgte = nueva;
                final = nueva;
            }
        }

        // Método para ver la siguiente solicitud a procesar sin sacarla de la cola.
        public SolicitudCredito Peek()
        {
            if (frente == null)
                return null;

            return frente;
        }

        // Método para sacar de la cola la solicitud más antigua (la que sigue en el turno).
        public SolicitudCredito Desencolar()
        {
            if (frente == null)
                return null;

            SolicitudCredito aux = frente;

            frente = frente.sgte;

            if (frente == null)
            {
                final = null;
            }

            aux.sgte = null;

            return aux;
        }

        // Método para mostrar las solicitudes pendientes en el orden en que serán atendidas.
        public void mostrarSolicitudes()
        {
            SolicitudCredito solicitud = frente;

            if (solicitud == null)
            {
                Console.WriteLine("No hay solicitudes pendientes en la cola.");
                return;
            }

            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine($"| {"Cliente",-20} | {"Tipo",-12} | {"Monto",-10} | {"Plazo",-6} | {"Estado",-10} |");
            Console.WriteLine("---------------------------------------------------------------------------------");

            while (solicitud != null)
            {
                string nombreCliente = solicitud.cliente.nombre + " " + solicitud.cliente.apellido;
                Console.WriteLine($"| {nombreCliente,-20} | {solicitud.tipo,-12} | {solicitud.monto,-10} | {solicitud.plazo,-6} | {solicitud.estado,-10} |");
                solicitud = solicitud.sgte;
            }

            Console.WriteLine("---------------------------------------------------------------------------------");
        }
    }
}
