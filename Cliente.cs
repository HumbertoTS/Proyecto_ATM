using System;

namespace Proyecto_ATM
{
    internal class Cliente
    {
        public string dni;
        public string nombre;
        public string apellido;
        public string direccion;
        public int telefono;
        public string email;
        public string pin;
        public bool bloqueado;
        public ListaEnlazadaCuenta cuentas;

        public Cliente sgte;

        public Cliente(String dni, String nombre, String apellido,
                    String direccion, int telefono, String email, String pin, bool bloqueado, ListaEnlazadaCuenta cuentas)
        {
            this.dni = dni;
            this.nombre = nombre;
            this.apellido = apellido;
            this.direccion = direccion;
            this.telefono = telefono;
            this.email = email;
            this.pin = pin;
            this.bloqueado = bloqueado;
            this.cuentas = cuentas;

            this.sgte = null;
        }
        //Método para contar los digitos.
        //Esto va a servir para validar la cantidad de digitos de DNI, telefono y pin.
        //deben ser de 8, 9 y 4 dígitos respectivamente.
        public static int contarDigitos(int numero)
        {
            if (numero == 0)
            { 
                return 1; 
            }

            int contador = 0;
            while (numero != 0)
            {
                numero /= 10;
                contador++;
            }
            return contador;
        }

        //Método para validar los digitos de dni.
        public static bool validarDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
            {
                return false;
            }

            if (dni.Length != 8)
            {
                return false;
            }

            foreach (char caracter in dni)
            {
                if (!char.IsDigit(caracter))
                {
                    return false;
                }
            }

            return true;
        }

        //Método para validar los digitos de telefono.
        public static bool contarDigitosTelefono(int telefono)
        {
            return telefono > 0 && contarDigitos(telefono) == 9;
        }

        //Método para validar los digitos de pin.
        public static bool validarPin(string pin)
        {
            if (string.IsNullOrWhiteSpace(pin))
            {
                return false;
            }

            if (pin.Length != 4)
            {
                return false;
            }

            foreach (char caracter in pin)
            {
                if (!char.IsDigit(caracter))
                {
                    return false;
                }
            }

            return true;
        }

        //Validar el formato del email, debe contener un "@" y un "." después del "@".
        //También valida que exista texto antes del "@".
        //Además, busca que exista el "." después del "@" por si existe un dominio.
        public static bool validarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            { 
                return false; 
            }

            int posicionArroba = email.IndexOf('@');
            int posicionPunto = email.LastIndexOf('.');

            return posicionArroba > 0 &&
                   posicionPunto > posicionArroba + 1 &&
                   posicionPunto < email.Length - 1;
        }
        //Método para validar el pin que ingresa el cliente.
        public bool validarPinAcceso(string pinIngresado)
        {
            return pin == pinIngresado;
        }

        //Método para bloquear al cliente.
        public void bloquearCliente()
        {
            bloqueado = true;
        }

        //Método para desbloquea al cliente.
        public void desbloquearCliente()
        {
            bloqueado = false;
        }

        //Método que verifica el estado de bloqueo del cliente.
        public bool verificarBloqueo()
        {
            return bloqueado;
        }

        //Se debe agregar un método para cambiar pin y validar el nuevo pin, debe ser de 4 dígitos y no puede ser igual al pin anterior.

    }
}
