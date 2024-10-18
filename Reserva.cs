public class Reserva
{
    public Cliente Cliente { get; set; }
    public Habitacion Habitacion { get; set; }
    public DateTime FechaEntrada { get; set; }
    public DateTime FechaSalida { get; set; }

    public Reserva(Cliente cliente, Habitacion habitacion, DateTime fechaEntrada, DateTime fechaSalida)
    {
        Cliente = cliente;
        Habitacion = habitacion;
        FechaEntrada = fechaEntrada;
        FechaSalida = fechaSalida;
    }
}
