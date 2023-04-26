public class PassedTestRequset {

    public PassedTestRequset() {
        this.UserAnswerRequests = new HashSet<UserAnswerRequest>();
    }
    public DateTime? Date { get; set; }
    public double? Result { get; set; }
    public int? TestId { get; set; }   
    public ICollection<UserAnswerRequest> UserAnswerRequests { get; set; }
}