using System.Text.Json.Serialization;

public class Question
{
    public Question()
    {
        // this.Tests = new HashSet<Test>();
        this.AnswerOptions = new HashSet<AnswerOption>();
    }
    public int Id { get; set; }
    public string? Task { get; set; }
    public int? TestId { get; set; }
    public int? QuestionTypeId { get; set; }
    public QuestionType? QuestionType { get; set; }
    public virtual ICollection<AnswerOption> AnswerOptions { get; set; }
    //  [JsonIgnore]
    // public virtual ICollection<Test> Tests { get; set; }
}