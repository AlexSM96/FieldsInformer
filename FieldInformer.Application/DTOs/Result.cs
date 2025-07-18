namespace FieldInformer.Application.DTOs;

public class Result<T>
{
    public T? Value { get; set; }

    public Exception? Exception { get; set; }
}
