using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Extensions;

namespace WebServer;

public class Utils
{
    public string GetPath()
    {
        char directorySeparator = Path.DirectorySeparatorChar;
        string webSite = Assembly.GetExecutingAssembly().Location;
        webSite = webSite.LeftOfRight(directorySeparator, 4) + directorySeparator + "WebSite";
        return webSite;
    }
}
