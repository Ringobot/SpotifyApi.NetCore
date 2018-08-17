// https://www.recaffeinate.co/post/serialize-errors-as-json-in-aspnetcore/
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

public class JsonExceptionMiddleware
{
    public async Task Invoke(HttpContext context)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (ex == null) return;

        var error = new
        {
            error = new
            {
                message = ex.Message
            }
        };

        context.Response.ContentType = "application/json";

        using (var writer = new StreamWriter(context.Response.Body))
        {
            new JsonSerializer().Serialize(writer, error);
            await writer.FlushAsync().ConfigureAwait(false);
        }
    }
}