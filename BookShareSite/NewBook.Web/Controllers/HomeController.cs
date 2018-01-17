using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace NewBook.Web.Controllers
{
    public class HomeController : ApiController
    {
        private static readonly Dictionary<string, string> textMediaTypeMapping = new Dictionary<string, string>
        {
            { ".html", "text/html" },
            { ".js", "text/javascript" },
            { ".css", "text/css" },
            { ".json", "application/json" }
        };
        private static readonly Dictionary<string, string> streamMediaTypeMapping = new Dictionary<string, string>
        {
            { ".png", "image/png" },
            { ".gif", "image/gif" },
        };

        [HttpGet, Route("index.html")]
        public HttpResponseMessage Index()
        {
            return GetFile("index.html");
        }

        private string webRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../");
        private HttpResponseMessage GetFile(string filePath)
        {
            string file = Path.Combine(webRoot, filePath);
            if (File.Exists(file))
            {
                var extension = Path.GetExtension(file);

                if (textMediaTypeMapping.ContainsKey(extension))
                {
                    return GetTextResponse(file, extension);
                }

                if (streamMediaTypeMapping.ContainsKey(extension))
                {
                    return GetStreamResponse(file, extension);
                }

                return NotFound();
            }
            else
            {
                return NotFound();
            }
        }

        private new HttpResponseMessage NotFound()
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
        }

        private HttpResponseMessage GetTextResponse(string file, string extension)
        {
            var content = File.ReadAllText(file);
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            var mediaType = "text/plain";
            mediaType = textMediaTypeMapping[extension];
            response.Content = new StringContent(content, Encoding.UTF8, mediaType);
            return response;
        }

        private HttpResponseMessage GetStreamResponse(string file, string extension)
        {
            var content = File.OpenRead(file);
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            var mediaType = "text/plain";
            mediaType = streamMediaTypeMapping[extension];
            response.Content = new StreamContent(content);
            return response;
        }
    }
}
