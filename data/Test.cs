public class Test
{
    public Test()
    {
        this.Questions = new HashSet<Question>();
    }
    public int Id { get; set; }
    public string Topic { get; set; } = null!;
    public int ChapterId { get; set; }  
    public Chapter? Chapter { get; set; }  
    public virtual ICollection<Question> Questions { get; set; }
}