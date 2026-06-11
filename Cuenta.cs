using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_ATM
{
    internal class Cuenta
    {
        public int numeroCuenta;
        public string tipoCuenta;
        public decimal saldo;
        public Movimiento[] movimientos; //se debe cambiar cuando se tenga listo la lista de movimientos

        public Cuenta sgte;

        public Cuenta(int numeroCuenta, string tipoCuenta, decimal saldo)
        {
            this.numeroCuenta = numeroCuenta;
            this.tipoCuenta = tipoCuenta;
            this.saldo = saldo;
        }
        //Método para consultar el saldo de la cuenta.
        public decimal consultarSaldo()
        {
            return saldo;
        }
        //Método para verificar si la cuenta tiene suficiente saldo.
        public bool tieneSaldo(decimal monto)
        {
            return saldo >= monto;
        }
        //Método para depositar dinero en la cuenta.
        public void depositar(decimal monto)
        {
            saldo += monto;
        }
        //Método para retirar dinero de la cuenta.
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
