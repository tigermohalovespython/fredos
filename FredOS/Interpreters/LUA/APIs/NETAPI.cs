using Cosmos.System.Graphics;
using Cosmos.System.Network.Config;
using Cosmos.System.Network.IPv4.UDP.DNS;
using Cosmos.System.Network.IPv4;
using CosmosHttp.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UniLua;

namespace Lynox.Additions.LUA.APIs
{
    public static class LUA_Networking
    {
        public const string LIB_NAME = "FredOS.Networking";

        public static int OpenLib(ILuaState lua)
        {
            var define = new NameFuncPair[]
            {
            new NameFuncPair("getcontent", LUA_READTEXT),
            new NameFuncPair("getip", LUA_READIP),
            //new NameFuncPair("clearcanvas", LUA_CLEARCANVAS),
            //new NameFuncPair("drawfilledrect", LUA_DRAWFILLEDRECT),
            //new NameFuncPair("drawrect", LUA_DRAWRECT),
            //new NameFuncPair("displaycanvas", LUA_DISPLAYCANVAS),
            };
            lua.L_NewLib(define);
            return 1;
        }
        //FILES
        public static int LUA_READTEXT(ILuaState lua)
        {

            lua.PushString(Http.DownloadFile(lua.ToString(1)));
            return 1;
        }
        public static int LUA_READIP(ILuaState lua)
        {

            lua.PushString(NetworkConfiguration.CurrentAddress.ToString());
            return 1;
        }

    }

    public static class Http
    {
        public static byte[] DownloadRawFile(string url)
        {
            if (url.StartsWith("https://"))
            {
                throw new WebException("HTTPS currently not supported, please use http://");
            }

            string path = ExtractPathFromUrl(url);
            string domainName = ExtractDomainNameFromUrl(url);

            var dnsClient = new DnsClient();

            dnsClient.Connect(DNSConfig.DNSNameservers[0]);
            dnsClient.SendAsk(domainName);
            Address address = dnsClient.Receive();
            dnsClient.Close();

            HttpRequest request = new();
            request.IP = address.ToString();
            request.Domain = domainName;
            request.Path = path;
            request.Method = "GET";
            request.Send();

            return request.Response.GetStream();
        }

        public static string DownloadFile(string url)
        {
            if (url.StartsWith("https://"))
            {
                throw new WebException("HTTPS currently not supported, please use http://");
            }

            string path = ExtractPathFromUrl(url);
            string domainName = ExtractDomainNameFromUrl(url);

            var dnsClient = new DnsClient();

            dnsClient.Connect(DNSConfig.DNSNameservers[0]);
            dnsClient.SendAsk(domainName);
            Address address = dnsClient.Receive();
            dnsClient.Close();

            HttpRequest request = new();
            request.IP = address.ToString();
            request.Domain = domainName;
            request.Path = path;
            request.Method = "GET";
            request.Send();

            return request.Response.Content;
        }

        private static string ExtractDomainNameFromUrl(string url)
        {
            int start;
            if (url.Contains("://"))
            {
                start = url.IndexOf("://") + 3;
            }
            else
            {
                start = 0;
            }

            int end = url.IndexOf("/", start);
            if (end == -1)
            {
                end = url.Length;
            }

            return url[start..end];
        }


        private static string ExtractPathFromUrl(string url)
        {
            int start;
            if (url.Contains("://"))
            {
                start = url.IndexOf("://") + 3;
            }
            else
            {
                start = 0;
            }

            int indexOfSlash = url.IndexOf("/", start);
            if (indexOfSlash != -1)
            {
                return url.Substring(indexOfSlash);
            }
            else
            {
                return "/";
            }
        }
    }

}
