using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RawRabbit.Serialization;

namespace Venture.Common.Extensions
{
    internal sealed class CustomJsonSerializer : JsonMessageSerializer
    {
        public CustomJsonSerializer() : base(new JsonSerializer
        {
            TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
            Formatting = Formatting.None,
            CheckAdditionalContent = true,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            ObjectCreationHandling = ObjectCreationHandling.Auto,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.Auto,
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            NullValueHandling = NullValueHandling.Ignore
        })
        {}
    }
}
