namespace fatmeh_lab2_backend.Presentation.Models;

public class Book
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public int AuthorId { get; set; }
    public string ISBN { get; set; }
    public int PublishedYear { get; set; }
    public DateTime ReleaseDate { get; set; }
    public Author Author { get; set; }
}