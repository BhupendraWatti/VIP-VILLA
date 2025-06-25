using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Villa_Services.Models;
using VIP_Villa.Models.Dto;
using VIP_Villa.Models.VM;
using VIP_Villa.Services.IServices;

namespace VIP_Villa.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberService _villaNum;
        IVillaService _villaService;
        private readonly IMapper _mapper;
        public VillaNumberController(IVillaNumberService villa, IMapper mapper, IVillaService villaService)
        {
            _villaNum = villa;
            _mapper = mapper;
            _villaService = villaService;
        }

        public async Task<IActionResult> IndexVillaNumber()
        {
            List<VillaNumberDto> list = new();
            var response = await _villaNum.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaNumberDto>>(Convert.ToString(response.Result));
            }
            return View(list);
           
        }
        public async Task<IActionResult> CreateVillaNumber()
        {
            VillaNumberCreateVM VillaNVM = new();
            var response = await _villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                VillaNVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>
                    (Convert.ToString(response.Result)).Select(i =>
                    new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                
            }
            return View(VillaNVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateVM T)
        {
            if (ModelState.IsValid)
            {
                
                var response = await _villaNum.CreateAsync<APIResponse>(T.VM);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "You new Villa is created";
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
                else
                {
                    if(response.ErrorMessage.Count> 0)
                    {
                        ModelState.AddModelError("ErrorMessage", response.ErrorMessage.FirstOrDefault());
                    }
                }
            }
            var res = await _villaService.GetAllAsync<APIResponse>();
            if (res != null && res.IsSuccess)
            {
                T.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>
                    (Convert.ToString(res.Result)).Select(i =>
                    new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }
            TempData["error"] = "Error encounted.";
            return View(T);
        }

        public async Task<IActionResult> UpdateVillaNumber(int villaNo)
        {
            VillaNumberUpdateVM updateVM = new VillaNumberUpdateVM();
            var response = await _villaNum.GetAsync<APIResponse>(villaNo);
            if (response != null && response.IsSuccess && response.Result != null)
            {
                VillaNumberDto model = JsonConvert.DeserializeObject<VillaNumberDto>(Convert.ToString(response.Result));
                if (model == null)
                    return NotFound(); // Prevent null passed to the view

                updateVM.VM =  _mapper.Map<VillaNumberUpdate>(model);
            }
            response = await _villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                updateVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>
                    (Convert.ToString(response.Result)).Select(i =>
                    new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            return View(updateVM);
            }
            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdateVM T)
        {
            if (ModelState.IsValid)
            {

                var response = await _villaNum.UpdateAsync<APIResponse>(T.VM);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "You Villa is Updated.";
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
                else
                {
                    if (response != null && response.ErrorMessage != null && response.ErrorMessage.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessage", response.ErrorMessage.FirstOrDefault());
                    }
                }
            }
            var res = await _villaService.GetAllAsync<APIResponse>();
            if (res != null && res.IsSuccess)
            {
                T.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>
                    (Convert.ToString(res.Result)).Select(i =>
                    new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }
            TempData["error"] = "Error encounted.";
            return View(T);


        }
        public async Task<IActionResult> DeleteVillaNumber(int villaNo)
        {
            VillaNumberDeleteVM updateVM = new VillaNumberDeleteVM();
            var response = await _villaNum.GetAsync<APIResponse>(villaNo);
            if (response != null && response.IsSuccess && response.Result != null)
            {
                VillaNumberDto model = JsonConvert.DeserializeObject<VillaNumberDto>(Convert.ToString(response.Result));
                if (model == null)
                    return NotFound(); // Prevent null passed to the view

                updateVM.VM =  model;
            }
            response = await _villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                updateVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>
                    (Convert.ToString(response.Result)).Select(i =>
                    new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            return View(updateVM);
            }
            return NotFound();


        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVillaNumber(VillaNumberDeleteVM T)
        {

            var response = await _villaNum.DeleteAsync<APIResponse>(T.VM.VillaNo);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "You new Villa is Deleted.";
                return RedirectToAction(nameof(IndexVillaNumber));
            }
            TempData["error"] = "Error encounted.";
            return View(T);


        }
    }
}
