using static VIPVIlla_Utility.SD;

namespace VIP_Villa.Models
{
    public class ApiRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }

    }
}
