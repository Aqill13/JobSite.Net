using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JobSite.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(Contact data)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", data);
            }

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = Request.Headers["User-Agent"].ToString();
            Console.WriteLine(userAgent);

            var message = await _contactService.SReadInIPAddress(x => x.IpAddress ==  ipAddress && x.UserAgent == userAgent);
            if (message != null)
            {
                var lastMessageDate = DateTime.Now - message.Date;
                if (lastMessageDate.TotalMinutes < 5)
                {
                    TempData["errMess"] = "Mesajınız yeni göndərilib. 5 dəqiqə sonra yenidən cəhd edin.";
                    return RedirectToAction(nameof(Index));
                }
            }
            var newMessage = new Contact
            {
                Name = data.Name,
                Email = data.Email,
                Subject = data.Subject,
                Message = data.Message,
                IpAddress = ipAddress,
                UserAgent = userAgent,
            };
            await _contactService.SCreateAsync(newMessage);
            TempData["successMess"] = "Mesajınız qeydə alındı.";
            return RedirectToAction(nameof(Index));
        }
    }
}
