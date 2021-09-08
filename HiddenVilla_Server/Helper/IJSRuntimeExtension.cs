using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace HiddenVilla_Server.Helper
{
    public static class IJSRuntimeExtension 
    {
        public static async ValueTask ToastrSuccess(this IJSRuntime iJsRuntime, string message)
        {
            await iJsRuntime.InvokeVoidAsync("showToastr", "success", message);
        }

        public static async ValueTask ToastrError(this IJSRuntime iJsRuntime, string message)
        {
            await iJsRuntime.InvokeVoidAsync("showToastr", "error", message);
        }
    }
}
