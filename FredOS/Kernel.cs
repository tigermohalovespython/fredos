using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
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

            Console.WriteLine("Kenrnel booted.");
            Console.WriteLine("Booting os.");

            LUA.Run(File.ReadAllText(@"1:\OSinit.lua"));

        }

        protected override void Run()
        {

        }
    }
}
