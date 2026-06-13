using System;
using Proyecto_ATM.Estructuras;

namespace Proyecto_ATM.Modelos
{
    internal class Cliente
    {
        public string dni;
        public string nombre;
        public string apellido;
        public string direccion;
        public string telefono;
        public string email;
        public string pin;
        public bool bloqueado;
        public ListaEnlazadaCuenta cuenta;
        
        public Cliente sgte;

        public Cliente(string dni, string nombre, string apellido,
                       string direccion, string telefono, string email, string pin, ListaEnlazadaCuenta cuenta)
        {
            this.dni = dni;
            this.nombre = nombre;
            this.apellido = apellido;
            this.direccion = direccion;
            this.telefono = telefono;
            this.email = email;
            this.pin = pin;
            this.cuenta = cuenta;
            this.bloqueado = false;
            this.sgte = null;
        }

        // Método auxiliar para verificar si una cadena contiene solo dígitos.
        private bool esNumerico(string cadena)
        {
            if (string.IsNullOrEmpty(cadena)) return false;
            foreach (char c in cadena)
            {
                if (!char.IsDigit(c)) return false;
            }
            return true;
        }

        // Método para validar los dígitos del DNI (debe ser de 8 dígitos numéricos).
        public bool contarDigitosDni(string dni)
        {
            return esNumerico(dni) && dni.Length == 8;
        }

        // Método para validar los dígitos del teléfono (debe ser de 9 dígitos numéricos).
        public bool contarDigitosTelefono(string telefono)
        {
            return esNumerico(telefono) && telefono.Length == 9;
        }

        // Método para validar los dígitos del PIN (debe ser de 4 dígitos numéricos).
        public bool contarDigitosPin(string pin)
        {
            return esNumerico(pin) && pin.Length == 4;
        }

        // Validar el formato del email, debe contener un "@" y un "." después del "@".
        public bool validarEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            int posicionArroba = email.IndexOf("@");
            int posicionPunto = email.LastIndexOf('.');

            return posicionArroba > 0 && posicionPunto > posicionArroba;
        }

        // Método para validar el pin que ingresa el cliente.
        public bool validarPinAcceso(string pinIngresado)
        {
            return this.pin == pinIngresado;
        }

        // Método para cambiar el pin y validar el nuevo pin.
        public bool cambiarPin(string nuevoPin, out string mensaje)
        {
            if (!esNumerico(nuevoPin) || nuevoPin.Length != 4)
            {
                mensaje = "El PIN debe tener exactamente 4 dígitos numéricos.";
                return false;
            }
            if (this.pin == nuevoPin)
            {
                mensaje = "El nuevo PIN no puede ser igual al PIN actual.";
                return false;
            }
            this.pin = nuevoPin;
            mensaje = "PIN cambiado exitosamente.";
            return true;
        }

        // Método para bloquear al cliente.
        public void bloquearCliente()
        {
            bloqueado = true;
        }

        // Método para desbloquear al cliente.
        public void desbloquearCliente()
        {
            bloqueado = false;
        }

        // Método que verifica el estado de bloqueo del cliente.
        public bool verificarBloqueo()
        {
            return bloqueado;
        }
    }
}
