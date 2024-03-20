using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniLua;

namespace Lynox.Additions.LUA.APIs
{
    public static class LUA_CLI
    {
        public const string LIB_NAME = "FredOS.CLI";

        public static int OpenLib(ILuaState lua)
        {
            var define = new NameFuncPair[]
            {
            new NameFuncPair("writeline", LUA_WRITELINE),
            new NameFuncPair("write", LUA_WRITE),
            new NameFuncPair("readline", LUA_READLINE),
            //new NameFuncPair("clearcanvas", LUA_CLEARCANVAS),
            //new NameFuncPair("drawfilledrect", LUA_DRAWFILLEDRECT),
            //new NameFuncPair("drawrect", LUA_DRAWRECT),
            //new NameFuncPair("displaycanvas", LUA_DISPLAYCANVAS),
            };
            lua.L_NewLib(define);
            return 1;
        }
        //FILES
        public static int LUA_READLINE(ILuaState lua)
        {

            lua.PushString(Console.ReadLine());
            return 1;
        }
        public static int LUA_WRITELINE(ILuaState lua)
        {

            Console.WriteLine(lua.ToString(1));
            return 1;
        }
        public static int LUA_WRITE(ILuaState lua)
        {

            Console.Write(lua.ToString(1));
            return 1;
        }

    }
}
