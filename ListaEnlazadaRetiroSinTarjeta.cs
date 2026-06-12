using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_ATM
{
    internal class ListaEnlazadaRetiroSinTarjeta
    {
        public RetiroSinTarjeta lista;

        public ListaEnlazadaRetiroSinTarjeta()
        {
            lista = null;
        }

        public void insertarRetiro(string codigo, decimal monto)
        {
            RetiroSinTarjeta q = new RetiroSinTarjeta(codigo, monto);

            if (lista == null)
            {
                lista = q;
            }
            else
            {
                RetiroSinTarjeta retiro = lista;
                while (retiro.sgte != null)
                {
                    retiro = retiro.sgte;
                }
                retiro.sgte = q;
            }
        }

        public void mostrarRetiros()
        {
            RetiroSinTarjeta retiro = lista;

            if (retiro == null)
            {
                Console.WriteLine("No hay retiros registrados.");
                return;
            }

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine($"| {"Código",-10} | {"Monto",-10} | {"Estado",-10} |");
            Console.WriteLine("-------------------------------------------");

            while (retiro != null)
            {
                Console.WriteLine($"| {retiro.codigo,-10} | {retiro.monto,-10} | {retiro.estado,-10} |");
                retiro = retiro.sgte;
            }

            Console.WriteLine("-------------------------------------------");
        }
    }
}
