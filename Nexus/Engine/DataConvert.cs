using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.IO;

namespace Nexus.Engine {

	public class DataConvert {

		public static string ToJson<T>(T value) {
			return JsonConvert.SerializeObject(value);
		}

		public static T FromJson<T>(string jsonString) {
			return JsonConvert.DeserializeObject<T>(jsonString);
		}

		public static string ToBson<T>(T value) {
			using(MemoryStream ms = new MemoryStream())
			using(BsonDataWriter datawriter = new BsonDataWriter(ms)) {
				JsonSerializer serializer = new JsonSerializer();
				serializer.Serialize(datawriter, value);
				return Convert.ToBase64String(ms.ToArray());
			}
		}

		public static T FromBson<T>(string base64data) {
			byte[] data = Convert.FromBase64String(base64data);

			using(MemoryStream ms = new MemoryStream(data))
			using(BsonDataReader reader = new BsonDataReader(ms)) {
				JsonSerializer serializer = new JsonSerializer();
				return serializer.Deserialize<T>(reader);
			}
		}
	}
}
