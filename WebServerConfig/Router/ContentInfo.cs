using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServerConfig.Router;

internal class ContentInfo
{
    public string? ContentType { get; set; }
    public Func<string, string, ContentInfo, ResponsePacket>? Loader { get; set; }
}
