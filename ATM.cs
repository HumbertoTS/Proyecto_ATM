using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proyecto_ATM
{
    internal class ATM
    {
        public ListaEnlazadaCliente clientes;
        public ListaEnlazadaSolicitudCredito solicitudes;
        public ListaEnlazadaRetiroSinTarjeta retirosSinTarjeta;
        public ListaEnlazadaPagoServicio pagosServicio;
        //Constructor del ATM que recibe la lista de clientes.
        public ATM(ListaEnlazadaCliente clientes) { 
            
            this.clientes = clientes;
            this.solicitudes = new ListaEnlazadaSolicitudCredito();
            this.retirosSinTarjeta = new ListaEnlazadaRetiroSinTarjeta();
            this.pagosServicio = new ListaEnlazadaPagoServicio();
        }
        
        //Buscar cliente por DNI.
        public Cliente buscarCliente(string dni)
        {
            return clientes.buscarPorDni(dni);
        }
        //Método para retiro.
        public void retiro(Cliente cliente)
        {
            Console.Clear();
            Console.WriteLine("===== RETIRO =====");

            Cuenta cuentaSeleccionada = cliente.cuentas.seleccionarCuenta();

            if (cuentaSeleccionada == null)
            { 
                return; 
            }

            Console.Clear();
            Console.WriteLine("\nCuenta: " + cuentaSeleccionada.numeroCuenta + " | " + cuentaSeleccionada.tipoCuenta);
            Console.WriteLine("Saldo disponible: S/ " + cuentaSeleccionada.consultarSaldo());

            decimal monto;

            do
            {
                Console.Write("\nIngrese el monto a retirar: S/. ");

                if (!decimal.TryParse(Console.ReadLine(), out monto))
                {
                    Console.WriteLine("Debe ingresar un monto válido.");
                    Thread.Sleep(1500);
                    continue;
                }

                if (monto <= 0)
                {
                    Console.WriteLine("El monto debe ser mayor a cero.");
                    Thread.Sleep(1500);
                    continue;
                }

                break;

            } while (true);
                       
            if (cuentaSeleccionada.retirar(monto))
            {
                Console.WriteLine("\nRetiro realizado correctamente.");
                Console.WriteLine("Monto retirado: S/ " + monto);
                Console.WriteLine("Saldo actual: S/ " + cuentaSeleccionada.consultarSaldo());
            }
            else
            {
                Console.WriteLine("\nSaldo insuficiente.");
                Console.WriteLine("Saldo disponible: S/ " + cuentaSeleccionada.consultarSaldo());
            }

            Thread.Sleep(2000);
            Console.Clear();
        }
        //Método para módulo transferencia.
        public void transferencia(Cliente cliente)
        {
            Console.Clear();
            Console.WriteLine("===== TRANSFERENCIA =====");

            Cuenta cuentaOrigen = cliente.cuentas.seleccionarCuenta();

            if (cuentaOrigen == null)
            {
                return;
            }

            Console.Write("\nSaldo disponible: S/. " + cuentaOrigen.consultarSaldo());

            string numeroCuentaDestino;
            Cuenta cuentaDestino;

            do
            {
                Console.Write("\nIngrese el número de cuenta destino: ");
                numeroCuentaDestino = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(numeroCuentaDestino))
                {
                    Console.WriteLine("Debe ingresar un número de cuenta válido.");
                    Thread.Sleep(1500);
                    continue;
                }

                cuentaDestino = buscarCuentaDestino(numeroCuentaDestino);

                if (cuentaDestino == null)
                {
                    Console.WriteLine("Cuenta destino no encontrada.");
                    Thread.Sleep(1500);
                    continue;
                }

                if (cuentaOrigen.numeroCuenta == cuentaDestino.numeroCuenta)
                {
                    Console.WriteLine("No puede transferir a la misma cuenta.");
                    Thread.Sleep(1500);
                    continue;
                }

                break;

            } while (true);

            decimal monto;

            do
            {
                Console.Write("\nIngrese el monto a transferir: S/. ");

                if (!decimal.TryParse(Console.ReadLine(), out monto) || monto <= 0)
                {
                    Console.WriteLine("Debe ingresar un monto válido.");
                    Thread.Sleep(1500);                    
                    continue;
                }

                break;

            } while (true);

            Console.Clear();
            Console.WriteLine("\n===== RESUMEN DE TRANSFERENCIA =====");
            Console.WriteLine("Cuenta origen : " + cuentaOrigen.numeroCuenta);
            Console.WriteLine("Cuenta destino: " + cuentaDestino.numeroCuenta);
            Console.WriteLine("Monto         : S/. " + monto);

            Console.WriteLine("\n1. Confirmar");
            Console.WriteLine("0. Cancelar");
            Console.Write("Seleccione una opción: ");

            int opcion;
            if (!int.TryParse(Console.ReadLine(), out opcion) || opcion != 1)
            {
                Console.WriteLine("Transferencia cancelada.");
                Thread.Sleep(1500);
                Console.Clear();
                return;
            }

            if (cuentaOrigen.retirar(monto))
            {
                cuentaDestino.depositar(monto);

                Console.WriteLine("\nTransferencia realizada correctamente.");
                Console.WriteLine("Saldo actual: S/. " + cuentaOrigen.consultarSaldo());
            }
            else
            {
                Console.WriteLine("\nSaldo insuficiente.");
            }

            Thread.Sleep(2000);
            Console.Clear();
        }

        public void solicitarCredito(Cuenta cuenta)
        {
            Console.WriteLine("===================================");
            Console.WriteLine("       SOLICITUD DE CRÉDITO        ");
            Console.WriteLine("===================================");
            Console.WriteLine("1. Personal");
            Console.WriteLine("2. Vehicular");
            Console.WriteLine("3. Hipotecario");
            Console.Write("Seleccione tipo de crédito: ");

            int opcion;
            if (!int.TryParse(Console.ReadLine(), out opcion) || opcion < 1 || opcion > 3)
            {
                Console.WriteLine("Opción no válida.");
                return;
            }

            string tipo;
            if (opcion == 1) tipo = "Personal";
            else if (opcion == 2) tipo = "Vehicular";
            else tipo = "Hipotecario";

            decimal monto;
            Console.Write("Ingrese el monto solicitado: S/ ");
            if (!decimal.TryParse(Console.ReadLine(), out monto) || monto <= 0)
            {
                Console.WriteLine("Monto no válido.");
                return;
            }

            int plazo;
            Console.Write("Ingrese el plazo en meses: ");
            if (!int.TryParse(Console.ReadLine(), out plazo) || plazo <= 0)
            {
                Console.WriteLine("Plazo no válido.");
                return;
            }

            solicitudes.insertarSolicitud(tipo, monto, plazo);
            Console.WriteLine("Solicitud registrada correctamente. Estado: Pendiente.");
        }

        public void retiroSinTarjeta(Cuenta cuenta)
        {
            Console.WriteLine("===================================");
            Console.WriteLine("        RETIRO SIN TARJETA         ");
            Console.WriteLine("===================================");

            decimal monto;
            Console.Write("Ingrese el monto a retirar: S/ ");
            if (!decimal.TryParse(Console.ReadLine(), out monto) || monto <= 0)
            {
                Console.WriteLine("Monto no válido.");
                return;
            }

            if (!cuenta.tieneSaldo(monto))
            {
                Console.WriteLine("Saldo insuficiente.");
                return;
            }

            Random rnd = new Random();
            string codigo = rnd.Next(100000, 999999).ToString();

            Console.WriteLine("===================================");
            Console.WriteLine("Su código de retiro es: " + codigo);
            Console.WriteLine("Monto: S/ " + monto);
            Console.WriteLine("Válido por 30 minutos.");
            Console.WriteLine("===================================");
            Console.Write("¿Confirma el retiro? (S/N): ");

            string confirma = Console.ReadLine();

            if (confirma != null && confirma.ToUpper() == "S")
            {
                cuenta.retirar(monto);
                retirosSinTarjeta.insertarRetiro(codigo, monto);
                Console.WriteLine("Retiro exitoso. Retire su dinero.");
            }
            else
            {
                Console.WriteLine("Operación cancelada.");
            }
        }

        public void pagoServicio(Cuenta cuenta)
        {
            Console.WriteLine("===================================");
            Console.WriteLine("         PAGO DE SERVICIOS         ");
            Console.WriteLine("===================================");
            Console.WriteLine("1. Pago de Luz");
            Console.WriteLine("2. Pago de Agua");
            Console.Write("Seleccione servicio: ");

            int opcion;
            if (!int.TryParse(Console.ReadLine(), out opcion) || opcion < 1 || opcion > 2)
            {
                Console.WriteLine("Opción no válida.");
                return;
            }

            string servicio;
            if (opcion == 1) servicio = "Luz";
            else servicio = "Agua";

            Console.Write("Ingrese código de suministro: ");
            string codigo = Console.ReadLine();

            if (string.IsNullOrEmpty(codigo))
            {
                Console.WriteLine("Código no válido.");
                return;
            }

            decimal monto;
            Console.Write("Ingrese el monto a pagar: S/ ");
            if (!decimal.TryParse(Console.ReadLine(), out monto) || monto <= 0)
            {
                Console.WriteLine("Monto no válido.");
                return;
            }

            if (!cuenta.tieneSaldo(monto))
            {
                Console.WriteLine("Saldo insuficiente.");
                return;
            }

            Console.WriteLine("===================================");
            Console.WriteLine("Servicio: " + servicio);
            Console.WriteLine("Código:   " + codigo);
            Console.WriteLine("Monto:    S/ " + monto);
            Console.WriteLine("===================================");
            Console.Write("¿Confirma el pago? (S/N): ");

            string confirma = Console.ReadLine();

            if (confirma != null && confirma.ToUpper() == "S")
            {
                cuenta.retirar(monto);
                pagosServicio.insertarPago(servicio, codigo, monto);
                Console.WriteLine("Pago de " + servicio + " realizado correctamente.");
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("Operación cancelada.");
            }
        }
        // Método para buscar cuenta destino en transferencias.
        public Cuenta buscarCuentaDestino(string numeroCuenta)
        {
            Cliente cliente = clientes.lista;

            while (cliente != null)
            {
                Cuenta cuentaDestino = cliente.cuentas.buscarCuenta(numeroCuenta);

                if(cuentaDestino != null)
                {
                    return cuentaDestino;
                }

                cliente = cliente.sgte;
            }

            return null;
        }
    }
}

