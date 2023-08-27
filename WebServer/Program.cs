using WebServer;
using WebServerConfig;
using Extensions;

//Server server = new();

Utils getPath = new();
string path = getPath.GetPath();

Console.WriteLine(path);

Console.ReadLine();
