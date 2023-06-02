using TestingAppApi.Users;

public class ActivityTrackedTestResponse {

    // public Guid? UserId { get; set; }
    public User? User { get; set; }
    // public DateTime? Date { get; set; }
    // public double? Result { get; set; }
    // public int? TestId { get; set; }
    // public int? TrackedTestId { get; set; }
    public PassedTest? PassedTest { get; set; }   
}