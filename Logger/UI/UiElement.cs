using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corgi
{
	public class UiElement
	{
		public int x, y;
		public int width, height;
		public bool isActive = true;

		public UiElement parent;

		protected int lastX, lastY;

		protected bool drawChildren = true;
		protected List<UiElement> children;

		public UiElement()
		{
			children = new List<UiElement>();
		}

		public UiElement AddChild(UiElement c)
		{
			children.Add(c);
			c.parent = this;
			return c;
		}
		public void RemoveChild(UiElement c)
		{
			c.parent = null;
			children.Remove(c);
		}

		public int GetHeight()
		{
			var h = 0;
			foreach (var c in children)
			{
				if (c.isActive == false) continue;
				h += c.GetHeight();
			}
			return h + height;
		}

		protected bool CheckXY(int _x, int _y)
		{
			if (_x >= lastX && _x < lastX + width &&
				_y >= lastY && _y < lastY + height)
				return true;
			return false;
		}

		public virtual void Invalidate()
		{
			var prevX = Console.CursorLeft;
			var prevY = Console.CursorTop;

			for (int i = lastX; i < lastX + width; i++)
			{
				for (int j = lastY; j < lastY + GetHeight(); j++)
				{
					Console.CursorLeft = i;
					Console.CursorTop = j;

					Console.Write(" ");
				}
			}

			Console.CursorLeft = prevX;
			Console.CursorTop = prevY;
		}
		public void InvalidateChildren()
		{
			Invalidate();
			foreach (var c in children)
				c.InvalidateChildren();
		}

		public virtual void Draw()
		{
			Draw(lastX, lastY);
		}
		public virtual void Draw(int x, int y)
		{
			var prevX = Console.CursorLeft;
			var prevY = Console.CursorTop;
			Console.CursorLeft = x;
			Console.CursorTop = y;

			lastX = x;
			lastY = y;
			DrawInternal();
			if (drawChildren)
				DrawChildren(x, y);

			Console.CursorLeft = prevX;
			Console.CursorTop = prevY;
		}
		protected virtual void DrawChildren(int x, int y)
		{
			foreach (var c in children)
				c.Draw(x + c.x, y + c.y);
		}
		protected virtual void DrawInternal()
		{

		}

		protected void WriteAt(int x, int y, string msg)
		{
			var prevX = Console.CursorLeft;
			var prevY = Console.CursorTop;
			Console.CursorLeft = x;
			Console.CursorTop = y;

			Console.Write(msg);

			Console.CursorLeft = prevX;
			Console.CursorTop = prevY;
		}
	}
}
