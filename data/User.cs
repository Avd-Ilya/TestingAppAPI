using System.Text.Json.Serialization;

public class User
{
    [JsonIgnore]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Fio { get; set; }
}