using AutoMapper;
using ePizzaHub.Core.Contracts;
using ePizzaHub.Models.ApiModels.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace ePizzaHub.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await _itemService.GetItemsAsync();

            #region --Common Api Response Format - Problem Statement + Solution --
            //Problem Statement : I want to have a response in below format but don't want write in each and every controller
            //as it will be duplicacy of the code i.e. same is present in Cart controller and need to write in each and every controller
            //to have a response in the same format.
            //Solution : Have a respone from the common location -->  Create a Middleware
            //ApiResponseModel<IEnumerable<GetItemResponse>> responseFormat =
            //    new ApiResponseModel<IEnumerable<GetItemResponse>>(true, items, "Record Fetched");
            //return Ok(responseFormat);
            #endregion


            return Ok(items);
        }
    }
}
