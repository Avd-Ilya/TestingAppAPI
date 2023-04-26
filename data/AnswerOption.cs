
using System.Text.Json.Serialization;

public class AnswerOption {
    public AnswerOption() {
        this.UserAnswers = new HashSet<UserAnswer>();
    }
    public int Id { get; set; }
    public string? Text { get; set; }
    public Boolean? IsCorrect { get; set; }
    public int? Position { get; set; }
    public int? LeftOptionId { get; set; }
    public int? QuestionId { get; set; }
    [JsonIgnore]
    public virtual ICollection<UserAnswer> UserAnswers { get; set; }
}