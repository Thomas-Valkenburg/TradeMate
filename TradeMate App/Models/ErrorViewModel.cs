<<<<<<<< HEAD:Web/Models/ErrorViewModel.cs
namespace Web.Models;
========
namespace TradeMate_App.Models;
>>>>>>>> 18a6114e3b9a14f2fd25ae9bde0ca0f20161e8ef:TradeMate App/Models/ErrorViewModel.cs

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}