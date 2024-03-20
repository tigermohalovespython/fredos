using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using Lynox.Additions.LUA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Sys = Cosmos.System;

namespace FredOS
{
    public class Kernel : Sys.Kernel
    {

        CosmosVFS vfs;

        protected override void BeforeRun()
        {

            vfs = new CosmosVFS();
            VFSManager.RegisterVFS(vfs);

            using (var xClient = new DHCPClient())
            {
                /** Send a DHCP Discover packet **/
                //This will automatically set the IP config after DHCP response
                xClient.SendDiscoverPacket();
            }

            Console.WriteLine("Kenrnel booted.");
            Console.WriteLine("Booting os.");

            LUA.Run(File.ReadAllText(@"1:\OSinit.lua"));

        }

        protected override void Run()
        {

        }
    }
}
