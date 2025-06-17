using Villa_Services.Models;

namespace VIP_Villa.Models.Dto
{
    public class VillaNumberDto
    {
        public int VillaNo { get; set; }
        public string SpecialDetails { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public Villa Villa{ get; set; }
    }
}
