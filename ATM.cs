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
    }
}
