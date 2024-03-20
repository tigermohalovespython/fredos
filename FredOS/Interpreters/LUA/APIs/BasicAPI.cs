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
    public static class LUA_BASIC
    {
        public const string LIB_NAME = "FredOS.Basic";

        public static int OpenLib(ILuaState lua)
        {
            var define = new NameFuncPair[]
            {
            new NameFuncPair("run", LUA_RUNBASIC),
            //new NameFuncPair("clearcanvas", LUA_CLEARCANVAS),
            //new NameFuncPair("drawfilledrect", LUA_DRAWFILLEDRECT),
            //new NameFuncPair("drawrect", LUA_DRAWRECT),
            //new NameFuncPair("displaycanvas", LUA_DISPLAYCANVAS),
            };
            lua.L_NewLib(define);
            return 1;
        }
        //FILES
        public static int LUA_RUNBASIC(ILuaState lua)
        {

            Basic.Run(lua.ToString(1));
            return 1;
        }
        
    }
}
