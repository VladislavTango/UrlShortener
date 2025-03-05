using Microsoft.AspNetCore.Mvc;
using URLShortener.Application.Url.Requests;
using MediatR;

namespace URLShortener.Controllers
{
    public class HomeController : Controller
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUrl(AddUrlRequest request)
        {
            var responce = await Mediator.Send(request);

            return View("AddUrl", responce);
        }
        [HttpGet]
        public async Task<IActionResult> GetUrls(GetUrlsRequest request)
        {
            var responce = await Mediator.Send(request);
            return View(responce);
        }
    }
}
