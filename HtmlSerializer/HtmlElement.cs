public class HtmlElement
{
    public string Id { get; set; }
    public string Name { get; set; }
    public Dictionary<string, string> Attributes { get; set; } = new();
    public List<string> Classes { get; set; } = new();
    public string InnerHtml { get; set; } = "";

    public HtmlElement Parent { get; set; }
    public List<HtmlElement> Children { get; set; } = new();

    public IEnumerable<HtmlElement> Descendants()
    {
        var queue = new Queue<HtmlElement>();
        queue.Enqueue(this);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            yield return current;

            foreach (var child in current.Children)
                queue.Enqueue(child);
        }
    }

    public IEnumerable<HtmlElement> Ancestors()
    {
        var current = this.Parent;

        while (current != null)
        {
            yield return current;
            current = current.Parent;
        }
    }
}
