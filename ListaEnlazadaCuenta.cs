using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_ATM
{
    internal class ListaEnlazadaCuenta
    {
        public Cuenta lista;        

        public ListaEnlazadaCuenta()
        {
            lista = null;
        }
        // Método para verificar si la lista de cuentas está vacía.
        public bool estaVacia()
        {
            return lista == null;
        }
        // Método para mostrar todas las cuentas.
        public void mostrarCuentas()
        {
            Cuenta cuenta = lista;
            if(cuenta == null)
            {
                Console.WriteLine("No hay cuentas registradas.");
                return;
            }

            int posicion = 1;

            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine($"| {"Opción",-8} | {"N° Cuenta",-12} | {"Tipo",-15} |");
            Console.WriteLine("-----------------------------------------------------------");

            while (cuenta != null)
            {
                Console.WriteLine($"| {posicion,-8} | {cuenta.numeroCuenta,-12} | {cuenta.tipoCuenta,-15} |");

                posicion++;
                cuenta = cuenta.sgte;
            }

            Console.WriteLine("-----------------------------------------------------------");
        }
        //Método para la opción de Consultar Saldo.
        public void mostrarCuentasSaldos()
        {
            ListaCircularCuentas circular = new ListaCircularCuentas();
            Cuenta cuenta = lista;

            if (cuenta == null)
            {
                Console.WriteLine("No hay cuentas registradas.");
                return;
            }

            circular.asignarInicio(lista);

            circular.mostrarCuentaCircular();
        }        
        // Método para agregar una cuenta.
        public void agregarCuenta(Cuenta nuevaCuenta)
        {
            if (nuevaCuenta == null)
            {
                return;
            }

            if (buscarCuenta(nuevaCuenta.numeroCuenta) != null)
            {
                Console.WriteLine("Ya existe una cuenta con ese número.");
                return;
            }

            if (lista == null)
            {
                lista = nuevaCuenta;
            }
            else
            {
                Cuenta cuenta = lista;
                while (cuenta.sgte != null)
                {
                    cuenta = cuenta.sgte;
                }
                cuenta.sgte = nuevaCuenta;
            }
        }
        // Método para buscar una cuenta.
        public Cuenta buscarCuenta(string numeroCuenta)
        {
            Cuenta cuenta = lista;
            while (cuenta != null)
            {
                if (cuenta.numeroCuenta == numeroCuenta)
                {
                    return cuenta;
                }
                cuenta = cuenta.sgte;
            }
            return null;
        }
        //Método para eliminar una cuenta.
        public void eliminarCuenta(string numeroCuenta)
        {
            //En caso este vacío la lista
            if (lista == null)
            {
                Console.WriteLine("No hay cuentas registradas.");
                return;
            }
            //Elimina la primera cuenta
            if (lista.numeroCuenta == numeroCuenta)
            {
                lista = lista.sgte;
                Console.WriteLine("Cuenta eliminada correctamente.");
                return;
            }
            Cuenta cuenta = lista;
            while (cuenta.sgte != null)
            {
                if (cuenta.sgte.numeroCuenta == numeroCuenta)
                {
                    cuenta.sgte = cuenta.sgte.sgte;
                    Console.WriteLine("Cuenta eliminada correctamente.");
                    return;
                }
                cuenta = cuenta.sgte;
            }
            Console.WriteLine("Cuenta no encontrada.");
        }
        //Busca la posición de la cuenta en la lista.
        public Cuenta buscarPorPosicion(int posicion)
        {
        Cuenta cuenta = lista;
        int contador = 1;
        while (cuenta != null)
        {
            if (contador == posicion)
            {
                return cuenta;
            }
            contador++;
            cuenta = cuenta.sgte;
        }
        return null; // Si no se encuentra la posición, devuelve null.
        }
        //Método para seleccionar una cuenta.
        public Cuenta seleccionarCuenta()
        {
            if (estaVacia())
            {
                Console.WriteLine("No hay cuentas registradas.");
                return null;
            }

            mostrarCuentas();
            int posicion = 0;
            Cuenta cuenta = null;

            while (cuenta == null)
            {
                Console.Write("Seleccione una cuenta: ");

                if (int.TryParse(Console.ReadLine(), out posicion))
                {
                    cuenta = buscarPorPosicion(posicion);

                    if (cuenta == null)
                    {
                        Console.WriteLine("La opción no existe.");
                    }
                }
                else
                {
                    Console.WriteLine("Debe escoger la opción de cuenta correcta.");
                }
            }

            return cuenta;
        }
        //Método para contar el número de cuentas en la lista.
        public int contarCuentas()
        {
            int contador = 0;
            Cuenta cuenta = lista;
            while (cuenta != null)
            {
                contador++;
                cuenta = cuenta.sgte;
            }
            return contador;
        }
        public void mostrarCuentasReporte()
        {
            Cuenta cuenta = lista;

            if (cuenta == null)
            {
                Console.WriteLine("Sin cuentas.");
                return;
            }

            while (cuenta != null)
            {
                Console.WriteLine("Cuenta: " + cuenta.numeroCuenta);
                Console.WriteLine("Tipo: " + cuenta.tipoCuenta);
                Console.WriteLine("Saldo: S/ " + cuenta.consultarSaldo());
                Console.WriteLine("------------------------");

                cuenta = cuenta.sgte;
            }
        }
    }
}
