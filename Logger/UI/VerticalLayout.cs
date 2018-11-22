using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corgi
{
    public class VerticalLayout : UiElement
    {
        protected override void DrawChildren(int x, int y)
        {
            var offset = 0;
            foreach (var c in children)
            {
                c.Draw(x + c.x, y + offset);
                offset += c.GetHeight();
            }
        }
    }
}
