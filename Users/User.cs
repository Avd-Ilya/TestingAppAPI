using System.Text.Json.Serialization;

namespace TestingAppApi.Users;

public class User
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Fio { get; set; }
}