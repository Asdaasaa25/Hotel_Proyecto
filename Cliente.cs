public class Cliente : Persona
{
    public string Email { get; set; }
    public string Telefono { get; set; }

    public Cliente(int id, string nombre, string apellido, string email, string telefono)
        : base(id, nombre, apellido)
    {
        Email = email;
        Telefono = telefono;
    }

    public override void MostrarInformacion()
    {
        Console.WriteLine($"Cliente: {Nombre} {Apellido}, Email: {Email}, Teléfono: {Telefono}");
    }
}
