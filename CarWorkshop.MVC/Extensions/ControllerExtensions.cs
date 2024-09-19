namespace CarWorkshop.MVC.Extensions;

using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;

public static class ControllerExtensions
{
    public static void SetNotification(this Controller controller, string type, string message)
    {
        var notification = new Notification(type, message);
        controller.TempData["Notification"] = JsonConvert.SerializeObject(notification);
    }
}