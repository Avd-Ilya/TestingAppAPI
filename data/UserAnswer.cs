public class UserAnswer {
    public UserAnswer() {
        this.SelectedOptions = new HashSet<SelectedOption>();
    }
    public int Id { get; set; }
    public int? PassedTestId { get; set; }
    public int? QuestionId { get; set; }
    public Question? Question { get; set; }
    public virtual ICollection<SelectedOption> SelectedOptions { get; set; }
}