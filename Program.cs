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
            ListaEnlazadaCliente listaClientes = new ListaEnlazadaCliente();

            // Crear cuenta
            Cuenta cuenta1 = new Cuenta(1001, "Ahorros", 1500);

            Cuenta[] cuentas = new Cuenta[1];
            cuentas[0] = cuenta1;

            // Insertar cliente
            listaClientes.insertaCliente(
                12345678,
                "Juan",
                "Perez",
                "Av. Principal 123",
                987654321,
                "juan@gmail.com",
                1234,
                cuentas
            );

            // Crear ATM
            ATM atm = new ATM(listaClientes);

            // Pedir datos
            Console.Write("Ingrese DNI: ");
            int dni = int.Parse(Console.ReadLine());

            Console.Write("Ingrese PIN: ");
            int pin = int.Parse(Console.ReadLine());

            // Probar login
            Cliente cliente = atm.IniciarSesion(dni, pin);

            if (cliente != null)
            {
                Console.WriteLine("Acceso concedido.");
            }
            else
            {
                Console.WriteLine("Acceso denegado.");
            }

            Console.ReadKey();
        }
    }
}
