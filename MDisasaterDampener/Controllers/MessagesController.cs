using MDisasaterDampener.Models;
using Microsoft.AspNetCore.Mvc;

namespace MDisasaterDampener.Controllers
{
    public class MessagesController : Controller
    {
        public IActionResult ViewMessages()
        {
            var messages = new MessagesViewModel
            {
               // Messages=messagesService.ReadMessages()
            };
            return View(messages);
        }
    }
}
