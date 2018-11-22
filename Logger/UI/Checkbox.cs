using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corgi
{
    public class Checkbox : UiElement
    {
        public delegate void ValueChangedDelegate(bool isChecked);

        public bool isChecked = false;
        private string _caption;
        public string caption
        {
            get => _caption;
            set
            {
                _caption = value;
                width = 3 + value.Length;
            }
        }

        public ValueChangedDelegate onValueChanged;

        public Checkbox(int _x, int _y)
        {
            ConsoleInput.instance.onMouseClick += OnMouseClick;

            x = _x; y = _y;
            width = 2; height = 1;
        }
        ~Checkbox()
        {
            ConsoleInput.instance.onMouseClick -= OnMouseClick;
        }

        private void OnMouseClick(int x, int y)
        {
            if (CheckXY(x, y) == false) return;
            isChecked ^= true;

            Draw();

            onValueChanged?.Invoke(isChecked);
        }
        protected override void DrawInternal()
        {
            if (isChecked)
                Console.Write("☑ ");
            else
                Console.Write("☐ ");

            if (string.IsNullOrEmpty(caption) == false)
            {
                string sc = SC.Gray;
                if (isChecked)
                    sc = SC.Green;

                Console.Write(" " + sc + caption);
                Console.ResetColor();
            }
        }
    }
}
