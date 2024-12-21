namespace ExampleCrud.WebApi.Extensions;

public static class ObjectExtensions
{
    public static bool IsNullOrDefault<T>(this T obj)
    {
        return obj == null || obj.Equals(default(T));
    }

}
