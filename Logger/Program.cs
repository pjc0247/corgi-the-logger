using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Win32;


namespace Corgi
{
	public class SC
	{
		public static readonly string Gray = "\x1B[90m";
		public static readonly string Green = "\x1B[92m";
	}

	
	
	

	class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;

			var c = new ConsoleInput();
			c.Start();

			c.onMouseClick = (int x, int y) =>
			{
			};

			/*
			var root = new UiRoot();

			var cb = new Foldable(10, 10);
			cb.caption = "SystemLog";

			cb.AddItem(new TextLine("Corgi doge computer"));
			cb.AddItem(new TextLine("Bitcoin pump"));
			cb.AddItem(new TextLine("Scam coin"));
			cb.AddItem(new TextLine("Corgi poop desk zuzu"));

			var fd = new Foldable(10, 10);
			fd.caption = "Zuzu";
			fd.AddItem(new TextLine("you"));
			cb.AddItem(fd);

			root.AddChild(cb);
			root.Draw();
			*/

			Logger.WriteLine("test", "asdf");
			Logger.WriteLine("test", "qwer");
			Logger.WriteLine("asdf", "qwer");


			/*
			string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), @"bluehair.png");
			using (Graphics g = Graphics.FromHwnd(GetConsoleWindow()))
			{
				using (Image image = Image.FromFile(path))
				{
					Size fontSize = GetConsoleFontSize();

					var location = new Point(0, 0);
					var imageSize = new Size(80, 40);

					// translating the character positions to pixels
					Rectangle imageRect = new Rectangle(
						location.X * fontSize.Width,
						location.Y * fontSize.Height,
						imageSize.Width * fontSize.Width,
						imageSize.Height * fontSize.Height);
					g.DrawImage(image, imageRect);
				}
			}
			*/

			while (true)
			{

			}
		}

		private static Size GetConsoleFontSize()
		{
			// getting the console out buffer handle
			IntPtr outHandle = CreateFile("CONOUT$", GENERIC_READ | GENERIC_WRITE,
				FILE_SHARE_READ | FILE_SHARE_WRITE,
				IntPtr.Zero,
				OPEN_EXISTING,
				0,
				IntPtr.Zero);
			int errorCode = Marshal.GetLastWin32Error();
			if (outHandle.ToInt32() == INVALID_HANDLE_VALUE)
			{
				throw new IOException("Unable to open CONOUT$", errorCode);
			}

			ConsoleFontInfo cfi = new ConsoleFontInfo();
			if (!GetCurrentConsoleFont(outHandle, false, cfi))
			{
				throw new InvalidOperationException("Unable to get font information.");
			}

			return new Size(cfi.dwFontSize.X, cfi.dwFontSize.Y);
		}

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr GetConsoleWindow();

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr CreateFile(
			string lpFileName,
			int dwDesiredAccess,
			int dwShareMode,
			IntPtr lpSecurityAttributes,
			int dwCreationDisposition,
			int dwFlagsAndAttributes,
			IntPtr hTemplateFile);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool GetCurrentConsoleFont(
			IntPtr hConsoleOutput,
			bool bMaximumWindow,
			[Out][MarshalAs(UnmanagedType.LPStruct)]ConsoleFontInfo lpConsoleCurrentFont);

		[StructLayout(LayoutKind.Sequential)]
		internal class ConsoleFontInfo
		{
			internal int nFont;
			internal Coord dwFontSize;
		}

		[StructLayout(LayoutKind.Explicit)]
		internal struct Coord
		{
			[FieldOffset(0)]
			internal short X;
			[FieldOffset(2)]
			internal short Y;
		}

		private const int GENERIC_READ = unchecked((int)0x80000000);
		private const int GENERIC_WRITE = 0x40000000;
		private const int FILE_SHARE_READ = 1;
		private const int FILE_SHARE_WRITE = 2;
		private const int INVALID_HANDLE_VALUE = -1;
		private const int OPEN_EXISTING = 3;
	}
}
