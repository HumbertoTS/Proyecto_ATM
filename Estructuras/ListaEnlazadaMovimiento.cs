using System;
using Proyecto_ATM.Modelos;

namespace Proyecto_ATM.Estructuras
{
    internal class ListaEnlazadaMovimiento
    {
        public Movimiento lista;

        public ListaEnlazadaMovimiento()
        {
            lista = null;
        }

        public bool estaVacia()
        {
            return lista == null;
        }

        public void agregarMovimiento(Movimiento nuevoMovimiento)
        {
            if (lista == null)
            {
                lista = nuevoMovimiento;
            }
            else
            {
                Movimiento temp = lista;
                while (temp.sgte != null)
                {
                    temp = temp.sgte;
                }
                temp.sgte = nuevoMovimiento;
            }
        }
    }
}
