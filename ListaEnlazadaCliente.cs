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

        public void insertaCliente(int dni, string nombre, string apellido, string direccion, int telefono,
                                    string email, int pin, Cuenta[] cuentas){
            Cliente q = new Cliente(dni, nombre, apellido, direccion, telefono,
                                    email, pin, cuentas);

            if (lista == null)
            {
                lista = q;
            }
            else
            {
                q.sgte = lista;
                lista = q;
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
    }
}
