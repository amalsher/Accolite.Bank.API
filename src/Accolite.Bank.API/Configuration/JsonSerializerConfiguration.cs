using System.Text.Json.Serialization;
using System.Text.Json;

namespace Accolite.Bank.API.Configuration;

public static class JsonSerializerConfiguration
{
    public static void ConfigureJsonSerializerOptions(this JsonSerializerOptions options)
    {
        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.IncludeFields = false;
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.WriteIndented = false;
        options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    }
}
