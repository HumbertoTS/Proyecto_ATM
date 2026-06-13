using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_ATM
{
    internal class ListaEnlazadaSolicitudCredito
    {
        public SolicitudCredito lista;

        public ListaEnlazadaSolicitudCredito()
        {
            lista = null;
        }

        public void insertarSolicitud(string tipo, decimal monto, int plazo)
        {
            SolicitudCredito q = new SolicitudCredito(tipo, monto, plazo);

            if (lista == null)
            {
                lista = q;
            }
            else
            {
                SolicitudCredito solicitud = lista;
                while (solicitud.sgte != null)
                {
                    solicitud = solicitud.sgte;
                }
                solicitud.sgte = q;
            }
        }

        public void mostrarSolicitudes()
        {
            SolicitudCredito solicitud = lista;

            if (solicitud == null)
            {
                Console.WriteLine("No hay solicitudes registradas.");
                return;
            }

            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine($"| {"Tipo",-15} | {"Monto",-10} | {"Plazo",-8} | {"Estado",-10} |");
            Console.WriteLine("-----------------------------------------------------------");

            while (solicitud != null)
            {
                Console.WriteLine($"| {solicitud.tipo,-15} | {solicitud.monto,-10} | {solicitud.plazo,-8} | {solicitud.estado,-10} |");
                solicitud = solicitud.sgte;
            }

            Console.WriteLine("-----------------------------------------------------------");
        }
    }
}
