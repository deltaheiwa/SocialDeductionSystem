namespace Core.Interfaces;

/// <summary>
/// An interface for strongly typed metadata bags.
/// </summary>
/// <remarks>
/// This allows consumers to query for data using a
/// string key, without knowing the concrete implementation.
/// </remarks>
public interface IEffectMetadata
{
    /// <summary>
    /// Tries to get a property from the metadata by name.
    /// </summary>
    /// <typeparam name="T">The expected type of the value.</typeparam>
    /// <param name="key">The string key for the value.</param>
    /// <param name="value">The output value, if found and of the correct type.</param>
    /// <returns>True if the key was found and the type matches, otherwise false.</returns>
    bool TryGetValue<T>(string key, out T? value);
}