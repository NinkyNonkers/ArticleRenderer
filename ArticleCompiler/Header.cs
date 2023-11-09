namespace ArticleCompiler;

public class Header : IArticleTextObject
{
    public Header(string content)
    {
        Content = content;
    }

    public string Content { get; }
}