using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Proyecto_ATM.Modelos;
using Proyecto_ATM.Estructuras;

namespace Proyecto_ATM.Servicios
{
    internal static class CargadorDatos
    {
        public static ListaEnlazadaCliente CargarClientesDesdeJson(string rutaArchivo)
        {
            ListaEnlazadaCliente listaClientes = new ListaEnlazadaCliente();

            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine($"[ADVERTENCIA] No se encontró el archivo {rutaArchivo}. Se iniciará con una lista vacía.");
                return listaClientes;
            }

            try
            {
                string jsonTexto = File.ReadAllText(rutaArchivo);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                
                // Deserializar el JSON a una arreglo de objetos (diccionarios)
                object[] datosClientes = (object[])serializer.DeserializeObject(jsonTexto);

                if (datosClientes != null)
                {
                    foreach (object item in datosClientes)
                    {
                        Dictionary<string, object> c = (Dictionary<string, object>)item;

                        string dni = c["dni"].ToString();
                        string nombre = c["nombre"].ToString();
                        string apellido = c["apellido"].ToString();
                        string direccion = c["direccion"].ToString();
                        string telefono = c["telefono"].ToString();
                        string email = c["email"].ToString();
                        string pin = c["pin"].ToString();

                        // Inicializar lista enlazada de cuentas para este cliente en memoria
                        ListaEnlazadaCuenta listaCuentas = new ListaEnlazadaCuenta();

                        if (c.ContainsKey("cuentas"))
                        {
                            object[] datosCuentas = (object[])c["cuentas"];
                            if (datosCuentas != null)
                            {
                                foreach (object cuentaItem in datosCuentas)
                                {
                                    Dictionary<string, object> cue = (Dictionary<string, object>)cuentaItem;

                                    int numeroCuenta = Convert.ToInt32(cue["numeroCuenta"]);
                                    string tipoCuenta = cue["tipoCuenta"].ToString();
                                    decimal saldo = Convert.ToDecimal(cue["saldo"]);

                                    // Instanciar cuenta en memoria
                                    Cuenta cuentaObj = new Cuenta(numeroCuenta, tipoCuenta, saldo);
                                    listaCuentas.agregarCuenta(cuentaObj);
                                }
                            }
                        }

                        // Guardar en la estructura de lista enlazada de clientes en memoria
                        listaClientes.insertaCliente(
                            dni,
                            nombre,
                            apellido,
                            direccion,
                            telefono,
                            email,
                            pin,
                            listaCuentas
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al procesar el archivo JSON: " + ex.Message);
            }

            return listaClientes;
        }
    }
}
