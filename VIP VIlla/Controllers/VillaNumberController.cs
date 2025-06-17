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
        private readonly IMapper _mapper;
        public VillaNumberController(IVillaNumberService villa, IMapper mapper)
        {
            _villaNum = villa;
            _mapper = mapper;
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
            var response = await _villaNum.GetAllAsync<APIResponse>();
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
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
            }
            return View(T);

        }

        public async Task<IActionResult> UpdateVillaNumber(int id)
        {
            var response = await _villaNum.GetAsync<APIResponse>(id);
            if (response != null && response.IsSuccess && response.Result != null)
            {
                VillaDto model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.Result));
                if (model == null)
                    return NotFound(); // Prevent null passed to the view

                return View(_mapper.Map<VillaUpdateDto>(model));
            }

            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdate T)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaNum.UpdateAsync<APIResponse>(T);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
            }
            return View(T);


        }
        public async Task<IActionResult> DeleteVillaNumber(int id)
        {
            var response = await _villaNum.GetAsync<APIResponse>(id);
            if (response != null && response.IsSuccess && response.Result != null)
            {
                VillaDto model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.Result));
                if (model == null)
                    return NotFound(); // Prevent null passed to the view

                return View(model);
            }

            return NotFound();

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVillaNumber(VillaNumberDto T)
        {

            var response = await _villaNum.DeleteAsync<APIResponse>(T.VillaNo);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexVillaNumber));
            }

            return View(T);


        }
    }
}
