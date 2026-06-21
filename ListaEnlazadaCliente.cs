using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_ATM
{
    internal class ListaEnlazadaCliente
    {
        public Cliente lista;
        public ListaEnlazadaCliente()
        {
            lista = null;
        }
        // Método para mostrar todos los clientes.
        public void mostrarClientes()
        {
            Cliente cliente = lista;

            if(cliente == null)
            {
                Console.WriteLine("No hay clientes registrados.");
                return;
            }

            //Cabecera de la tabla para mostrar los clientes.
            Console.WriteLine("-----------------------------------------------------------------------------------");
            Console.WriteLine($"|{"DNI",-10} | {"Nombre",-15} | {"Apellido",-15} | {"Teléfono",-12} | {"Email",-15}");
            Console.WriteLine("-----------------------------------------------------------------------------------");


            while (cliente != null)
            {
                
                Console.WriteLine($"|{cliente.dni,-10} | {cliente.nombre,-15} | {cliente.apellido,-15} | {cliente.telefono,-12} | {cliente.email,-15} ");
                cliente = cliente.sgte;           
            }
            Console.WriteLine("-----------------------------------------------------------------------------------");
        }
        //Método para insertar un nuevo cliente.
        public void insertaCliente(string dni, string nombre, string apellido, string direccion, int telefono,
                                    string email, string tarjeta, string pin, bool bloqueado, ListaEnlazadaCuenta cuenta)
        {
            //Validar si el cliente ya existe antes de insertarlo.
            if (buscarPorDni(dni) != null)
            {
                Console.WriteLine("Ya existe un cliente con ese DNI.");
                return;
            }

            if(!Cliente.validarDni(dni))
            {
                Console.WriteLine("El DNI debe tener 8 dígitos.");
                return;
            }

            if (!Cliente.contarDigitosTelefono(telefono))
            {
                Console.WriteLine("Teléfono inválido.");
                return;
            }

            if (!Cliente.validarEmail(email))
            {
                Console.WriteLine("Correo electrónico inválido.");
                return;
            }

            if (!Cliente.validarPin(pin))
            {
                Console.WriteLine("PIN inválido.");
                return;
            }

            Cliente q = new Cliente(dni, nombre, apellido, direccion, telefono,
                                    email, tarjeta, pin, bloqueado, cuenta);

            if (lista == null)
            {
                lista = q;
            }
            else
            {
                Cliente cliente = lista;
                while (cliente.sgte != null)
                {
                    cliente = cliente.sgte;
                }
                cliente.sgte = q;
            }
        }

        public Cliente buscarPorDni(string dni)
        {
            Cliente cliente = lista;

            while (cliente != null)
            {
                if (cliente.dni == dni)
                {
                    return cliente;
                }

                cliente = cliente.sgte;
            }

            return null;
        }

         public Cliente buscarPorTarjeta(string tarjeta)
         {
             Cliente cliente = lista;

             while (cliente != null)
             {
                 if (cliente.tarjeta == tarjeta)
                 {
                     return cliente;
                 }

                 cliente = cliente.sgte;
             }

             return null;
         }              

        //Método para eliminar un cliente por su DNI.
        public bool eliminarCliente(string dni)
        {
            if (lista == null)
            {
                Console.WriteLine("No hay clientes registrados.");
                return false;
            }
            // Elimina el primer cliente
            if (lista.dni == dni)
            {
                lista = lista.sgte;
                Console.WriteLine("Cliente eliminado correctamente.");
                return true;
            }

            Cliente cliente = lista;

            while (cliente.sgte != null)
            {
                if (cliente.sgte.dni == dni)
                {
                    cliente.sgte = cliente.sgte.sgte;
                    Console.WriteLine("Cliente eliminado correctamente.");
                    return true;
                }

                cliente = cliente.sgte;
            }

            Console.WriteLine("Cliente no encontrado.");
            return false;
        }
               

        public void reporteClientes()
        {
            Cliente cliente = lista;

            if (cliente == null)
            {
                Console.WriteLine("No hay clientes registrados.");
                return;
            }


            int contador = 0;


            Console.WriteLine("================================================");
            Console.WriteLine("              REPORTE DE CLIENTES");
            Console.WriteLine("================================================");


            while (cliente != null)
            {
                contador++;

                Console.WriteLine("\nCliente N° " + contador);
                Console.WriteLine("DNI: " + cliente.dni);
                Console.WriteLine("Nombre: " + cliente.nombre + " " + cliente.apellido);
                Console.WriteLine("Teléfono: " + cliente.telefono);
                Console.WriteLine("Email: " + cliente.email);

                if (string.IsNullOrEmpty(cliente.tarjeta))
                {
                    Console.WriteLine("Tarjeta: Sin asignar");
                }
                else
                {
                    Console.WriteLine("Tarjeta: " + cliente.tarjeta);
                }


                if (cliente.cuentas != null)
                {
                    Console.WriteLine("Cuentas:");
                    Console.WriteLine("--------------------------------");

                    cliente.cuentas.mostrarCuentasReporte();
                }
                else
                {
                    Console.WriteLine("Sin cuentas registradas.");
                }


                Console.WriteLine("--------------------------------");


                cliente = cliente.sgte;
            }

            Console.WriteLine("Total de clientes: " + contador);
        }
    }
}
