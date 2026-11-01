public static class HtmlElementExtensions
{
    public static HashSet<HtmlElement> FindElements(this HtmlElement element, Selector selector)
    {
        var result = new HashSet<HtmlElement>();
        FindRecursive(element, selector, result);
        return result;
    }

    private static void FindRecursive(HtmlElement element, Selector selector, HashSet<HtmlElement> result)
    {
        var matches = element.Descendants()
                             .Skip(1)
                             .Where(e => selector.IsMatch(e));

        if (selector.Child == null)
        {
            foreach (var match in matches)
                result.Add(match);
            return;
        }

        foreach (var match in matches)
            FindRecursive(match, selector.Child, result);
    }
}
