using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_ATM
{
    internal class ListaCircularCuentas
    {
        public Cuenta listacircular;
        public Cuenta actual;

        public ListaCircularCuentas()
        {
            listacircular = null;
            actual=null;
        }

        public void asignarInicio(Cuenta cuenta)
        {
            listacircular = cuenta;
            actual = cuenta;
        }

        public void insertarCuenta(Cuenta nuevaCuenta)
        {
            if (listacircular == null)
            {
                listacircular = nuevaCuenta;                
                actual.sgte = listacircular;
                nuevaCuenta.sgte = listacircular;

                return;
            }
            else
            {
                Cuenta cuenta = listacircular;

                while (cuenta.sgte != listacircular)
                {
                    cuenta = cuenta.sgte;
                }

                cuenta.sgte = nuevaCuenta;
                nuevaCuenta.sgte = listacircular;                
            }
        }

        public void mostrarCuentaCircular()
        {
            if (actual == null)
            {
                Console.WriteLine("No hay cuentas registradas.");
                return;
            }

            string opcion;

            do
            {
                Console.Clear();

                Console.WriteLine("--------------------------------");
                Console.WriteLine("Cuenta actual:");
                Console.WriteLine(actual.numeroCuenta + " - " + actual.tipoCuenta);
                Console.WriteLine("Saldo: S/ " + actual.consultarSaldo());
                Console.WriteLine("--------------------------------");

                Console.WriteLine("[1] Siguiente cuenta");
                Console.WriteLine("[0] Regresar al Menú");
                Console.Write("Seleccione: ");

                opcion = Console.ReadLine().ToUpper();


                if (opcion == "1")
                {
                    if (actual.sgte == null)
                    {
                        actual = listacircular;
                    }
                    else
                    {
                        actual = actual.sgte;
                    }
                }


            } while (opcion != "0");
        }
                
    }
}
