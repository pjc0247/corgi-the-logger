using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corgi
{
    public class TextLine : UiElement
    {
        public string msg;

        public TextLine(string _msg)
        {
            msg = _msg;

            width = msg.Length;
            height = 1;
        }

        protected override void DrawInternal()
        {
            Console.WriteLine(msg);
        }
    }
}
