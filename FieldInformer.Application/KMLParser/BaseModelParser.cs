namespace FieldInformer.Application.KMLParser;

public abstract class BaseModelParser<T>(string path) : KMLParser<T>(path) 
    where T : BaseModel, new()
{
    protected override T Parse(Placemark placemark)
    {
        var model = new T();
        var schemaData = placemark.ExtendedData.SchemaData
           .OfType<SchemaData>()
           .FirstOrDefault();

        if (schemaData == null)
        {
            throw new Exception($"{nameof(SchemaData)}");
        }

        var fieldIdData = schemaData.SimpleData.FirstOrDefault(x => x.Name == "fid");
        var sizeData = schemaData.SimpleData.FirstOrDefault(x => x.Name == "size");

        if (fieldIdData == null || sizeData == null)
        {
            throw new KMLParseException($"{nameof(SimpleData)}");
        }

        if (!long.TryParse(fieldIdData.Text, out long id))
        {
            throw new KMLParseException($"{nameof(fieldIdData)}.{nameof(fieldIdData.Text)}");
        }

        if (!double.TryParse(sizeData.Text, out double size))
        {
            throw new KMLParseException($"{nameof(sizeData)}.{nameof(sizeData.Text)}");
        }

        model.Id = id;
        model.Name = placemark.Name;
        model.Size = size;

        return model;
    }
}
