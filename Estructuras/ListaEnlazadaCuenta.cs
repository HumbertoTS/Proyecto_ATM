using System;
using Proyecto_ATM.Modelos;

namespace Proyecto_ATM.Estructuras
{
    internal class ListaEnlazadaCuenta
    {
        public Cuenta lista;

        public ListaEnlazadaCuenta()
        {
            lista = null;
        }

        // Método para verificar si la lista de cuentas está vacía.
        public bool estaVacia()
        {
            return lista == null;
        }

        // Método para agregar una cuenta.
        public void agregarCuenta(Cuenta nuevaCuenta)
        {
            if (lista == null)
            {
                lista = nuevaCuenta;
            }
            else
            {
                Cuenta cuenta = lista;
                while (cuenta.sgte != null)
                {
                    cuenta = cuenta.sgte;
                }
                cuenta.sgte = nuevaCuenta;
            }
        }

        // Método para buscar una cuenta por número.
        public Cuenta buscarCuenta(int numeroCuenta)
        {
            Cuenta cuenta = lista;
            while (cuenta != null)
            {
                if (cuenta.numeroCuenta == numeroCuenta)
                {
                    return cuenta;
                }
                cuenta = cuenta.sgte;
            }
            return null;
        }

        // Método para eliminar una cuenta por su número de cuenta.
        public bool eliminarCuenta(int numeroCuenta)
        {
            if (lista == null)
            {
                return false;
            }

            // Elimina la primera cuenta
            if (lista.numeroCuenta == numeroCuenta)
            {
                lista = lista.sgte;
                return true;
            }

            Cuenta cuenta = lista;
            while (cuenta.sgte != null)
            {
                if (cuenta.sgte.numeroCuenta == numeroCuenta)
                {
                    cuenta.sgte = cuenta.sgte.sgte;
                    return true;
                }
                cuenta = cuenta.sgte;
            }

            return false;
        }

        // Busca la posición de la cuenta en la lista.
        public Cuenta buscarporPosicion(int posicion)
        {
            Cuenta cuenta = lista;
            int contador = 1;
            while (cuenta != null)
            {
                if (contador == posicion)
                {
                    return cuenta;
                }
                contador++;
                cuenta = cuenta.sgte;
            }
            return null; // Si no se encuentra la posición, devuelve null.
        }

        // Método para contar el número de cuentas en la lista.
        public int contarCuentas()
        {
            int contador = 0;
            Cuenta cuenta = lista;
            while (cuenta != null)
            {
                contador++;
                cuenta = cuenta.sgte;
            }
            return contador;
        }
    }
}
