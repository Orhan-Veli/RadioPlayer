using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Radio_Player.Business.Abstract;
using Radio_Player.Dal.Data.Concrete;
using Radio_Player.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Radio_Player.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRadioService _radioService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IRedisService _redisService;
        const string redisKey = "model";
        public HomeController
            (
            ILogger<HomeController> logger,
            IRadioService radioService,
            IHttpContextAccessor contextAccessor,
            IRedisService redisService
            ) 
        {
            _logger = logger;
            _radioService = radioService;
            _contextAccessor = contextAccessor;
            _redisService = redisService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var redisResult = await _redisService.GetAll(redisKey);
            var emptyList = new List<RadioListViewModel>();
            var result = await _radioService.GetAll();
            if (result.Data.Count>0)
            {                
                if (redisResult.Data == null)
                {
                     emptyList = result.Data.Adapt<List<RadioListViewModel>>();
                    _logger.LogInformation(result.Data.Count.ToString() + "data have found.");
                    await _redisService.Create(emptyList.Adapt<List<Radio>>());
                }
                else
                {
                    if (redisResult.Success)
                    {
                        foreach (var item in redisResult.Data)
                        {
                            emptyList.Add(item.Adapt<RadioListViewModel>());
                        }
                    }                  
                }
               
            }           
            return View(emptyList);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RadioAddViewModel radioAddViewModel)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Create Model is not Valid");
                return new JsonResult(false);
            }
            var result = radioAddViewModel.Adapt<Radio>();
            await _radioService.Create(result);
            _logger.LogInformation("Radio is created");
           await _redisService.Delete(redisKey);
            return new JsonResult(true);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _logger.LogError("Delete id is not Valid");
                return new JsonResult(false);
            }
            await _radioService.Delete(id);
            _logger.LogInformation("Radio Deleted");
            await _redisService.Delete(redisKey);
            return new JsonResult(true);
        }

        [HttpPut]
        public async Task<IActionResult> Update(RadioUpdateViewModel radioUpdateViewModel)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Update model is not Valid");
                return new JsonResult(false);
            }
            var model = radioUpdateViewModel.Adapt<Radio>();
            await _radioService.Update(model);
            _logger.LogInformation("Update model is not Valid");
            await _redisService.Delete(redisKey);
            return new JsonResult(true);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
