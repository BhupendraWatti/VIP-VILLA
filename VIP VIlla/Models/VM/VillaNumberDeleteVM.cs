using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Villa_Services.Models;
using VIP_Villa.Models.Dto;

namespace VIP_Villa.Models.VM
{
    public class VillaNumberDeleteVM
    {
        public VillaNumberDeleteVM()
        {
            VM = new VillaNumberDto(); 
        }
        public VillaNumberDto VM { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>  VillaList { get; set; }
    }
}
