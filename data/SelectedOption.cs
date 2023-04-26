public class SelectedOption {
    public int Id { get; set; }
    public int? Position { get; set; }
    public int? AnswerOtionId { get; set; }
    public AnswerOption? AnswerOtion { get; set; }
    public int? UserAnswerId { get; set; }
}