using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corgi
{
    public class Foldable : Checkbox
    {
        public string decoration = "-";
        public int indent = 3;

        private int items = 0;

        public Foldable(int _x, int _y) :
            base(_x, _y)
        {
            onValueChanged += OnValueChanged;

            drawChildren = isChecked;
        }

        private void OnValueChanged(bool isChecked)
        {
            parent.InvalidateChildren();

            drawChildren = isChecked;
            foreach (var c in children)
                c.isActive = isChecked;

            Console.Clear();
            //if (isChecked == false)
            //    Console.Clear();
            //InvalidateChildren();

            parent.Draw();
        }

        public void AddItem(UiElement item)
        {
            item.isActive = isChecked;
            item.x = indent;
            item.y = items + 1;

            AddChild(item);

            var deco = AddChild(new TextLine(decoration));
            deco.height = 0;
            deco.x = indent - 2;
            deco.y = item.y;

            items++;
        }

        protected override void DrawChildren(int x, int y)
        {
            foreach (var c in children)
            {
                if (string.IsNullOrEmpty(decoration) == false)
                    WriteAt(x + c.x, y + c.y, decoration + " ");

                c.Draw(x + c.x, y + c.y);
            }
        }
    }
}
