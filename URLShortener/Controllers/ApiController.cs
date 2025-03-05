using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using URLShortener.Application.Url.Requests;
using URLShortener.Domain.Interfaces;
using URLShortener.Domain.Models;

namespace URLShortener.Controllers
{
    [ApiController]
    [Route("/")] 
    public class ApiController : ControllerBase
    {
        private readonly IUrlRepository _urlRepository;

        public ApiController(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }
        /// <summary>
        /// браузер со временем из-за статической маршрутизации начнет игнорить контроллер
        /// и сразу зайдёт в нужный url, если хочется что-бы цифры расли смените браузер
        /// из-за этого я принудительно меняю в urlupdate сокращенный путь
        /// </summary>

        [HttpGet("{shortUrl}")]
        public async Task<IActionResult> Redirect(string shortUrl)
        {
            var URL = await _urlRepository.GetByShortUrl(shortUrl);
            if (URL == null)
                throw new Exception("not found");

            URL.RedirectCounter += 1;
            await _urlRepository.UpdateUrl(URL);

            return RedirectPermanent(URL.LongUrl);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUrl([FromBody] DeleteRequest request)
        {
            if (_urlRepository.GetByShortUrl(request.shorUrl) == null)
                return NotFound("Нет такой записи"); 

            var deletedUrl = await _urlRepository.DeleteUrl(request.shorUrl);
            return Ok(deletedUrl);
        }


        [HttpPut("update")]
        public async Task<IActionResult> UpdateUrl(UpdateUrlRequest request)
        {
            string pattern = @"^(https?:\/\/)?([а-я0-9_-]{1,32}|[a-z0-9_-]{1,32})\.([а-я0-9_-]{1,8}|[a-z0-9_-]\S{1,8})$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            if (!regex.IsMatch(request.LongUrl))
                throw new Exception("Это не ссылка");
            var old = await _urlRepository.GetByShortUrl(request.ShortUrl);
            if (old == null)
                throw new Exception("not found");
            UrlEntity entity = new()
            {
                Id = old.Id,
                LongUrl = request.LongUrl,
                ShortUrl = Guid.NewGuid().ToString().Substring(0, 6),
                CreateDate = old.CreateDate,
                RedirectCounter = 0,
            };

            await _urlRepository.UpdateUrl(entity);
            return Ok();
        }
    }
}
