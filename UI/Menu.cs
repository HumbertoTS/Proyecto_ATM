using System;
using Proyecto_ATM.Modelos;
using Proyecto_ATM.Estructuras;
using Proyecto_ATM.Servicios;

namespace Proyecto_ATM.UI
{
    internal class Menu
    {
        private ATM atm;

        public Menu(ATM atm)
        {
            this.atm = atm;
        }

        // Método para iniciar sesión del cliente.
        public void iniciarSesion()
        {
            Cliente cliente = null;
            while (cliente == null)
            {
                int dni;
                Console.WriteLine("\n ------------------------------------------------");
                Console.WriteLine("| Bienvenido al ATM BCP. Por favor, inicie sesión |");
                Console.WriteLine(" ------------------------------------------------");
                Console.Write("Ingrese su DNI: ");

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
                Console.WriteLine("\n[ERROR] El cliente se encuentra bloqueado.");
                return;
            }

            int intentos = 0;
            while (intentos < 3)
            {
                int pin;
                Console.Write("Ingrese su PIN: ");
                string pinInput = LeerPin();
                if (!int.TryParse(pinInput, out pin))
                {
                    Console.WriteLine("\nDebe ingresar un PIN válido.");
                    continue;
                }

                if (atm.validarPin(cliente, pin))
                {
                    Console.Clear();
                    Console.WriteLine($"==========================================");
                    Console.WriteLine($" ¡Bienvenido, {cliente.nombre} {cliente.apellido}!");
                    Console.WriteLine($"==========================================");
                    mostrarMenu(cliente);
                    return;
                }

                intentos++;
                Console.WriteLine($"\nPIN incorrecto. Intentos restantes: {3 - intentos}");
            }

            cliente.bloquearCliente();
            Console.WriteLine("\n[BLOQUEADO] El cliente ha sido bloqueado por exceder el número de intentos.");
            Console.WriteLine("Por favor, acérquese a plataforma para desbloquear su cuenta.");
        }

        // Método para mostrar el menú de opciones al cliente.
        public void mostrarMenu(Cliente cliente)
        {
            int opcion;

            do
            {
                Console.WriteLine();
                Console.WriteLine("========================================");
                Console.WriteLine("            MENÚ OPERACIONES            ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Consultar Saldo");
                Console.WriteLine("2. Retiro");
                Console.WriteLine("3. Transferencia");
                Console.WriteLine("4. Ver Historial de Movimientos");
                Console.WriteLine("5. Cambiar PIN");
                Console.WriteLine("6. Salir");
                Console.WriteLine("========================================");
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Debe ingresar una opción válida.");
                    continue;
                }

                Console.Clear();
                switch (opcion)
                {
                    case 1:
                        ejecutarConsultarSaldo(cliente);
                        break;

                    case 2:
                        ejecutarRetiro(cliente);
                        break;

                    case 3:
                        ejecutarTransferencia(cliente);
                        break;

                    case 4:
                        ejecutarVerMovimientos(cliente);
                        break;

                    case 5:
                        ejecutarCambioPin(cliente);
                        break;

                    case 6:
                        Console.WriteLine("Gracias por utilizar nuestro ATM. ¡Vuelva pronto!");
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

            } while (opcion != 6);
        }

        // Selecciona una cuenta del cliente imprimiendo las opciones en consola.
        private Cuenta seleccionarCuenta(ListaEnlazadaCuenta cuentas)
        {
            if (cuentas.estaVacia())
            {
                Console.WriteLine("El cliente no tiene cuentas registradas.");
                return null;
            }

            int posicion;
            Cuenta cuenta = null;

            while (cuenta == null)
            {
                Cuenta temp = cuentas.lista;
                int cont = 1;
                Console.WriteLine("\n-----------------------------------------------------------");
                Console.WriteLine($"| {"Opción",-8} | {"N° Cuenta",-12} | {"Tipo",-15} |");
                Console.WriteLine("-----------------------------------------------------------");
                while (temp != null)
                {
                    Console.WriteLine($"| {cont,-8} | {temp.numeroCuenta,-12} | {temp.tipoCuenta,-15} |");
                    cont++;
                    temp = temp.sgte;
                }
                Console.WriteLine("-----------------------------------------------------------");

                Console.Write("Seleccione una cuenta: ");
                if (int.TryParse(Console.ReadLine(), out posicion))
                {
                    cuenta = cuentas.buscarporPosicion(posicion);
                    if (cuenta == null)
                    {
                        Console.WriteLine("La opción no existe. Intente de nuevo.");
                    }
                }
                else
                {
                    Console.WriteLine("Debe ingresar un número válido.");
                }
            }

            return cuenta;
        }

