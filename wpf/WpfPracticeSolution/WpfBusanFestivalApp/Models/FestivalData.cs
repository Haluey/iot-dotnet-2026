using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace WpfBusanFestivalApp.Models
{
    public class FestivalData
    {
        [JsonProperty("item")]
        public ObservableCollection<FestivalItem> Items { get; set; }
    }
}
