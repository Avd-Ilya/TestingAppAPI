public class UserAnswerRequest {
    public UserAnswerRequest() {
        this.SelectedOptionRequests = new HashSet<SelectedOptionRequest>();
    }
    public int? QuestionId { get; set; }
    public virtual ICollection<SelectedOptionRequest> SelectedOptionRequests { get; set; }
}