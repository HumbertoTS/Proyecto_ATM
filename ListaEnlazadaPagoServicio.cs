using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_ATM
{
    internal class ListaEnlazadaPagoServicio
    {
        public PagoServicio lista;
        public ListaEnlazadaPagoServicio()
        {
            lista = null;
        }
        public void insertarPago(string tipoServicio, string codigoSuministro, decimal monto)
        {
            PagoServicio q = new PagoServicio(tipoServicio, codigoSuministro, monto);

            if (lista == null)
            {
                lista = q;
            }
            else
            {
                PagoServicio pago = lista;
                while (pago.sgte != null)
                {
                    pago = pago.sgte;
                }
                pago.sgte = q;
            }
        }

        public void mostrarPagos()
        {
            PagoServicio pago = lista;

            if (pago == null)
            {
                Console.WriteLine("No hay pagos registrados.");
                return;
            }

            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine($"| {"Servicio",-10} | {"Código",-12} | {"Monto",-10} |");
            Console.WriteLine("-----------------------------------------------------------");

            while (pago != null)
            {
                Console.WriteLine($"| {pago.tipoServicio,-10} | {pago.codigoSuministro,-12} | {pago.monto,-10} |");
                pago = pago.sgte;
            }

            Console.WriteLine("-----------------------------------------------------------");
        }
    }
}