        // Lógica de interfaz para Consultar Saldo.
        private void ejecutarConsultarSaldo(Cliente cliente)
        {
            Console.WriteLine("--- CONSULTAR SALDO ---");
            Cuenta cuenta = seleccionarCuenta(cliente.cuenta);
            if (cuenta != null)
            {
                Console.WriteLine($"\nEl saldo de la cuenta N° {cuenta.numeroCuenta} ({cuenta.tipoCuenta}) es: S/ {cuenta.consultarSaldo():F2}");
            }
        }

        // Lógica de interfaz para Retiros.
        private void ejecutarRetiro(Cliente cliente)
        {
            Console.WriteLine("--- REALIZAR RETIRO ---");
            Cuenta cuenta = seleccionarCuenta(cliente.cuenta);
            if (cuenta == null) return;

            Console.Write("Ingrese el monto a retirar: S/ ");
            decimal monto;
            if (decimal.TryParse(Console.ReadLine(), out monto))
            {
                string mensaje;
                bool exito = atm.realizarRetiro(cuenta, monto, out mensaje);
                Console.WriteLine($"\n{mensaje}");
                if (exito)
                {
                    Console.WriteLine($"Saldo restante: S/ {cuenta.consultarSaldo():F2}");
                }
            }
            else
            {
                Console.WriteLine("\nMonto inválido.");
            }
        }

        // Lógica de interfaz para Transferencias.
        private void ejecutarTransferencia(Cliente cliente)
        {
            Console.WriteLine("--- REALIZAR TRANSFERENCIA ---");
            Console.WriteLine("Seleccione su cuenta de origen:");
            Cuenta origen = seleccionarCuenta(cliente.cuenta);
            if (origen == null) return;

            Console.Write("\nIngrese el número de cuenta de destino: ");
            int numeroDestino;
            if (!int.TryParse(Console.ReadLine(), out numeroDestino))
            {
                Console.WriteLine("Número de cuenta inválido.");
                return;
            }

            Cuenta destino = atm.buscarCuentaGlobal(numeroDestino);
            if (destino == null)
            {
                Console.WriteLine("\n[ERROR] La cuenta de destino no existe en el sistema.");
                return;
            }

            Console.Write($"Ingrese el monto a transferir a la cuenta N° {destino.numeroCuenta}: S/ ");
            decimal monto;
            if (decimal.TryParse(Console.ReadLine(), out monto))
            {
                string mensaje;
                bool exito = atm.realizarTransferencia(origen, destino, monto, out mensaje);
                Console.WriteLine($"\n{mensaje}");
                if (exito)
                {
                    Console.WriteLine($"Nuevo saldo cuenta origen (N° {origen.numeroCuenta}): S/ {origen.consultarSaldo():F2}");
                }
            }
            else
            {
                Console.WriteLine("\nMonto inválido.");
            }
        }

        // Lógica de interfaz para ver el Historial de Movimientos.
        private void ejecutarVerMovimientos(Cliente cliente)
        {
            Console.WriteLine("--- VER HISTORIAL DE MOVIMIENTOS ---");
            Cuenta cuenta = seleccionarCuenta(cliente.cuenta);
            if (cuenta == null) return;

            if (cuenta.movimientos.estaVacia())
            {
                Console.WriteLine("\nNo se registraron movimientos en esta cuenta.");
                return;
            }

            Console.WriteLine($"\nHistorial de la cuenta N° {cuenta.numeroCuenta} ({cuenta.tipoCuenta}):");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"| {"Fecha/Hora",-20} | {"Tipo Operación",-22} | {"Monto",-12} | {"Detalles",-35} |");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            
            Movimiento temp = cuenta.movimientos.lista;
            while (temp != null)
            {
                Console.WriteLine($"| {temp.fechaHora.ToString("dd/MM/yyyy HH:mm:ss"),-20} | {temp.tipoOperacion,-22} | S/ {temp.monto,-9:F2} | {temp.detalles,-35} |");
                temp = temp.sgte;
            }
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
        }

        // Lógica de interfaz para Cambiar el PIN.
        private void ejecutarCambioPin(Cliente cliente)
        {
            Console.WriteLine("--- CAMBIAR PIN DE ACCESO ---");
            Console.Write("Ingrese su PIN actual: ");
            int pinActual;
            if (!int.TryParse(LeerPin(), out pinActual))
            {
                Console.WriteLine("\nPIN inválido.");
                return;
            }

            if (!cliente.validarPinAcceso(pinActual))
            {
                Console.WriteLine("\n[ERROR] El PIN ingresado no coincide con su PIN actual.");
                return;
            }

            Console.Write("Ingrese su nuevo PIN (4 dígitos): ");
            int nuevoPin;
            if (!int.TryParse(LeerPin(), out nuevoPin))
            {
                Console.WriteLine("\nPIN nuevo inválido. Debe contener sólo números.");
                return;
            }

            string mensaje;
            bool exito = cliente.cambiarPin(nuevoPin, out mensaje);
            Console.WriteLine($"\n{mensaje}");
        }

        // Método auxiliar para ocultar el PIN con asteriscos.
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
