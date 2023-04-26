public class Chapter
{
    public int Id { get; set; }
    public string Name { get; set; }  = null!;
    public int SubjectId { get; set; } 
    public Subject? Subject { get; set; }
}