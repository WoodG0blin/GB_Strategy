using System;
using System.Reflection;
using UnityEngine;

public static class AssetInjector 
{
    public static T Inject<T>(this AssetContext context, T target)
    {
        var targetType = target.GetType();

        while (targetType != null)
        {
            var allFields = targetType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            for (int i = 0; i < allFields.Length; i++)
            {
                var field = allFields[i];
                var injectAssetAttribute = field.GetCustomAttribute<InjectAssetAttribute>();
                if (injectAssetAttribute != null)
                {
                    var objectToInject = context.GetObjectOfType(field.FieldType, injectAssetAttribute.AssetName);
                    field.SetValue(target, objectToInject);
                }
            }

            var allProperties = targetType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            for (int i = 0; i < allProperties.Length; i++)
            {
                var field = allProperties[i];
                var injectAssetAttribute = field.GetCustomAttribute<InjectAssetAttribute>();
                if (injectAssetAttribute != null)
                {
                    var objectToInject = context.GetObjectOfType(field.PropertyType, injectAssetAttribute.AssetName);
                    field.SetValue(target, objectToInject);
                }
            }

            targetType = targetType.BaseType;
        }
        return target;
    }
}
