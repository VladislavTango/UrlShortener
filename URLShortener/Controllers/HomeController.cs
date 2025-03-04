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
        public IActionResult AddUrl(AddUrlRequest request)
        {
            var responce = Mediator.Send(request);
            return View(responce);
        }
        [HttpGet]
        public IActionResult GetUrls(GetUrlsRequest request)
        {
            var responce = Mediator.Send(request);
            return View(responce);
        }
        [HttpPut]
        public IActionResult UpdateUrl(UpdateUrlRequest request)
        {
            var responce = Mediator.Send(request);
            return View(responce);
        }
        [HttpDelete]
        public IActionResult UpdateUrl(DeleteRequest request)
        {
            var responce = Mediator.Send(request);
            return View(responce);
        }
        public async Task<IActionResult> Redirect(RedirectRequest request)
        {
            var responce = await Mediator.Send(request);
            return Redirect(responce);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
