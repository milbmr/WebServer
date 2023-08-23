using System.Net;
using System.Net.Sockets;
using System.Text;
using Extensions;

namespace WebServerConfig;

public class Server
{
    public int MaxConnections { get; set; }
    protected Semaphore? maxThreads;

    public Server()
    {
        MaxConnections = 20;
        maxThreads = new(MaxConnections, MaxConnections);
    }

    public void Start()
    {
        List<IPAddress> localIps = GetLocalHostIPs();
        HttpListener listener = InitilizeListener(localIps, 800);
        Start(listener);
    }

    private HttpListener InitilizeListener(List<IPAddress> localIp, int port)
    {
        HttpListener listener = new();
        string url = UrlWithPort("http://localhost", port);
        listener.Prefixes.Add(url);

        localIp.ForEach(ip =>
        {
            string url = UrlWithPort("http://" + ip.ToString(), port);
            Console.WriteLine("Listening on ip " + url);
            listener.Prefixes.Add(url);
        });

        return listener;
    }

    private string UrlWithPort(string url, int port)
    {
        string output = url + "/";

        if (port != 80)
        {
            output = url + ":" + port.ToString() + "/";
        }

        return output;
    }

    private List<IPAddress> GetLocalHostIPs()
    {
        IPHostEntry host;
        host = Dns.GetHostEntry(Dns.GetHostName());
        List<IPAddress> ipies = host.AddressList
            .Where(ip => ip.AddressFamily == AddressFamily.InterNetwork)
            .ToList();

        return ipies;
    }

    private void Start(HttpListener listener)
    {
        listener.Start();
        Task.Run(() => RunServer(listener));
    }

    private void RunServer(HttpListener listener)
    {
        while (true)
        {
            maxThreads!.WaitOne();
            StartListener(listener);
        }
    }

    private async void StartListener(HttpListener listener)
    {
        HttpListenerContext context = await listener.GetContextAsync();
        maxThreads!.Release();

        Log(context.Request);

        string response = "hello browser";
        byte[] encoded = Encoding.UTF8.GetBytes(response);
        context.Response.ContentLength64 = encoded.Length;
        context.Response.OutputStream.Write(encoded, 0, encoded.Length);
        context.Response.OutputStream.Close();
    }

    private void Log(HttpListenerRequest request)
    {
        Console.WriteLine(
            request.RemoteEndPoint
                + " "
                + request.HttpMethod
                + " /"
                + request.Url!.AbsoluteUri.RightOf('/', 3)
        );
    }
}
