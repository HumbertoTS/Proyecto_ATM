using System;
using Proyecto_ATM.Estructuras;

namespace Proyecto_ATM.Modelos
{
    internal class Cuenta
    {
        public int numeroCuenta;
        public string tipoCuenta;
        private decimal saldo;
        public ListaEnlazadaMovimiento movimientos;

        public Cuenta sgte;

        public Cuenta(int numeroCuenta, string tipoCuenta, decimal saldo)
        {
            this.numeroCuenta = numeroCuenta;
            this.tipoCuenta = tipoCuenta;
            this.saldo = saldo;
            this.movimientos = new ListaEnlazadaMovimiento();
            this.sgte = null;
        }

        // Método para consultar el saldo de la cuenta.
        public decimal consultarSaldo()
        {
            return saldo;
        }

        // Método para verificar si la cuenta tiene suficiente saldo.
        public bool tieneSaldo(decimal monto)
        {
            return saldo >= monto;
        }

        // Método para depositar dinero en la cuenta.
        public void depositar(decimal monto)
        {
            saldo += monto;
        }

        // Método para retirar dinero de la cuenta.
        public bool retirar(decimal monto)
        {
            if (saldo >= monto)
            {
                saldo -= monto;
                return true;
            }

            return false;
        }
    }
}
