using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_ATM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Crear la lista de clientes
            ListaEnlazadaCliente listaClientes = new ListaEnlazadaCliente();

            // Crear la lista de cuentas del cliente
            ListaEnlazadaCuenta listaCuentas = new ListaEnlazadaCuenta();

            Cuenta cuenta1 = new Cuenta(1001, "Ahorros", 5000);
            Cuenta cuenta2 = new Cuenta(1002, "Corriente", 2500);

            listaCuentas.agregarCuenta(cuenta1);
            listaCuentas.agregarCuenta(cuenta2);

            // Registrar un cliente
            listaClientes.insertaCliente(
                12345678,
                "Juan",
                "Pérez",
                "Av. Lima 123",
                987654321,
                "juan@gmail.com",
                1234,
                listaCuentas
            );

            // Crear el ATM
            ATM atm = new ATM(listaClientes);

            // Crear el menú
            Menu menu = new Menu(atm);

            // Iniciar sesión
            while (true)
            {
                Console.Clear();
                menu.iniciarSesion();
            }

            //Console.ReadKey();
        }
    }
}
