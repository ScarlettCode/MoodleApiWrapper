using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;

namespace System.Web
{
    public static class ObjectExtensions
    {
        public static IDictionary<string, string> ToKeyValue(this object metaToken)
        {
            if (metaToken == null)
            {
                return null;
            }

            var token = metaToken as JToken;
            if (token == null)
            {
                var jo = JObject.FromObject(metaToken);
                if(jo != null)
                    return ToKeyValue(jo);
            }

            if (token.HasValues)
            {
                var contentData = new Dictionary<string, string>();
                foreach (var child in token.Children().ToList())
                {
                    var childContent = child.ToKeyValue();
                    if (childContent != null)
                    {
                        contentData = contentData.Concat(childContent)
                                                 .ToDictionary(k => k.Key, v => v.Value);
                    }
                }

                return contentData;
            }

            var jValue = token as JValue;
            if (jValue?.Value == null)
            {
                return null;
            }

            var value = jValue?.Type == JTokenType.Date ?
                            jValue?.ToString("o", CultureInfo.InvariantCulture) :
                            jValue?.ToString(CultureInfo.InvariantCulture);

            return new Dictionary<string, string> { { token.Path, value } };
        }

        public static string ToQueryString(this object obj)
        {
            var keyValueContent = obj.ToKeyValue();
            var formUrlEncodedContent = new FormUrlEncodedContent(keyValueContent);
            var urlEncodedString = formUrlEncodedContent.ReadAsStringAsync().Result;
            return urlEncodedString;
        }
    }
}
