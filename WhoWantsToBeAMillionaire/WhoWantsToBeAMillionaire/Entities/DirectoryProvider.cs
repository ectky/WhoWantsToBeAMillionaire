using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.App.Contracts;

namespace Game.App.Entities
{
    public static class DirectoryProvider
    {
        public static string CurrentDirectory()
        {
            return @"../../Resources/";
        }
    }
}
