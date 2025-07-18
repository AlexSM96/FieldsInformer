namespace FieldInformer.Application.KMLParser;

public abstract class KMLParser<T>
{
    private readonly KmlFile _kmlFile;

    public KMLParser(string path)
    {
        _kmlFile = ReadFile(path);
        ParsedItems = GetParsedItems();
    }

    public IEnumerable<T> ParsedItems { get; }

    protected abstract T Parse(Placemark placemark);

    private KmlFile ReadFile(string path)
    {
        using var streamReader = File.OpenText(path);
        return KmlFile.Load(streamReader);
    }

    private IEnumerable<T> GetParsedItems()
    {
        foreach (Placemark placemark in _kmlFile.Root.Flatten().OfType<Placemark>().ToList())
        {
            yield return Parse(placemark);
        }
    }
}
