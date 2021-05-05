using Newtonsoft.Json;
using System.Collections.Generic;

namespace COA.Api.Resources
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public Dictionary<string, List<string>> ErrorList { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
