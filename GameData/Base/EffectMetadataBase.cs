using Core.Interfaces;

namespace GameData.Base;

public abstract class EffectMetadataBase : IEffectMetadata
{
    private readonly Dictionary<string, object> _properties = new();

    /// <summary>
    /// Helper for derived classes to set a value in the constructor.
    /// </summary>
    protected void SetValue(string key, object value)
    {
        _properties[key] = value;
    }
    
    public bool TryGetValue<T>(string key, out T? value)
    {
        if (_properties.TryGetValue(key, out object? obj) && obj is T typedValue)
        {
            value = typedValue;
            return true;
        }

        value = default;
        return false;
    }
}
