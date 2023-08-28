using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerConfig.Router;

public class Router
{
    public string? WebsitePath { get; set; }
    private Dictionary<string, ContentInfo> folderMap;

    public Router()
    {
        folderMap = new Dictionary<string, ContentInfo>
        {
            {
                "ico",
                new ContentInfo() { ContentType = "image/ico", Loader = ImageLoader }
            },
            {
                "png",
                new ContentInfo() { ContentType = "image/png", Loader = ImageLoader }
            },
            {
                "jpg",
                new ContentInfo() { ContentType = "image/jpg", Loader = ImageLoader }
            },
            {
                "gif",
                new ContentInfo() { ContentType = "image/gif", Loader = ImageLoader }
            },
            {
                "bmp",
                new ContentInfo() { ContentType = "image/bmp", Loader = ImageLoader }
            },
            {
                "html",
                new ContentInfo() { ContentType = "text/html", Loader = PageLoader }
            },
            {
                "css",
                new ContentInfo() { ContentType = "text/css", Loader = FileLoader }
            },
            {
                "js",
                new ContentInfo() { ContentType = "text/javascript", Loader = FileLoader }
            },
            {
                "",
                new ContentInfo() { ContentType = "text/html", Loader = PageLoader }
            },
        };
    }

    private ResponsePacket ImageLoader(string path, string ext, ContentInfo contentinfo)
    {
        ResponsePacket output;
        if (!File.Exists(path))
        {
            Console.WriteLine("file doesn't exist");
        }

        using (FileStream fileS = new(path, FileMode.Open, FileAccess.Read))
        using (BinaryReader reader = new(fileS))
        {
            output = new()
            {
                Data = reader.ReadBytes((int)fileS.Length),
                ContentType = contentinfo.ContentType
            };
        }
        return output;
    }

    private ResponsePacket FileLoader(string path, string ext, ContentInfo contentinfo)
    {
        ResponsePacket output;
        if (!File.Exists(path))
        {
            Console.WriteLine("file doesn't exist");
        }

        string text = File.ReadAllText(path);
        output = new()
        {
            Data = Encoding.UTF8.GetBytes(text),
            ContentType = contentinfo.ContentType,
            Encoding = Encoding.UTF8
        };

        return output;
    }

    private ResponsePacket PageLoader(string path, string ext, ContentInfo contentinfo)
    {
        throw new NotImplementedException();
    }
}
