using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using static Corgi.NativeMethods;

namespace Corgi
{
    delegate void MouseClickDelegate(int x, int y);

    class ConsoleInput
    {
        public static ConsoleInput instance;

        public MouseClickDelegate onMouseClick;

        public ConsoleInput()
        {
            instance = this;
        }
        public void Start()
        {
            IntPtr inHandle = GetStdHandle(STD_INPUT_HANDLE);
            uint mode = 0;
            GetConsoleMode(inHandle, ref mode);
            mode &= ~ENABLE_QUICK_EDIT_MODE; //disable
            mode |= ENABLE_WINDOW_INPUT; //enable (if you want)
            mode |= ENABLE_MOUSE_INPUT; //enable
            mode |= 4;
            SetConsoleMode(inHandle, mode);


            IntPtr outHandle = GetStdHandle(STD_OUTPUT_HANDLE);
            GetConsoleMode(outHandle, ref mode);
            mode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING;
            SetConsoleMode(outHandle, mode);

            IntPtr handleIn = GetStdHandle(STD_INPUT_HANDLE);
            new Thread(() =>
            {
                while (true)
                {
                    uint numRead = 0;
                    INPUT_RECORD[] record = new INPUT_RECORD[1];
                    record[0] = new INPUT_RECORD();
                    ReadConsoleInput(handleIn, record, 1, ref numRead);

                    switch (record[0].EventType)
                    {
                        case INPUT_RECORD.MOUSE_EVENT:
                            {
                                var e = record[0].MouseEvent;

                                if (e.dwButtonState == MOUSE_EVENT_RECORD.FROM_LEFT_1ST_BUTTON_PRESSED)
                                    onMouseClick?.Invoke(e.dwMousePosition.X, e.dwMousePosition.Y);
                                break;
                            }
                            /*
                            case INPUT_RECORD.KEY_EVENT:
                                KeyEvent?.Invoke(record[0].KeyEvent);
                                break;
                            case INPUT_RECORD.WINDOW_BUFFER_SIZE_EVENT:
                                WindowBufferSizeEvent?.Invoke(record[0].WindowBufferSizeEvent);
                                break;
                            */
                    }
                }
            }).Start();
        }
    }
}
