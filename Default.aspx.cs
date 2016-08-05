using System;
using System.IO;
using System.Net;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var url = Request.QueryString["url"];
        var callback = Request.QueryString["callback"];
        if (!string.IsNullOrEmpty(url))
        {
            var hwr = WebRequest.Create(url).GetResponse();
            var stream = hwr.GetResponseStream();
            if (stream != null)
            {
                using (var sr = new StreamReader(stream))
                {
                    var format = "{0}";
                    var contentType = "application/json";
                    if (!string.IsNullOrEmpty(callback))
                    {
                        format = "{1}({0})";
                        contentType = "application/javascript";
                    }
                    Response.ContentType = contentType;
                    Response.Write(string.Format(format, sr.ReadToEnd(), callback));
                }
            }
        }
        Response.End();
    }
}