using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galac.Adm.IntegracionMS.Venta {
    public class CustomDecimalJsonConverter : JsonConverter {
        public override bool CanConvert(Type objectType) {
            return (objectType == typeof(decimal));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {

            writer.WriteValue(Convert.ToDecimal(value).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));

        }
    }

}
