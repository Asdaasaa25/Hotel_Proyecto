public class Recepcion
{
    public List<Habitacion> habitaciones = new List<Habitacion>(); // Lista para habitaciones
    private List<Cliente> clientes = new List<Cliente>(); // Lista para clientes
    private List<Reserva> reservas = new List<Reserva>(); // Lista para reservas

    // Campo privado para contar IDs de clientes
    private int contadorIdClientes = 1;

    // Método para agregar un cliente
    public void AgregarCliente(Cliente cliente)
    {
        // Asignar un ID único al cliente
        cliente.Id = contadorIdClientes;
        contadorIdClientes++;  // Incrementar el contador para el siguiente cliente

        // Agregar el cliente a la lista
        clientes.Add(cliente);
        Console.WriteLine($"Cliente {cliente.Nombre} {cliente.Apellido} agregado con éxito. ID asignado: {cliente.Id}");
    }

    // Método para buscar un cliente por su ID
    public Cliente BuscarClientePorId(int id)
    {
        foreach (var cliente in clientes)
        {
            if (cliente.Id == id)
                return cliente;
        }
        throw new Exception("Cliente no encontrado.");
    }

    internal void AgregarHabitacion(Habitacion habitacion)
    {
        // Agregar la habitación a la lista de habitaciones
        habitaciones.Add(habitacion);
        Console.WriteLine($"Habitación {habitacion.Numero} - {habitacion.Tipo} agregada con éxito.");
    }


    internal void AsignarHabitacion(Cliente cliente, Habitacion habitacion, DateTime fechaEntrada, DateTime fechaSalida)
    {
        // Crear una nueva reserva para el cliente y la habitación
        Reserva nuevaReserva = new Reserva(cliente, habitacion, fechaEntrada, fechaSalida);

        // Agregar la reserva a la lista de reservas
        reservas.Add(nuevaReserva);

        // Marcar la habitación como no disponible
        habitacion.Disponible = false;

        Console.WriteLine($"Habitación {habitacion.Numero} asignada a {cliente.Nombre} {cliente.Apellido} desde" +
            $" {fechaEntrada.ToShortDateString()} hasta {fechaSalida.ToShortDateString()}.");
    }


    internal void GenerarFacturaDeCliente(int idCliente, decimal impuesto)
    {
        // Buscar la reserva del cliente por su ID
        var reserva = reservas.Find(r => r.Cliente.Id == idCliente);

        if (reserva == null)
        {
            Console.WriteLine("No se encontró una reserva para este cliente.");
            return;
        }

        // Crear la factura con la reserva encontrada y el impuesto
        Factura factura = new Factura(reserva, impuesto);

        // Generar la factura
        factura.GenerarFactura();
    }


    internal void ListarClientes()
    {
        if (clientes.Count == 0)
        {
            Console.WriteLine("No hay clientes registrados.");
            return;
        }

        foreach (var cliente in clientes)
        {
            // Verificar si el cliente tiene una habitación asignada (una reserva)
            var reserva = reservas.Find(r => r.Cliente.Id == cliente.Id);
            string habitacionAsignada = reserva != null ? reserva.Habitacion.Numero.ToString() : "No asignada";

            Console.WriteLine($"ID: {cliente.Id}, Nombre: {cliente.Nombre}, Apellido: {cliente.Apellido}, Habitación asignada: {habitacionAsignada}");
        }
    }


    internal void ModificarCliente(int idCliente, string nuevoNombre, string nuevoApellido)
    {
        Cliente cliente = BuscarClientePorId(idCliente);

        if (!string.IsNullOrEmpty(nuevoNombre))
        {
            cliente.Nombre = nuevoNombre;
        }

        if (!string.IsNullOrEmpty(nuevoApellido))
        {
            cliente.Apellido = nuevoApellido;
        }

        Console.WriteLine("Cliente modificado con éxito.");
    }


    internal void MostrarHabitacionesDisponibles()
    {
        foreach (var habitacion in habitaciones)
        {
            if (habitacion.Disponible)
            {
                Console.WriteLine($"Habitación {habitacion.Numero} - Tipo: {habitacion.Tipo} - Precio por noche: {habitacion.PrecioPorNoche}");
            }
        }
    }


    // Otros métodos (ListarClientes, AsignarHabitacion, etc.) permanecen sin cambios
}
