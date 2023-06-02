using TestingAppApi.Users;

public class TrackedTest {

    public int Id { get; set; }
    public string? Key { get; set; }
    public DateTime? DateCreation { get; set; }
    public string? Description { get; set; }
    public int? TestId { get; set; }
    public Test? Test { get; set; }
    public Guid? UserId { get; set; }
    public User? User { get; set; }
}