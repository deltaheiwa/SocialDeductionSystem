namespace SocialDeductionSystem.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class RoleAdapterAttribute : Attribute
{
    public string TargetVariationId { get; }
    public string BaseRoleId { get; }

    /// <summary>
    /// Marks this class as an adapter for a specific base role within a specific SDG variation.
    /// </summary>
    /// <param name="targetVariationId">The unique ID of the SDG variation this adapter applies to.</param>
    /// <param name="baseRoleId">The unique ID of the base role this adapter modifies.</param>
    public RoleAdapterAttribute(string targetVariationId, string baseRoleId)
    {
        if (string.IsNullOrWhiteSpace(targetVariationId))
            throw new ArgumentException("TargetVariationId cannot be null or whitespace.", nameof(targetVariationId));
        if (string.IsNullOrWhiteSpace(baseRoleId))
            throw new ArgumentException("BaseRoleId cannot be null or whitespace.", nameof(baseRoleId));

        TargetVariationId = targetVariationId;
        BaseRoleId = baseRoleId;
    }
}