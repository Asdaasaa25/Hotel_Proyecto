public class Habitacion : IReservable
{
    public int Numero { get; set; }
    public string Tipo { get; set; }
    public decimal PrecioPorNoche { get; set; }
    public bool Disponible { get; set; }

    public Habitacion(int numero, string tipo, decimal precioPorNoche, bool disponible = true)
    {
        Numero = numero;
        Tipo = tipo;
        PrecioPorNoche = precioPorNoche;
        Disponible = disponible;
    }

    public void Reservar(DateTime fechaEntrada, DateTime fechaSalida)
    {
        if (Disponible)
        {
            Console.WriteLine($"Habitación {Numero} reservada del {fechaEntrada.ToShortDateString()} al {fechaSalida.ToShortDateString()}");
            Disponible = false;
        }
        else
        {
            throw new InvalidOperationException("La habitación no está disponible.");
        }
    }

    public void LiberarHabitacion()
    {
        Disponible = true;
        Console.WriteLine($"Habitación {Numero} está ahora disponible.");
    }
}
