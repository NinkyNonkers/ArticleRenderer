namespace ArticleCompiler;

public class Paragraph : IArticleTextObject
{
    public Paragraph(string content)
    {
        Content = content;
    }

    public string Content { get; }
}