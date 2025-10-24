namespace CarerConsole.Data;

public class User
{
    [Key]
    public int Id { get; set; }

    [Column("corporation_id")]
    public int CorporationId { get; set; }

    public string Name { get; set; } = null!;

    public int Age { get; set; }

    public void Deconstruct(out int id, out int corporationId, out string name, out int age)
    {
        id = Id;
        corporationId = CorporationId;
        name = Name;
        age = Age;
    }
}
