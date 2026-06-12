using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_ATM
{
    internal class Menu
    {
        private ATM atm;

        public Menu(ATM atm)
        {
            this.atm = atm;
        }
        
        //Método para iniciar sesión del cliente.
        public void iniciarSesion()
        {
            Cliente cliente = null;
            while (cliente == null)
            {
                int dni;
                
                Console.WriteLine(" ------------------------------------------------");
                Console.WriteLine("|Bienvenido al ATM BCP. Por favor, inicie sesión.|");
                Console.WriteLine(" ------------------------------------------------");
                Console.WriteLine("Ingrese su DNI: ");

                if (!int.TryParse(Console.ReadLine(), out dni))
                {
                    Console.WriteLine("\nDebe ingresar un DNI válido.");
                    continue;
                }

                cliente = atm.buscarCliente(dni);

                if (cliente == null)
                {
                    Console.WriteLine("DNI no encontrado. Intente nuevamente.");
                }
            }

            if (cliente.verificarBloqueo())
            {
                Console.WriteLine("El cliente se encuentra bloqueado");
                return;
            }

            int intentos = 0;
            while(intentos < 3)
            {
                int pin;
                Console.WriteLine("Ingrese su PIN: ");
                string pinInput = LeerPin();                
                if (!int.TryParse(pinInput, out pin))
                {
                    Console.WriteLine("Debe ingresar un PIN válido.");
                    continue;
                }

                if (atm.validarPin(cliente, pin))
                {
                    Console.Clear();
                    Console.WriteLine("Bienvenido " + cliente.nombre);
                    mostrarMenu(cliente);
                    return;
                }
                intentos++;
                Console.WriteLine("PIN incorrecto. Intentos restantes: " + (3 - intentos));
               
            }

            cliente.bloquearCliente();
            Console.WriteLine("El cliente ha sido bloqueado por exceder la cantidad de intentos");
            Console.WriteLine("Por favor, acercarse a plataforma para desbloquear su cuenta");
            
        }
        //Método para mostrar el menú de opciones al cliente después de iniciar sesión exitosamente.
        public void mostrarMenu(Cliente cliente)
        {
            int opcion;

            do
            {
                Console.WriteLine();
                Console.WriteLine("===================================");
                Console.WriteLine("              ATM");
                Console.WriteLine("===================================");
                Console.WriteLine("1. Retiro");
                Console.WriteLine("2. Transferencia");
                Console.WriteLine("3. Salir");
                Console.WriteLine("===================================");
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Debe ingresar una opción válida.");
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        //atm.retiro(cliente);
                        Console.WriteLine("Funcionalidad de retiro aún no implementada.");
                        break;

                    case 2:
                        //atm.transferencia(cliente);
                        Console.WriteLine("Funcionalidad de transferencia aún no implementada.");
                        break;

                    case 3:
                        Console.WriteLine("Gracias por utilizar nuestro ATM.");
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

            } while (opcion != 3);
        }
        //Método para leer el PIN ingresado por el cliente, ocultando los caracteres.
        private string LeerPin()
        {
            string pin = "";

            while (true)
            {
                ConsoleKeyInfo tecla = Console.ReadKey(true);

                if (tecla.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    return pin;
                }

                if (tecla.Key == ConsoleKey.Backspace && pin.Length > 0)
                {
                    pin = pin.Substring(0, pin.Length - 1);
                    Console.Write("\b \b");
                    continue;
                }

                if (char.IsDigit(tecla.KeyChar))
                {
                    pin += tecla.KeyChar;
                    Console.Write("*");
                }
            }
        }
    }
}
