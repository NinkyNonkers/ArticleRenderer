namespace ArticleCompiler;

public static class HtmlHelper
{
    public static string ToHtml(this Article article, string headerTemplate)
    {
        string header = headerTemplate.Replace("<title>", $"<title>{article.Title}"); //TODO: add author for later
        string body = $"\n<body>\n<h1>{article.Title}</h1>";
        
        foreach (IArticleTextObject obj in article.Texts)
        {
            switch (obj)
            {
                case Header h:
                    body += $"<h2>{h.Content}</h2>";
                    break;
                case Paragraph p:
                    body += $"<p>{p.Content}</p>";
                    break;
            }
        }
        
        body += "\n</body>";
        return "<DOCTYPE html>\n<html>" + header + body + "</html>";
    }

    public static string GenerateSummaryPage(List<Article> articles, string headerTemplate)
    {
        string header = headerTemplate.Replace("<title>", $"<title>the daily nonk</title>");
        string body = $"\n<body>\n<h1>";
        
        foreach (Article article in articles)
        {
            body += $"\n<a href={article.Title}.html><div>";
            body += $"\n<h3>{article.Title}</h3>";
            body += $"\n<p>{article.Summary}</p>";
            body += "\n</div> </a>";
        }
        
        body += "\n</body>";
        return "<DOCTYPE html>\n<html>" + header + body + "</html>";
    }
}