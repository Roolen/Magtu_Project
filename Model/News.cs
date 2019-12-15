using System.Text;

namespace Model
{
    public class News
    {
        public string Title { get; }
        public string Author { get; }
        public string PublishDate { get; }
        public StringBuilder Content { get; }

        public News(string title, string author, string publishDate, StringBuilder content)
        {
            Title = title;
            Author = author;
            PublishDate = publishDate;
            Content = content;
        }
    }
}
