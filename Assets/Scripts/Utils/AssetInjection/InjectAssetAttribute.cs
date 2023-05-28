using System;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class InjectAssetAttribute : Attribute
{
    public readonly string AssetName;
    public InjectAssetAttribute(string assetName = null) => AssetName = assetName;
}
