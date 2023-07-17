using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FileExceptions
{
    public class ScheduleModal
    {
        [JsonPropertyName("appPath")]
        public string AppPath { get; set; }
        [JsonPropertyName("scheduleTimes")]
        public List<string> ScheduleTimes { get; set; }
        [JsonPropertyName("arguments")]
        public string Arguments { get; set; }
    }
}
