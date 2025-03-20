namespace MinApiLookup.Attributes;
[AttributeUsage(AttributeTargets.Property)]
public class DbColumnAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}

[AttributeUsage(AttributeTargets.Property)]
public class QueryParamAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}
