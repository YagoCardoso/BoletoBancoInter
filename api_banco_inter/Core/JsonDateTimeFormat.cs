using Newtonsoft.Json.Converters;

namespace BancoInter.Core
{
    internal class JsonDateTimeFormat : IsoDateTimeConverter
    {
        public JsonDateTimeFormat()
        {
            base.DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
