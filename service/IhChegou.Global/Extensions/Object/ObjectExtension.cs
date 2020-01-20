namespace IhChegou.Global.Extensions.Object
{
    public static class ObjectExtension
    {
        public static string ToJson(this object value)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(value, Newtonsoft.Json.Formatting.Indented);
        }
    }
}