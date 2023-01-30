using Microsoft.JSInterop;
namespace Bookshop_BlazorClient.Utilities;

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

    //public async static Task ShowSwalModal(this IJSRuntime IJSRuntime, string type, string title, string message)
    //{
    //    await IJSRuntime.InvokeAsync<string>("ShowSwalModal", type, title, message);
    //}

    #endregion

    #region PgwSlideshow 
    public async static Task StartProductSlider(this IJSRuntime IJSRuntime)
    {
        await IJSRuntime.InvokeVoidAsync("StartProductSlider");
    }

    #endregion

}
