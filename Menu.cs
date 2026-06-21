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

        //Menú Principal
        public void menuPrincipal()
        {
            int opcion;

            do
            {
                Console.Clear();

                Console.WriteLine("=================");
                Console.WriteLine("       ATM");
                Console.WriteLine("=================");
                Console.WriteLine("1. Cliente");
                Console.WriteLine("2. Administrador");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione: ");

                
                    opcion = int.Parse(Console.ReadLine());


                switch (opcion)
                {
                    case 1:
                        iniciarSesion();
                        break;

                    case 2:
                        iniciarSesionAdmin();
                        break;

                    case 0:
                        Console.WriteLine("\nCerrando sistema...");
                        Thread.Sleep(2000);
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Opción inválida.");
                        Thread.Sleep(1500);
                        break;
                }

            } while (opcion != 0);
        }

        //Menú para clientes
        //Método para iniciar sesión del cliente.
        public void iniciarSesion()
        {
            Cliente cliente = null;
            while (cliente == null)
            {
                string tarjeta;

                Console.Clear();
                Console.WriteLine(" ------------------------------------------------");
                Console.WriteLine("|              Bienvenido al ATM BCP.           |");
                Console.WriteLine("|             Por favor, inicie sesión.         |");
                Console.WriteLine(" ------------------------------------------------");
                Console.Write("Ingrese su número de tarjeta: ");

                tarjeta = Console.ReadLine();
                if (!Cliente.validarTarjeta(tarjeta))
                {
                    Console.WriteLine("\nDebe ingresar una tarjeta válida.");
                    Thread.Sleep(2000);
                    Console.Clear();
                    continue;
                }

                cliente = atm.buscarClientePorTarjeta(tarjeta);

                if (cliente == null)
                {
                    Console.WriteLine("No se encontró el número de tarjeta. Intente nuevamente.");
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
                Console.Write("Ingrese su PIN: ");
                string pin = LeerPin();                
                if (!Cliente.validarPin(pin))
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
                    Console.WriteLine("|    Bienvenido " + cliente.nombre + " " + cliente.apellido +"   |");                    
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
<<<<<<< HEAD
=======
            Console.WriteLine("Ingreso al menu exitoso");
            Console.ReadKey();
>>>>>>> origin/AReginaldo
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
<<<<<<< HEAD
                Console.WriteLine("3. Consultar Saldo");
                Console.WriteLine("4. Depósito");
                Console.WriteLine("5. Cambiar PIN");
                Console.WriteLine("6. Historial de Movimientos");
                Console.WriteLine("7. Solicitud de Crédito");
                Console.WriteLine("8. Retiro sin Tarjeta");
                Console.WriteLine("9. Pago de Servicios");
                Console.WriteLine("0. Salir");
=======
                Console.WriteLine("3. Salir");
                Console.WriteLine("7. Solicitud de Crédito");
                Console.WriteLine("8. Retiro sin Tarjeta");
                Console.WriteLine("9. Pago de Servicios");
>>>>>>> origin/AReginaldo
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
<<<<<<< HEAD
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
=======
                        //atm.retiro(cliente);
                        break;

                    case 2:
                        //atm.transferencia(cliente);
>>>>>>> origin/AReginaldo
                        break;

                    case 3:
                        Console.Clear();
                        cliente.cuentas.mostrarCuentasSaldos();
                        /*if (!volverAlMenu())
                        {
                            Console.Clear();
                            return;
                        }*/
                        break;
                    case 7:
                        Cuenta cuentaCredito = cliente.cuenta.seleccionarCuenta();
                        atm.solicitarCredito(cuentaCredito);
                        break;

<<<<<<< HEAD
                    case 4:
                        atm.deposito(cliente);
                        if (!volverAlMenu())
                        {
                            Console.Clear();
                            return;
                        }
                        break;

                    case 5:
                        atm.cambiarPin(cliente);
                        if (!volverAlMenu())
                        {
                            Console.Clear();
                            return;
                        }
                        break;

                    case 6:
                        atm.verHistorialMovimientos(cliente);
                        if (!volverAlMenu())
                        {
                            Console.Clear();
                            return;
                        }
                        break;

                    case 7:
                        Cuenta cuentaCredito = cliente.cuentas.seleccionarCuenta();
                        if (cuentaCredito != null)
                        {
                            atm.solicitarCredito(cuentaCredito);
                        }                        
                        break;

                    case 8:
                        Cuenta cuentaRetiro = cliente.cuentas.seleccionarCuenta();
                        if (cuentaRetiro != null)
                        {
                            atm.retiroSinTarjeta(cuentaRetiro);
                        }
                        break;

                    case 9:
                        Cuenta cuentaPago = cliente.cuentas.seleccionarCuenta();
                        if (cuentaPago != null)
                        {
                            atm.pagoServicio(cuentaPago);
                        }
                        break;

                    case 0:
                        
                        Console.WriteLine("\nGracias por usar el sistema. ¡Hasta luego!\n");
                        Thread.Sleep(2000);
                        
                        return;

=======
                    case 8:
                        Cuenta cuentaRetiro = cliente.cuenta.seleccionarCuenta();
                        atm.retiroSinTarjeta(cuentaRetiro);
                        break;

                    case 9:
                        Cuenta cuentaPago = cliente.cuenta.seleccionarCuenta();
                        atm.pagoServicio(cuentaPago);
                        break;
>>>>>>> origin/AReginaldo
                    default:
                        Console.WriteLine("Opción no válida.");
                        Thread.Sleep(2000);
                        break;
                }

<<<<<<< HEAD
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
            {
                if (opcion == 0)
                {
                    Console.WriteLine("\nGracias por usar el sistema. ¡Hasta luego!");
                    Thread.Sleep(2000);
                }

                return opcion == 1; 
            }
=======
            } while (opcion != 3);
>>>>>>> origin/AReginaldo
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

        //Menú para administrador
        public void iniciarSesionAdmin()
        {
            string codigo = "1596";
            string passwordAdmin = "1234";
            Console.Clear();
            Console.WriteLine(" ------------------------------------------------");
            Console.WriteLine("|          Bienvenido al ATM BCP Admin.         |");
            Console.WriteLine("|             Por favor, inicie sesión.         |");
            Console.WriteLine(" ------------------------------------------------");
            Console.Write("Ingrese su código: ");
            string usuario = Console.ReadLine();
            Console.Write("Ingrese su pin: ");
            string password = LeerPin();
            if (usuario == codigo && password == passwordAdmin)
            {
                Console.Clear();
                Console.WriteLine("===================================");
                Console.WriteLine("|              ATM Admin           |");
                Console.WriteLine("===================================");
                Console.WriteLine("|    Bienvenido Administrador      |");
                Console.WriteLine("|    Tu banco, tu tranquilidad.   |");
                Console.WriteLine("===================================");
                Thread.Sleep(2000);
                menuAdmin();
            }
            else
            {
                Console.WriteLine("\nUsuario o contraseña incorrectos.");
                Thread.Sleep(2000);
            }
        }

        public void menuAdmin()
        {
            int opcion;

            do
            {
                Console.Clear();

                Console.WriteLine("======= ADMIN =======");
                Console.WriteLine("1. Crear cuenta");
                Console.WriteLine("2. Asignar tarjeta");
                Console.WriteLine("3. Reportes");
                Console.WriteLine("0. Volver");

                Console.Write("Seleccione: ");

                opcion = int.Parse(Console.ReadLine());


                switch (opcion)
                {
                    case 1:
                        atm.crearCuenta();
                        break;

                    case 2:
                        atm.asignarTarjeta();
                        break;

                    case 3:
                        atm.reportes();
                        break;
                }                

            } while (opcion != 0);
        }
    }
}
