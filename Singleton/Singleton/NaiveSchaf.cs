using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// this Singleton is not Thread safe
namespace Singleton
{
    class NaiveSchaf
    {
        private string _color;
        private static NaiveSchaf _instance;

        public string Color { get => _color; set => _color = value; }

        private NaiveSchaf(string color)
        {
            Color = color;
        }

        public static NaiveSchaf getInstance(string color)
        {
            if(_instance == null)
            {
                _instance = new NaiveSchaf(color);
            }

            return _instance;
        }

        public void DoSomething()
        {
            Console.Write("määäääähh I'm a {0} sheep", Color);
        }
    }
}
