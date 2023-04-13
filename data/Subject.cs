using System.Text.Json.Serialization;

public class Subject
{

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int SchoolClassId { get; set; }
    public SchoolClass? SchoolClass { get; set; }
}