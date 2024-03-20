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
    public static class LUA_FS
    {
        public const string LIB_NAME = "FredOS.FileSystem";

        public static int OpenLib(ILuaState lua)
        {
            var define = new NameFuncPair[]
            {
            new NameFuncPair("readalltext", LUA_READTEXT),
            new NameFuncPair("writealltext", LUA_WRITETEXT),
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

            lua.PushString(File.ReadAllText(lua.ToString(1)));
            return 1;
        }
        public static int LUA_WRITETEXT(ILuaState lua)
        {

            File.WriteAllText(lua.ToString(1), lua.ToString(2));
            return 1;
        }

    }
}
