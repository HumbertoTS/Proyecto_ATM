using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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

                Console.Clear();
                Console.WriteLine(" ------------------------------------------------");
                Console.WriteLine("|              Bienvenido al ATM BCP.           |");
                Console.WriteLine("|             Por favor, inicie sesión.         |");
                Console.WriteLine(" ------------------------------------------------");
                Console.Write("Ingrese su DNI: ");


                if (!int.TryParse(Console.ReadLine(), out dni))
                {
                    Console.WriteLine("\nDebe ingresar un DNI válido.");
                    Thread.Sleep(2000);
                    Console.Clear();
                    continue;
                }
                if (!Cliente.contarDigitosDni(dni))
                {
                    Console.WriteLine("\nEl DNI debe tener 8 dígitos.");
                    Thread.Sleep(2000);
                    continue;
                }

                cliente = atm.buscarCliente(dni);

                if (cliente == null)
                {
                    Console.WriteLine("DNI no encontrado. Intente nuevamente.");
                    Thread.Sleep(2000);
                    Console.Clear();
                }
            }

            if (cliente.verificarBloqueo())
            {
                Console.WriteLine("Se encuentra bloqueado por temas de seguridad o ingreso erróneo de pin.");
                Console.WriteLine("Por favor, acercarse a plataforma para desbloquear su cuenta.");
                Thread.Sleep(4000);
                return;
            }

            int intentos = 0;
            while(intentos < 3)
            {
                int pin;
                Console.Write("Ingrese su PIN: ");
                string pinInput = LeerPin();                
                if (!int.TryParse(pinInput, out pin))
                {
                    Console.WriteLine("Debe ingresar un PIN válido.\n");
                    Thread.Sleep(2000);
                    
                    continue;
                }

                if (cliente.validarPinAcceso(pin))
                {
                    Console.Clear();
                    Console.WriteLine("===================================");
                    Console.WriteLine("|              ATM                |");
                    Console.WriteLine("===================================");
                    Console.WriteLine("| Bienvenido " + cliente.nombre + " " + cliente.apellido +" |");
                    Console.WriteLine();
                    Console.WriteLine("|    Tu banco, tu tranquilidad.   |");
                    Console.WriteLine("===================================");
                    Thread.Sleep(2000);
                    mostrarMenu(cliente);
                    return;
                }
                intentos++;
                Console.WriteLine("PIN incorrecto. Intentos restantes: " + (3 - intentos));
               
            }

            cliente.bloquearCliente();
            Console.WriteLine("El cliente ha sido bloqueado por exceder la cantidad de intentos");
            Console.WriteLine("Por favor, acercarse a plataforma para desbloquear su cuenta.");
            Thread.Sleep(4000);
            
        }
        //Método para mostrar el menú de opciones al cliente después de iniciar sesión exitosamente.
        public void mostrarMenu(Cliente cliente)
        {
            int opcion;

            while (true)
            {
                Console.Clear();
               
                Console.WriteLine();
                Console.WriteLine("===================================");
                Console.WriteLine("              ATM");
                Console.WriteLine("===================================");
                Console.WriteLine("1. Retiro");
                Console.WriteLine("2. Transferencia");
                Console.WriteLine("7. Solicitud de Crédito");
                Console.WriteLine("8. Retiro sin Tarjeta");
                Console.WriteLine("9. Pago de Servicios");
                Console.WriteLine("0. Salir");
                Console.WriteLine("===================================");
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Debe ingresar una opción válida.");
                    Console.ReadKey();
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        atm.retiro(cliente);
                        if (!volverAlMenu())
                        {
                            Console.Clear();
                            return;
                        }                        
                        break;

                    case 2:
                        atm.transferencia(cliente);
                        if (!volverAlMenu())
                        {
                            Console.Clear();
                            return;
                        }
                        break;

                    case 7:
                        Cuenta cuentaCredito = cliente.cuentas.seleccionarCuenta();
                        atm.solicitarCredito(cuentaCredito);
                        break;

                    case 8:
                        Cuenta cuentaRetiro = cliente.cuentas.seleccionarCuenta();
                        atm.retiroSinTarjeta(cuentaRetiro);
                        break;

                    case 9:
                        Cuenta cuentaPago = cliente.cuentas.seleccionarCuenta();
                        atm.pagoServicio(cuentaPago);
                        break;

                    case 0:
                        
                        Console.WriteLine("\nGracias por utilizar nuestro ATM.\n");
                        Thread.Sleep(2000);
                        
                        return;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

            } 
        }
        //Método para consultar si desea volver al menú o solo salir.
        public bool volverAlMenu()
        {
            int opcion;

            do
            {
                Console.WriteLine("\n¿Desea realizar otra operación?");
                Console.WriteLine("0. No");
                Console.WriteLine("1. Sí");                
                Console.Write("Seleccione una opción: ");

            } while (!int.TryParse(Console.ReadLine(), out opcion) ||
                     (opcion != 0 && opcion != 1));

            return opcion == 1;
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
