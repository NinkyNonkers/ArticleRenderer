using System.Text.RegularExpressions;

namespace ArticleCompiler;

public class Article
{
    public List<Header> Headers { get; }
    public List<Paragraph> Paragraphs { get; }
    public string Title { get; }
    public string Author { get; }
    public List<IArticleTextObject> Texts { get; }
    public string Summary
    {
        get
        {
            string firstPara = Paragraphs[0].Content;
            if (firstPara.Length < DesignConstants.TextPreviewLength)
                return firstPara;
            return firstPara[(DesignConstants.TextPreviewLength - 3)..] + "...";
        }
    }

    public Article(string title, string author, List<IArticleTextObject> textObj)
    {
        Texts = textObj;
        Title = title;
        Author = author;
        Headers = textObj.Where(o => o is Header).Cast<Header>().ToList();
        Paragraphs = textObj.Where(o => o is Paragraph).Cast<Paragraph>().ToList();
    }
    
    //private static Regex TitleRgx = new("=(.*)=");
   // private static Regex HeadingRgx = new("==(.*)==");
   
   private static Regex TitleRgx = new("#(.*)=");
   private static Regex HeadingRgx = new("##(.*)");

   //TODO: add lists
    public static Article Parse(string[] text)
    {
        List<IArticleTextObject> textObjs = new List<IArticleTextObject>();
        string title = "";
        string author = "";
        
        foreach (string line in text)
        {
            Match mtch = HeadingRgx.Match(line);
            
            if (mtch.Success)
            {
                textObjs.Add(new Header(mtch.Groups[1].Value));
                continue;
            }

            mtch = TitleRgx.Match(line);

            if (mtch.Success)
            {
                title = mtch.Groups[1].Value;
                continue;
            }
            
            textObjs.Add(new Paragraph(line));
        }

        return new Article(title, author, textObjs);
    }

    public static Article Parse(string text)
    {
        return Parse(text.Split("\n"));
    }
    
}