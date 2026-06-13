# Proyecto Cajero Automático (ATM) - Estructura de Datos

Este es un proyecto académico de consola desarrollado en **C#** para el curso de **Estructura de Datos** (Universidad Privada del Norte - UPN). Su objetivo principal es simular el funcionamiento lógico de un Cajero Automático (ATM) implementando **estructuras de datos lineales dinámicas de forma personalizada (Listas Enlazadas Simples)** en lugar de colecciones nativas de .NET.

---

## 👥 Integrantes del Equipo

* **[Nombre del Integrante 1]** - [Código de Estudiante 1]
* **[Nombre del Integrante 2]** - [Código de Estudiante 2]
* **[Nombre del Integrante 3]** - [Código de Estudiante 3]
* **[Nombre del Integrante 4]** - [Código de Estudiante 4]

---

## 🏗️ Arquitectura y Estructura del Código

El proyecto sigue una estructura limpia organizada en capas de software para separar las responsabilidades (Desacoplamiento de Interfaz de Usuario y Estructuras de Datos):

```
Proyecto_ATM/
│
├── Modelos/                       # Objetos de negocio (Entidades / Nodos)
│   ├── Cliente.cs                 # Datos del cliente (DNI, PIN, etc.) y validaciones
│   ├── Cuenta.cs                  # Representación de cuentas bancarias
│   └── Movimiento.cs              # Representación de transacciones (Depósitos/Retiros)
│
├── Estructuras/                   # Estructuras de datos lineales dinámicas (Listas Enlazadas)
│   ├── ListaEnlazadaCliente.cs    # Lista simple de Clientes
│   ├── ListaEnlazadaCuenta.cs     # Lista simple de Cuentas
│   └── ListaEnlazadaMovimiento.cs # Lista simple de Movimientos (Historial)
│
├── Servicios/                     # Reglas de negocio y cargadores
│   ├── ATM.cs                     # Procesador lógico del cajero (transacciones)
│   └── CargadorDatos.cs           # Lector y parseador JSON a las listas en memoria
│
├── UI/                            # Capa de Interacción / Consola
│   └── Menu.cs                    # Renderizado de menús de usuario, entradas y salidas
│
├── Program.cs                     # Punto de entrada de la aplicación
├── datos_iniciales.json           # Base de datos inicial en texto plano (JSON)
└── Proyecto_ATM.csproj            # Archivo de configuración del proyecto C#
```

---

## ⚙️ Características Implementadas

1. **Carga de Datos desde JSON a Memoria:** Al iniciar la aplicación, el servicio lee el archivo `datos_iniciales.json` y rellena las listas enlazadas en memoria. Toda la ejecución posterior ocurre al 100% sobre estas listas.
2. **Uso de Tipos String para Identificación:** El DNI, PIN y Teléfono son cadenas de texto (`string`), previniendo la pérdida de ceros a la izquierda y optimizando las validaciones.
3. **Inicio de Sesión y Bloqueo de Tarjeta:**
   * Entrada enmascarada del PIN con asteriscos (`*`).
   * Validación rigurosa de intentos de PIN (cualquier entrada vacía o con letras cuenta como intento fallido).
   * Bloqueo del cliente al acumular **3 intentos fallidos**.
4. **Consulta de Saldos:** Visualiza el saldo en tiempo real de cualquiera de las cuentas del cliente.
5. **Depósitos en Efectivo:** Incrementa el saldo de la cuenta elegida y añade la transacción al historial.
6. **Retiros:** Valida saldo suficiente, descuenta fondos y añade la transacción al historial.
7. **Transferencias Interbancarias:** Permite transferir montos entre cuentas de cualquier usuario del banco (búsqueda global de cuentas), registrando movimientos cruzados de envío y recepción.
8. **Historial de Movimientos:** Muestra un reporte en formato de tabla en consola que despliega la fecha, hora, tipo de operación, monto y detalles de las transacciones realizadas.
9. **Cambio de PIN:** Permite actualizar la clave secreta previa validación de longitud (exactamente 4 dígitos numéricos) y diferencia con el PIN actual.

---

## 📂 Configuración del Archivo JSON (`datos_iniciales.json`)

El archivo JSON almacena los clientes iniciales con la siguiente estructura:
```json
[
  {
    "dni": "01234567",
    "nombre": "Carlos",
    "apellido": "Rodríguez",
    "direccion": "Jr. Trujillo 789",
    "telefono": "955667788",
    "email": "carlos@gmail.com",
    "pin": "0123",
    "cuentas": [
      {
        "numeroCuenta": 3001,
        "tipoCuenta": "Ahorros",
        "saldo": 3000.00
      }
    ]
  }
]
```

### 👤 Usuarios de Prueba Pre-Cargados:
* **Cliente 1:** Juan Pérez (DNI: `12345678`, PIN: `1234`)
* **Cliente 2:** María Gómez (DNI: `87654321`, PIN: `4321`)
* **Cliente 3:** Carlos Rodríguez (DNI: `01234567` (con cero a la izquierda), PIN: `0123`)

---

## 🚀 Cómo Compilar y Ejecutar

### Requisitos:
* **Visual Studio** (2019 / 2022 / 2026) con soporte para desarrollo de escritorio de .NET.
* **.NET Framework 4.7.2** o superior instalado.

### Pasos:
1. Abre el archivo de solución `Proyecto_ATM.slnx` o el archivo `Proyecto_ATM.csproj` en Visual Studio.
2. Si te solicita recargar el archivo de configuración del proyecto, haz clic en **Recargar** (*Reload*).
3. Presiona la tecla **F5** o haz clic en el botón **Iniciar / Depurar** (botón verde de reproducción) en la barra de herramientas.
4. El ejecutable se iniciará en una ventana de consola clásica de Windows y copiará automáticamente el archivo `datos_iniciales.json` al directorio de compilación `/bin/Debug` para su lectura.
