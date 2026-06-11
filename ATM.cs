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
        //Método para iniciar sesión en el ATM.
        public Cliente IniciarSesion(int dni, int pin)
        {
            Cliente cliente = clientes.buscarPorDni(dni);

            if (cliente == null)
            {
                Console.WriteLine("DNI no encontrado.");
                return null;
            }

            if (cliente.pin != pin)
            {
                Console.WriteLine("PIN incorrecto.");
                return null;
            }

            Console.WriteLine($"Bienvenido {cliente.nombre}");
            return cliente;
        }
    }
}
