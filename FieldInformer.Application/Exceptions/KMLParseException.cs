namespace FieldInformer.Application.Exceptions;

public class KMLParseException(string property) : Exception($"{property} not parsed");

