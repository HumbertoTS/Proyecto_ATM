using System;
using Proyecto_ATM.Modelos;
using Proyecto_ATM.Estructuras;
using Proyecto_ATM.Servicios;
using Proyecto_ATM.UI;

namespace Proyecto_ATM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Cargar la lista de clientes y sus cuentas desde el JSON directamente a memoria
            string rutaJson = "datos_iniciales.json";
            ListaEnlazadaCliente listaClientes = CargadorDatos.CargarClientesDesdeJson(rutaJson);

            // Crear el ATM
            ATM atm = new ATM(listaClientes);

            // Crear el menú
            Menu menu = new Menu(atm);

            // Iniciar sesión (ciclo infinito del cajero)
            while (true)
            {
                menu.iniciarSesion();
            }
        }
    }
}
