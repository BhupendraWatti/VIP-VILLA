using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VIP_Villa.Models;
using VIP_Villa.Models.Dto;
using VIP_Villa.Services.IServices;

namespace VIP_Villa.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService _villa;
        private readonly IMapper _mapper;
        public VillaController(IVillaService villa, IMapper mapper)
        {
            _villa = villa;
            _mapper = mapper;
        }
        public async Task<IActionResult> IndexVilla()
        {
            List<VillaDto> list = new();
            var response = await _villa.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess) { 
                list = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> CreateVilla()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla(VillaCreateDto T)
        {
            if (ModelState.IsValid)
            {
                var response = await _villa.CreateAsync<APIResponse>(T);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            return View(T);
            
        }

        public async Task<IActionResult> UpdateVilla(int id)
        {
            var response = await _villa.GetAsync<APIResponse>(id);
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
        public async Task<IActionResult> UpdateVilla(VillaUpdateDto T)
        {
            if (ModelState.IsValid)
            {
                var response = await _villa.UpdateAsync<APIResponse>(T);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
                    return View(T);

            
        }
        public async Task<IActionResult> DeleteVilla(int id)
        {
            var response = await _villa.GetAsync<APIResponse>(id);
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
        public async Task<IActionResult> DeleteVilla(VillaDto T)
        {
       
                var response = await _villa.DeleteAsync<APIResponse>(T.Id);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVilla));
                }
                     
        return View(T);


        }
    }
}
