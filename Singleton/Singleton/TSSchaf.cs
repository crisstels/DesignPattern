using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
// A Thread safe Singleton
namespace Singleton
{
    class TSSchaf
    {
        private string _color;

        public string Color { get => _color; set => _color = value; }

        private TSSchaf(string color)
        {
            Color = color;
        }

        private static TSSchaf _instance;
        private static readonly object _lock = new object();

        public static TSSchaf getInstance(string color)
        {
            if(_instance == null)
            {
                lock (_lock)
                {
                    if(_instance == null)
                    {
                        _instance = new TSSchaf(color);
                    }
                }
            }

            return _instance;
        }
    }
}
