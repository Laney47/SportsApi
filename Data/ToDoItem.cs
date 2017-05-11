using Newtonsoft.Json;
namespace SportsApi.Data
{
    public class ToDoItem
    {
        [JsonProperty(PropertyName = "id")]
        public string ID { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
       
        
    }
}
