using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_ATM
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
        public Cuenta[] cuentas;

        public Cliente(int dni, String nombre, String apellido,
                    String direccion, int telefono, String email, int pin, Cuenta[] cuentas)
        {
            this.dni = dni;
            this.nombre = nombre;
            this.apellido = apellido;
            this.direccion = direccion;
            this.telefono = telefono;
            this.email = email;
            this.pin = pin;
            this.cuentas = cuentas;
        }
        //Método para contar los digitos.
        //Esto va a servir para validar la cantidad de digitos de DNI, telefono y pin.
        //deben ser de 8, 9 y 4 dígitos respectivamente.
        public int ContarDigitos(int numero)
        {
            int contador = 0;
            while (numero != 0)
            {
                numero /= 10;
                contador++;
            }
            return contador;
        }

        //Método para validar los digitos de dni.
        public bool ContarDigitosDni(int dni)
        {
            return ContarDigitos(dni) == 8;
        }

        //Método para validar los digitos de telefono.
        public bool ContarDigitosTelefono(int telefono)
        {
            return ContarDigitos(telefono) == 9;
        }

        //Método para validar los digitos de pin.
        public bool ContarDigitosPin(int pin)
        {
            return ContarDigitos(pin) == 4;
        }

        //Validar el formato del email, debe contener un "@" y un "." después del "@".
        //También valida que exista texto antes del "@".
        //Además, busca que exista el "." después del "@" por si existe un dominio.
        public bool ValidarEmail(String email)
        {
            int posicionArroba = email.IndexOf("@");
            int posicionPunto = email.LastIndexOf('.');

            return posicionArroba > 0 &&
                   posicionPunto > posicionArroba;
        }
        //Método para validar el pin que ingresa el cliente.
        public bool ValidarPinAcceso(int pinIngresado)
        {
            return this.pin == pinIngresado;
        }

        public void BloquearCliente()
        {
            bloqueado = true;
        }

        //Método que valida si el cliente esta bloqueado o no.
        public bool EstaBloqueado()
        {
            return bloqueado;
        }


    }
}
