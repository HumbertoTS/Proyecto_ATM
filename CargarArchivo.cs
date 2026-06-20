using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_ATM
{
    internal class CargarArchivo
    {
        private ListaEnlazadaCliente clientes;

        public CargarArchivo(ListaEnlazadaCliente clientes)
        {
            this.clientes = clientes;
        }
        public void cargarArchivo(string ruta)
        {
            if (!File.Exists(ruta))
            {
                Console.WriteLine("Archivo no encontrado.");
                return;
            }


            string[] lineas = File.ReadAllLines(ruta);


            foreach (string linea in lineas)
            {
                string[] datos = linea.Split(';');

                int posicion = 0;

                string dni = datos[posicion++];
                string nombre = datos[posicion++];
                string apellido = datos[posicion++];
                string direccion = datos[posicion++];
                int telefono = int.Parse(datos[posicion++]);
                string email = datos[posicion++];
                string tarjeta = datos[posicion++];
                string pin = datos[posicion++];
                bool bloqueado = bool.Parse(datos[posicion++]);


                int cantidadCuentas = int.Parse(datos[posicion++]);


                ListaEnlazadaCuenta cuentas = new ListaEnlazadaCuenta();

                for (int i = 0; i < cantidadCuentas; i++)
                {
                    string numeroCuenta = datos[posicion++];
                    string tipoCuenta = datos[posicion++];
                    decimal saldo = decimal.Parse(datos[posicion++]);

                    Cuenta nueva = new Cuenta(numeroCuenta, tipoCuenta, saldo);

                    cuentas.agregarCuenta(nueva);
                }                               

                clientes.insertaCliente(
                    dni,
                    nombre,
                    apellido,
                    direccion,
                    telefono,
                    email,
                    tarjeta,
                    pin,
                    bloqueado,
                    cuentas
                );
            }
        }
    }
}
