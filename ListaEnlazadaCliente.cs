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
            Console.WriteLine($"|{"DNI",-10} | {"Nombre",-15} | {"Apellido",-15} | |{"Teléfono",-12} | {"Email",-15}");
            Console.WriteLine("-----------------------------------------------------------------------------------");


            while (cliente != null)
            {
                
                Console.WriteLine($"|{cliente.dni,-10} | {cliente.nombre,-15} | {cliente.apellido,-15} | {cliente.telefono,-12} | {cliente.email,-15} ");
                cliente = cliente.sgte;           
            }
            Console.WriteLine("-----------------------------------------------------------------------------------");
        }
        //Método para insertar un nuevo cliente.
        public void insertaCliente(int dni, string nombre, string apellido, string direccion, int telefono,
                                    string email, int pin, ListaEnlazadaCuenta cuenta){
            //Validar si el cliente ya existe antes de insertarlo.
            if (buscarPorDni(dni) != null)
            {
                Console.WriteLine("Ya existe un cliente con ese DNI.");
                return;
            }

            Cliente q = new Cliente(dni, nombre, apellido, direccion, telefono,
                                    email, pin, cuenta);

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

        public Cliente buscarPorDni(int dni)
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
        //Método para eliminar un cliente por su DNI.
        public bool eliminarCliente(int dni)
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
    }
}
