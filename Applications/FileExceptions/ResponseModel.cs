using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FileExceptions
{
    public class ResponseModel
    {
        [JsonPropertyName("schedules")]
        public List<ScheduleModal> Schedules { get; set; } = new List<ScheduleModal>();
    }
}
