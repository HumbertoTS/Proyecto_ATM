using System;
using Proyecto_ATM.Estructuras;

namespace Proyecto_ATM.Modelos
{
    internal class Cliente
    {
        public int dni;
        public string nombre;
        public string apellido;
        public string direccion;
        public int telefono;
        public string email;
        public int pin;
        public bool bloqueado;
        public ListaEnlazadaCuenta cuenta;
        
        public Cliente sgte;

        public Cliente(int dni, string nombre, string apellido,
                       string direccion, int telefono, string email, int pin, ListaEnlazadaCuenta cuenta)
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

        // Método para contar los dígitos.
        // Esto sirve para validar la cantidad de dígitos de DNI, teléfono y PIN.
        public int contarDigitos(int numero)
        {
            int contador = 0;
            while (numero != 0)
            {
                numero /= 10;
                contador++;
            }
            return contador;
        }

        // Método para validar los dígitos del DNI (debe ser de 8 dígitos).
        public bool contarDigitosDni(int dni)
        {
            return contarDigitos(dni) == 8;
        }

        // Método para validar los dígitos del teléfono (debe ser de 9 dígitos).
        public bool contarDigitosTelefono(int telefono)
        {
            return contarDigitos(telefono) == 9;
        }

        // Método para validar los dígitos del PIN (debe ser de 4 dígitos).
        public bool contarDigitosPin(int pin)
        {
            return contarDigitos(pin) == 4;
        }

        // Validar el formato del email, debe contener un "@" y un "." después del "@".
        public bool validarEmail(string email)
        {
            int posicionArroba = email.IndexOf("@");
            int posicionPunto = email.LastIndexOf('.');

            return posicionArroba > 0 && posicionPunto > posicionArroba;
        }

        // Método para validar el pin que ingresa el cliente.
        public bool validarPinAcceso(int pinIngresado)
        {
            return this.pin == pinIngresado;
        }

        // Método para cambiar el pin y validar el nuevo pin.
        public bool cambiarPin(int nuevoPin, out string mensaje)
        {
            if (contarDigitos(nuevoPin) != 4)
            {
                mensaje = "El PIN debe tener exactamente 4 dígitos.";
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
