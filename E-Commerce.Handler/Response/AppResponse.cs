using System.Net;

namespace E_Commerce.Handler.JWTToken.Intrefaces
{
    public class AppResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public Object? Data { get; set; }
}
}