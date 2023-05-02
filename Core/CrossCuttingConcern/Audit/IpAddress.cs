using System.Net.Sockets;
using System.Net;

namespace Identity_Session.Core.CrossCuttingConcern.Audit
{
    public static class IpAddress
    {
        public static string FindUserIp()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
            //return "";
        }
    }
}
