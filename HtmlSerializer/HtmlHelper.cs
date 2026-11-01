using System.Text.Json;

public class HtmlHelper
{
    private readonly static HtmlHelper _instance = new HtmlHelper();
    public static HtmlHelper Instance => _instance;

    public string[] HtmlTags { get; set; }
    public string[] HtmlVoidTags { get; set; }

    private HtmlHelper()
    {
        var tagsJson = File.ReadAllText("Seed/HtmlTags.json");
        HtmlTags = JsonSerializer.Deserialize<string[]>(tagsJson);

        var voidTagsJson = File.ReadAllText("Seed/HtmlVoidTags.json");
        HtmlVoidTags = JsonSerializer.Deserialize<string[]>(voidTagsJson);
    }
}
