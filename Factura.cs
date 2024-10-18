public class Factura : IFacturable
{
    public Reserva Reserva { get; set; }  // Reserva asociada a la factura
    public decimal Impuesto { get; set; }  // Porcentaje de impuesto a aplicar

    public Factura(Reserva reserva, decimal impuesto)
    {
        Reserva = reserva;
        Impuesto = impuesto;
    }

    // Método para calcular el total de la factura
    public decimal CalcularTotal()
    {
        // Cálculo de los días de la estancia
        TimeSpan duracionEstancia = Reserva.FechaSalida - Reserva.FechaEntrada;
        int diasEstancia = duracionEstancia.Days;

        // Calcular el total sin impuestos
        decimal totalSinImpuestos = diasEstancia * Reserva.Habitacion.PrecioPorNoche;

        // Calcular impuestos
        decimal totalConImpuestos = totalSinImpuestos + (totalSinImpuestos * (Impuesto / 100));

        return totalConImpuestos;
    }

    // Método para generar y mostrar la factura
    public void GenerarFactura()
    {
        decimal total = CalcularTotal();

        Console.WriteLine("----- Factura -----");
        Console.WriteLine($"Cliente: {Reserva.Cliente.Nombre} {Reserva.Cliente.Apellido}");
        Console.WriteLine($"Habitación Número: {Reserva.Habitacion.Numero}");
        Console.WriteLine($"Fecha de Entrada: {Reserva.FechaEntrada.ToShortDateString()}");
        Console.WriteLine($"Fecha de Salida: {Reserva.FechaSalida.ToShortDateString()}");
        Console.WriteLine($"Precio por noche: {Reserva.Habitacion.PrecioPorNoche}");
        Console.WriteLine($"Impuesto aplicado: {Impuesto}%");
        Console.WriteLine($"Total a pagar: {total:C}");
    }
}
