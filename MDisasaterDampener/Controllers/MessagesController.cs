using MDisasaterDampener.Models;
using Microsoft.AspNetCore.Mvc;

namespace MDisasaterDampener.Controllers
{
    public class MessagesController : Controller
    {
        public IActionResult ViewMessages()
        {
            MessagesViewModel messages = new()
            {
                // Messages=messagesService.ReadMessages()
            };
            return View(messages);
        }
    }
}
