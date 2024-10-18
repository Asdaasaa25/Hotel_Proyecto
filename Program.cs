using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Recepcion recepcion = new Recepcion();

        // Agregar algunas habitaciones de ejemplo
        recepcion.AgregarHabitacion(new Habitacion(101, "Simple", 100));
        recepcion.AgregarHabitacion(new Habitacion(102, "Doble", 150));

        bool continuar = true;
        while (continuar)
        {
            Console.WriteLine("\nSeleccione una opción:");
            Console.WriteLine("1. Agregar nuevo cliente");
            Console.WriteLine("2. Ver lista de clientes");
            Console.WriteLine("3. Modificar cliente");
            Console.WriteLine("4. Asignar habitación");
            Console.WriteLine("5. Generar factura");
            Console.WriteLine("6. Salir");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    AgregarClienteManualmente(recepcion);
                    break;
                case "2":
                    ListarClientes(recepcion);
                    break;
                case "3":
                    ModificarCliente(recepcion);
                    break;
                case "4":
                    AsignarHabitacion(recepcion);
                    break;
                case "5":
                    GenerarFactura(recepcion);
                    break;
                case "6":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        }
    }

    static void AgregarClienteManualmente(Recepcion recepcion)
    {
        Console.WriteLine("\n--- Agregar nuevo cliente ---");

        Console.Write("Ingrese el nombre del cliente: ");
        string nombre = Console.ReadLine() ?? string.Empty;

        Console.Write("Ingrese el apellido del cliente: ");
        string apellido = Console.ReadLine() ?? string.Empty;

        Console.Write("Ingrese el email del cliente: ");
        string email = Console.ReadLine() ?? string.Empty;

        Console.Write("Ingrese el teléfono del cliente: ");
        string telefono = Console.ReadLine() ?? string.Empty;

        Cliente nuevoCliente = new Cliente(0, nombre, apellido, email, telefono);
        recepcion.AgregarCliente(nuevoCliente);
        Console.WriteLine("Cliente agregado con éxito.");
    }

    static void ListarClientes(Recepcion recepcion)
    {
        Console.WriteLine("\n--- Lista de clientes ---");
        recepcion.ListarClientes();
    }

    static void ModificarCliente(Recepcion recepcion)
    {
        Console.WriteLine("\n--- Modificar cliente ---");

        Console.Write("Ingrese el ID del cliente que desea modificar: ");
        int idCliente;

        if (int.TryParse(Console.ReadLine(), out idCliente))
        {
            Console.Write("Ingrese el nuevo nombre (dejar en blanco si no desea modificar): ");
            string nuevoNombre = Console.ReadLine() ?? string.Empty;

            Console.Write("Ingrese el nuevo apellido (dejar en blanco si no desea modificar): ");
            string nuevoApellido = Console.ReadLine() ?? string.Empty;

            recepcion.ModificarCliente(idCliente, nuevoNombre, nuevoApellido);
        }
        else
        {
            Console.WriteLine("ID inválido.");
        }
    }

    static void AsignarHabitacion(Recepcion recepcion)
    {
        Console.WriteLine("\n--- Asignar habitación ---");

        Console.Write("Ingrese el ID del cliente: ");
        int idCliente;
        if (!int.TryParse(Console.ReadLine(), out idCliente))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        Cliente cliente = recepcion.BuscarClientePorId(idCliente);

        recepcion.MostrarHabitacionesDisponibles();

        Console.Write("Ingrese el número de la habitación que desea asignar: ");
        int numeroHabitacion;
        if (!int.TryParse(Console.ReadLine(), out numeroHabitacion))
        {
            Console.WriteLine("Número de habitación inválido.");
            return;
        }

        Habitacion habitacion = recepcion.habitaciones.Find(h => h.Numero == numeroHabitacion);
        if (habitacion == null || !habitacion.Disponible)
        {
            Console.WriteLine("La habitación no está disponible o no existe.");
            return;
        }

        string[] formatos = { "dd-MM-yyyy", "dd/MM/yyyy" };

        Console.Write("Ingrese la fecha de entrada (DD-MM-YYYY): ");
        DateTime fechaEntrada;
        if (!DateTime.TryParseExact(Console.ReadLine(), formatos, null, System.Globalization.DateTimeStyles.None, out fechaEntrada))
        {
            Console.WriteLine("Fecha de entrada inválida. El formato correcto es DD-MM-YYYY o DD/MM/YYYY.");
            return;
        }

        Console.Write("Ingrese la fecha de salida (DD-MM-YYYY): ");
        DateTime fechaSalida;
        if (!DateTime.TryParseExact(Console.ReadLine(), formatos, null, System.Globalization.DateTimeStyles.None, out fechaSalida))
        {
            Console.WriteLine("Fecha de salida inválida. El formato correcto es DD-MM-YYYY o DD/MM/YYYY.");
            return;
        }

        recepcion.AsignarHabitacion(cliente, habitacion, fechaEntrada, fechaSalida);
    }

    static void GenerarFactura(Recepcion recepcion)
    {
        Console.WriteLine("\n--- Generar factura ---");

        Console.Write("Ingrese el ID del cliente: ");
        int idCliente;
        if (!int.TryParse(Console.ReadLine(), out idCliente))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        // IVA fijo al 16%
        decimal impuesto = 16m;

        recepcion.GenerarFacturaDeCliente(idCliente, impuesto);
    }
}
