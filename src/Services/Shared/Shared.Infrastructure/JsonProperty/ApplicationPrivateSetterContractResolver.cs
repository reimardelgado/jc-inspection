using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Shared.Infrastructure.JsonProperty;

public class ApplicationPrivateSetterContractResolver : DefaultContractResolver
{
    protected override Newtonsoft.Json.Serialization.JsonProperty CreateProperty(MemberInfo member,
        MemberSerialization memberSerialization)
    {
        var jsonProperty = base.CreateProperty(member, memberSerialization);
        if (jsonProperty.Writable)
        {
            return jsonProperty;
        }

        if (member is PropertyInfo propertyInfo)
        {
            jsonProperty.Writable = propertyInfo.GetSetMethod(true) != null;
        }

        return jsonProperty;
    }
}