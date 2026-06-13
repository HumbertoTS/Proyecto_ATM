using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_ATM
{
    internal class ATM
    {
        public ListaEnlazadaCliente clientes;
        //Constructor del ATM que recibe la lista de clientes.
        public ATM(ListaEnlazadaCliente clientes) { 
            
            this.clientes = clientes;            
        }
        //Método para validar pin.
        public bool validarPin(Cliente cliente, int pin)
        {
            return cliente.validarPinAcceso(pin);
        }
        
        public Cliente buscarCliente(int dni)
        {
            return clientes.buscarPorDni(dni);
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
            }
            else
            {
                Console.WriteLine("Operación cancelada.");
            }
        }
    }
}
