namespace GradTest.Utils;

public interface IHttpContextHandler
{
    public bool IsReusable { get; }
    public void ProcessRequest(HttpContext context);
}