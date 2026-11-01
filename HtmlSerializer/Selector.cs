using System.Text.RegularExpressions;

public class Selector
{
    public string TagName { get; set; }
    public string Id { get; set; }
    public List<string> Classes { get; set; } = new();

    public Selector Parent { get; set; }
    public Selector Child { get; set; }

    public static Selector FromQueryString(string query)
    {
        var levels = query.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        Selector root = null;
        Selector current = null;

        foreach (var level in levels)
        {
            var selector = new Selector();

            var parts = Regex.Split(level, @"(?=[#.])");

            foreach (var part in parts)
            {
                if (string.IsNullOrEmpty(part))
                    continue;

                if (part.StartsWith("#"))
                    selector.Id = part.Substring(1);
                else if (part.StartsWith("."))
                    selector.Classes.Add(part.Substring(1));
                else if (HtmlHelper.Instance.HtmlTags.Contains(part.ToLower()))
                    selector.TagName = part.ToLower();
            }

            if (root == null)
            {
                root = selector;
            }
            else
            {
                current.Child = selector;
                selector.Parent = current;
            }

            current = selector;
        }

        return root;
    }

    public bool IsMatch(HtmlElement element)
    {
        if (TagName != null && element.Name != TagName)
            return false;

        if (Id != null && element.Id != Id)
            return false;

        return Classes.All(c => element.Classes.Contains(c));
    }
}
