namespace BGJ20212.Game.ApprenticeGC.Dialogue.EditorPart.Generated
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class CutScene
    {
        [JsonProperty("inkVersion")]
        public long InkVersion { get; set; }

        [JsonProperty("root")]
        public List<CutSceneRoot> Root { get; set; }

        [JsonProperty("listDefs")]
        public ListDefs ListDefs { get; set; }
    }

    public partial class ListDefs
    {
    }

    public partial class PurpleRoot
    {
        [JsonProperty("#f")]
        public long F { get; set; }

        [JsonProperty("#n")]
        public string N { get; set; }
    }

    public partial class FluffyRoot
    {
        [JsonProperty("conversation")]
        public List<ConversationElement> Conversation { get; set; }

        [JsonProperty("#f")]
        public long F { get; set; }
    }

    public partial class ConversationClass
    {
        [JsonProperty("#", NullValueHandling = NullValueHandling.Ignore)]
        public string Empty { get; set; }

        [JsonProperty("#f", NullValueHandling = NullValueHandling.Ignore)]
        public long? F { get; set; }
    }

    public partial struct TentacledRoot
    {
        public PurpleRoot PurpleRoot;
        public string String;

        public static implicit operator TentacledRoot(PurpleRoot PurpleRoot) => new TentacledRoot { PurpleRoot = PurpleRoot };
        public static implicit operator TentacledRoot(string String) => new TentacledRoot { String = String };
    }

    public partial struct StickyRoot
    {
        public List<TentacledRoot> AnythingArray;
        public string String;

        public static implicit operator StickyRoot(List<TentacledRoot> AnythingArray) => new StickyRoot { AnythingArray = AnythingArray };
        public static implicit operator StickyRoot(string String) => new StickyRoot { String = String };
        public bool IsNull => AnythingArray == null && String == null;
    }

    public partial struct ConversationElement
    {
        public ConversationClass ConversationClass;
        public string String;

        public static implicit operator ConversationElement(ConversationClass ConversationClass) => new ConversationElement { ConversationClass = ConversationClass };
        public static implicit operator ConversationElement(string String) => new ConversationElement { String = String };
    }

    public partial struct CutSceneRoot
    {
        public List<StickyRoot> AnythingArray;
        public FluffyRoot FluffyRoot;
        public string String;

        public static implicit operator CutSceneRoot(List<StickyRoot> AnythingArray) => new CutSceneRoot { AnythingArray = AnythingArray };
        public static implicit operator CutSceneRoot(FluffyRoot FluffyRoot) => new CutSceneRoot { FluffyRoot = FluffyRoot };
        public static implicit operator CutSceneRoot(string String) => new CutSceneRoot { String = String };
    }

    public partial class CutScene
    {
        public static CutScene FromJson(string json) => JsonConvert.DeserializeObject<CutScene>(json, BGJ20212.Game.ApprenticeGC.Dialogue.EditorPart.Generated.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this CutScene self) => JsonConvert.SerializeObject(self, BGJ20212.Game.ApprenticeGC.Dialogue.EditorPart.Generated.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                CutSceneRootConverter.Singleton,
                StickyRootConverter.Singleton,
                TentacledRootConverter.Singleton,
                ConversationElementConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class CutSceneRootConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(CutSceneRoot) || t == typeof(CutSceneRoot?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new CutSceneRoot { String = stringValue };
                case JsonToken.StartObject:
                    var objectValue = serializer.Deserialize<FluffyRoot>(reader);
                    return new CutSceneRoot { FluffyRoot = objectValue };
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<List<StickyRoot>>(reader);
                    return new CutSceneRoot { AnythingArray = arrayValue };
            }
            throw new Exception("Cannot unmarshal type CutSceneRoot");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (CutSceneRoot)untypedValue;
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            if (value.AnythingArray != null)
            {
                serializer.Serialize(writer, value.AnythingArray);
                return;
            }
            if (value.FluffyRoot != null)
            {
                serializer.Serialize(writer, value.FluffyRoot);
                return;
            }
            throw new Exception("Cannot marshal type CutSceneRoot");
        }

        public static readonly CutSceneRootConverter Singleton = new CutSceneRootConverter();
    }

    internal class StickyRootConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(StickyRoot) || t == typeof(StickyRoot?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Null:
                    return new StickyRoot { };
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new StickyRoot { String = stringValue };
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<List<TentacledRoot>>(reader);
                    return new StickyRoot { AnythingArray = arrayValue };
            }
            throw new Exception("Cannot unmarshal type StickyRoot");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (StickyRoot)untypedValue;
            if (value.IsNull)
            {
                serializer.Serialize(writer, null);
                return;
            }
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            if (value.AnythingArray != null)
            {
                serializer.Serialize(writer, value.AnythingArray);
                return;
            }
            throw new Exception("Cannot marshal type StickyRoot");
        }

        public static readonly StickyRootConverter Singleton = new StickyRootConverter();
    }

    internal class TentacledRootConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TentacledRoot) || t == typeof(TentacledRoot?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new TentacledRoot { String = stringValue };
                case JsonToken.StartObject:
                    var objectValue = serializer.Deserialize<PurpleRoot>(reader);
                    return new TentacledRoot { PurpleRoot = objectValue };
            }
            throw new Exception("Cannot unmarshal type TentacledRoot");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (TentacledRoot)untypedValue;
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            if (value.PurpleRoot != null)
            {
                serializer.Serialize(writer, value.PurpleRoot);
                return;
            }
            throw new Exception("Cannot marshal type TentacledRoot");
        }

        public static readonly TentacledRootConverter Singleton = new TentacledRootConverter();
    }

    internal class ConversationElementConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ConversationElement) || t == typeof(ConversationElement?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new ConversationElement { String = stringValue };
                case JsonToken.StartObject:
                    var objectValue = serializer.Deserialize<ConversationClass>(reader);
                    return new ConversationElement { ConversationClass = objectValue };
            }
            throw new Exception("Cannot unmarshal type ConversationElement");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (ConversationElement)untypedValue;
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            if (value.ConversationClass != null)
            {
                serializer.Serialize(writer, value.ConversationClass);
                return;
            }
            throw new Exception("Cannot marshal type ConversationElement");
        }

        public static readonly ConversationElementConverter Singleton = new ConversationElementConverter();
    }
}
