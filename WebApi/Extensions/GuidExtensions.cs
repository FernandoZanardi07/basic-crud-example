namespace ExampleCrud.WebApi.Extensions;

public static class GuidExtensions
{
    public static bool IsNullOrDefault(this Guid? guid)
    {
        return guid == null || !guid.HasValue || guid.Value == Guid.Empty || guid.Value == default;
    }

    public static Guid GetValueOrDefault(this Guid? guid)
    {
        return guid ?? Guid.Empty;
    }
}
