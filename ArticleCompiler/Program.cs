// See https://aka.ms/new-console-template for more information

using ArticleCompiler;
using NinkyNonk.Shared.Environment;

Project.LoggingProxy.LogProgramInfo();

Project.LoggingProxy.LogInfo("Parsing articles...");
Dictionary<Article, string?> articles = new Dictionary<Article, string?>();

foreach (string file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory).Where(f => f.Contains(".md")))
{
    string[] fContent = File.ReadAllLines(file);
    if (fContent.Length < 1)
        continue;
    articles.Add(Article.Parse(fContent), null);
}

string headerTemplate = File.ReadAllText(Project.LoggingProxy.AskInput("Enter header template file: "));

Project.LoggingProxy.LogInfo("Rendering articles...");
List<Article> tempArticles = articles.Keys.ToList();

foreach (Article arti in tempArticles)
    articles[arti] = arti.ToHtml(headerTemplate);

Project.LoggingProxy.LogInfo("Rendering summary page...");
string summaryPage = HtmlHelper.GenerateSummaryPage(tempArticles, headerTemplate);

Project.LoggingProxy.LogInfo("Writing rendered html...");
File.WriteAllText("index.html", summaryPage);

foreach (Article article in articles.Keys) 
    File.WriteAllText(article.Title.Replace(" ", ""), articles[article]);

Project.LoggingProxy.LogSuccess("Done!");