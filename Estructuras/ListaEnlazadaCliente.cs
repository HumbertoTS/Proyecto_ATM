using System;
using Proyecto_ATM.Modelos;

namespace Proyecto_ATM.Estructuras
{
    internal class ListaEnlazadaCliente
    {
        public Cliente lista;

        public ListaEnlazadaCliente()
        {
            lista = null;
        }

        // Método para insertar un nuevo cliente.
        // Retorna true si se insertó con éxito, false si el DNI ya existe.
        public bool insertaCliente(string dni, string nombre, string apellido, string direccion, string telefono,
                                    string email, string pin, ListaEnlazadaCuenta cuenta)
        {
            // Validar si el cliente ya existe antes de insertarlo.
            if (buscarPorDni(dni) != null)
            {
                return false;
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

            return true;
        }

        // Busca un cliente por su DNI.
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

        // Método para eliminar un cliente por su DNI.
        // Retorna true si se eliminó con éxito, false en caso contrario.
        public bool eliminarCliente(string dni)
        {
            if (lista == null)
            {
                return false;
            }

            // Elimina el primer cliente
            if (lista.dni == dni)
            {
                lista = lista.sgte;
                return true;
            }

            Cliente cliente = lista;

            while (cliente.sgte != null)
            {
                if (cliente.sgte.dni == dni)
                {
                    cliente.sgte = cliente.sgte.sgte;
                    return true;
                }

                cliente = cliente.sgte;
            }

            return false;
        }
    }
}
