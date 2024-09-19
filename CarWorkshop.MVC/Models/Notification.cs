namespace CarWorkshop.MVC.Models;

public class Notification(string type, string message)
{
    public string Type { get; set; } = type;
    public string Message { get; set; } = message;
}