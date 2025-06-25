using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Villa_Services.Models;
using VIP_Villa.Models.Dto;

namespace VIP_Villa.Models.VM
{
    public class VillaNumberCreateVM
    {
        public VillaNumberCreateVM()
        {
            VM = new VillaNumberCreate(); 
        }
        public VillaNumberCreate VM { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>  VillaList { get; set; }
    }
}
