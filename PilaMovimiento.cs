using System;

namespace Proyecto_ATM
{
    internal class PilaMovimiento
    {
        public Movimiento tope;

        public PilaMovimiento()
        {
            tope = null;
        }

        // Método para registrar un movimiento insertándolo al inicio de la lista
        // Método para registrar un movimiento insertándolo al inicio de la lista (Push)
        public void registrarMovimientoPush(string tipo, decimal monto, string detalle)
        {
            Movimiento nuevo = new Movimiento(tipo, monto, detalle);
            nuevo.sgte = tope;
            if (tope != null)
            {
                tope.ant = nuevo;
            }
            tope = nuevo;
        }

        public Movimiento Peek()
        {
            if (tope == null)
                return null;

            return tope;
        }

        // Método para desapilar (Pop) desvinculando tanto adelante como atrás
        public Movimiento Pop()
        {
            if (tope == null)
                return null;

            Movimiento aux = tope;
            tope = tope.sgte;

            if (tope != null)
            {
                tope.ant = null;
            }

            aux.sgte = null;
            aux.ant = null;

            return aux;
        }

        // Método para contar el número de movimientos en la pila/lista
        public int contarMovimientos()
        {
            int contador = 0;
            Movimiento aux = tope;
            while (aux != null)
            {
                contador++;
                aux = aux.sgte;
            }
            return contador;
        }

        // Método para mostrar el historial de movimientos de la cuenta con navegación
        public void mostrarHistorial()
        {
            if (tope == null)
            {
                Console.WriteLine("No hay movimientos registrados en esta cuenta.");
                return;
            }

            int total = contarMovimientos();

            if (total <= 3)
            {
                Console.WriteLine("-----------------------------------------------------------------------------------------");
                Console.WriteLine($"| {"Fecha y Hora",-20} | {"Operación",-22} | {"Monto",-12} | {"Detalle",-28} |");
                Console.WriteLine("-----------------------------------------------------------------------------------------");

                Movimiento movimiento = tope;
                while (movimiento != null)
                {
                    Console.WriteLine($"| {movimiento.fecha.ToString("dd/MM/yyyy HH:mm:ss"),-20} | {movimiento.tipo,-22} | S/ {movimiento.monto,-9:F2} | {movimiento.detalle,-28} |");
                    movimiento = movimiento.sgte;
                }

                Console.WriteLine("-----------------------------------------------------------------------------------------");
            }
            else
            {
                Movimiento inicioVentana = tope;
                string opcion = "";

                do
                {
                    Console.Clear();
                    Console.WriteLine("=========================================================================================");
                    Console.WriteLine("                              HISTORIAL DE MOVIMIENTOS");
                    Console.WriteLine("=========================================================================================");
                    Console.WriteLine("-----------------------------------------------------------------------------------------");
                    Console.WriteLine($"| {"Fecha y Hora",-20} | {"Operación",-22} | {"Monto",-12} | {"Detalle",-28} |");
                    Console.WriteLine("-----------------------------------------------------------------------------------------");

                    Movimiento actual = inicioVentana;
                    for (int i = 0; i < 3 && actual != null; i++)
                    {
                        Console.WriteLine($"| {actual.fecha.ToString("dd/MM/yyyy HH:mm:ss"),-20} | {actual.tipo,-22} | S/ {actual.monto,-9:F2} | {actual.detalle,-28} |");
                        actual = actual.sgte;
                    }
                    Console.WriteLine("-----------------------------------------------------------------------------------------");

                    bool tieneSiguiente = inicioVentana.sgte != null;
                    bool tieneAnterior = inicioVentana.ant != null;

                    if (tieneSiguiente)
                    {
                        Console.WriteLine("[1] Siguiente movimiento");
                    }
                    if (tieneAnterior)
                    {
                        Console.WriteLine("[2] Anterior movimiento");
                    }
                    Console.WriteLine("[0] Volver al menú principal");
                    Console.Write("Seleccione: ");
                    opcion = Console.ReadLine();

                    if (opcion == "1" && tieneSiguiente)
                    {
                        inicioVentana = inicioVentana.sgte;
                    }
                    else if (opcion == "2" && tieneAnterior)
                    {
                        inicioVentana = inicioVentana.ant;
                    }

                } while (opcion != "0");
            }
        }
    }
}
