public class PassedTest {
    public PassedTest() {
        this.UserAnswers = new HashSet<UserAnswer>();
    }
    public int Id { get; set; }
    public DateTime? Date { get; set; }
    public double? Result { get; set; }
    public int? TestId { get; set; }
    public Test? Test { get; set; }   
    public Guid? UserId { get; set; }
    public int? TrackedTestId { get; set; }   
    public virtual ICollection<UserAnswer> UserAnswers { get; set; }
}