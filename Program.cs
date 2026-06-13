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
            ListaEnlazadaCuenta listaCuentasMaria = new ListaEnlazadaCuenta();

            // Crear la lista de cuentas del cliente
            ListaEnlazadaCuenta listaCuentas = new ListaEnlazadaCuenta();

            Cuenta cuenta1 = new Cuenta(1001, "Ahorros", 5000);
            Cuenta cuenta2 = new Cuenta(1002, "Corriente", 2500);

            listaCuentas.agregarCuenta(cuenta1);
            listaCuentas.agregarCuenta(cuenta2);

            listaCuentasMaria.agregarCuenta(new Cuenta(2001, "Ahorros", 1000));

            listaCuentasMaria.agregarCuenta(new Cuenta(2002, "Corriente", 1500));

            listaClientes.insertaCliente(
                87654321,
                "María",
                "López",
                "Av. Arequipa 456",
                999888777,
                "maria@gmail.com",
                4321,
                false,
                listaCuentasMaria
            );

            // Registrar un cliente
            listaClientes.insertaCliente(
                12345678,
                "Juan",
                "Pérez",
                "Av. Lima 123",
                987654321,
                "juan@gmail.com",
                1234,
                false,
                listaCuentas
            );

            // Crear el ATM
            ATM atm = new ATM(listaClientes);

            // Crear el menú
            Menu menu = new Menu(atm);

            // Iniciar sesión
            while (true)
            {
                
                menu.iniciarSesion();
                
            }

            //Console.ReadKey();
        }
    }
}
