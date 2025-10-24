namespace CarerConsole.Data;

public class Corporation
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public void Deconstruct(out int id, out string name)
    {
        id = Id;
        name = Name;
    }
}
