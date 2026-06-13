using System;
using Proyecto_ATM.Modelos;
using Proyecto_ATM.Estructuras;

namespace Proyecto_ATM.Servicios
{
    internal class ATM
    {
        public ListaEnlazadaCliente clientes;

        // Constructor del ATM que recibe la lista de clientes.
        public ATM(ListaEnlazadaCliente clientes)
        {
            this.clientes = clientes;
        }

        // Método para validar PIN.
        public bool validarPin(Cliente cliente, int pin)
        {
            return cliente.validarPinAcceso(pin);
        }

        // Busca un cliente por DNI.
        public Cliente buscarCliente(int dni)
        {
            return clientes.buscarPorDni(dni);
        }

        // Busca una cuenta de forma global en todo el banco (todos los clientes).
        // Útil para realizar transferencias interbancarias o entre clientes.
        public Cuenta buscarCuentaGlobal(int numeroCuenta)
        {
            Cliente tempCliente = clientes.lista;
            while (tempCliente != null)
            {
                Cuenta tempCuenta = tempCliente.cuenta.buscarCuenta(numeroCuenta);
                if (tempCuenta != null)
                {
                    return tempCuenta;
                }
                tempCliente = tempCliente.sgte;
            }
            return null;
        }

        // Método para realizar un Retiro.
        public bool realizarRetiro(Cuenta cuenta, decimal monto, out string mensaje)
        {
            if (monto <= 0)
            {
                mensaje = "El monto a retirar debe ser mayor a cero.";
                return false;
            }

            if (!cuenta.tieneSaldo(monto))
            {
                mensaje = "Saldo insuficiente para realizar esta transacción.";
                return false;
            }

            if (cuenta.retirar(monto))
            {
                Movimiento nuevoMov = new Movimiento("Retiro", monto, "Retiro en Cajero Automático");
                cuenta.movimientos.agregarMovimiento(nuevoMov);
                mensaje = "Retiro realizado con éxito.";
                return true;
            }

            mensaje = "Error al procesar el retiro.";
            return false;
        }

        // Método para realizar una Transferencia.
        public bool realizarTransferencia(Cuenta origen, Cuenta destino, decimal monto, out string mensaje)
        {
            if (origen.numeroCuenta == destino.numeroCuenta)
            {
                mensaje = "No se puede transferir a la misma cuenta de origen.";
                return false;
            }

            if (monto <= 0)
            {
                mensaje = "El monto a transferir debe ser mayor a cero.";
                return false;
            }

            if (!origen.tieneSaldo(monto))
            {
                mensaje = "Saldo insuficiente en la cuenta de origen.";
                return false;
            }

            if (origen.retirar(monto))
            {
                destino.depositar(monto);

                // Registrar movimientos en ambas cuentas
                Movimiento movOrigen = new Movimiento("Transferencia Enviada", monto, $"Destino: Cuenta N° {destino.numeroCuenta}");
                Movimiento movDestino = new Movimiento("Transferencia Recibida", monto, $"Origen: Cuenta N° {origen.numeroCuenta}");

                origen.movimientos.agregarMovimiento(movOrigen);
                destino.movimientos.agregarMovimiento(movDestino);

                mensaje = "Transferencia realizada con éxito.";
                return true;
            }

            mensaje = "Error al procesar la transferencia.";
            return false;
        }
    }
}
