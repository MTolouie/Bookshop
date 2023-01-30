using Microsoft.JSInterop;
namespace Bookshop_BlazorServer.Utilities;

public static class IJSRuntimeExtensions
{

    #region TOASTR

    public async static Task ToastrSuccess(this IJSRuntime JSRuntime, string message)
    {
        await JSRuntime.InvokeVoidAsync("ShowToastr", "success", message);
    }

    public async static Task ToastrError(this IJSRuntime JSRuntime, string message)
    {
        await JSRuntime.InvokeVoidAsync("ShowToastr", "error", message);
    }

    public async static Task ToastrWarning(this IJSRuntime JSRuntime, string message)
    {
        await JSRuntime.InvokeVoidAsync("ShowToastr", "warning", message);
    }

    #endregion

    #region SWEATALERT

    public async static Task Swal(this IJSRuntime IJSRuntime, string type, string title, string message)
    {
        await IJSRuntime.InvokeVoidAsync("ShowSweatAlert", type, title, message);
    }

    #endregion

}
