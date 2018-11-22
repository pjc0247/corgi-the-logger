using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corgi
{
	public class Logger
	{
		private static UiRoot root;
		private static VerticalLayout vertical;

		private static Dictionary<string, Foldable> categories;

		static Logger()
		{
			root = new UiRoot();
			vertical = new VerticalLayout();

			root.AddChild(vertical);

			categories = new Dictionary<string, Foldable>();
		}

		public static void WriteLine(string tag, string message)
		{
			if (categories.ContainsKey(tag) == false)
				CreateCategory(tag);

			categories[tag].AddItem(new TextLine(message));
		}

		private static void CreateCategory(string name)
		{
			var fd = new Foldable(0, 0);
			fd.caption = name;

			vertical.AddChild(fd);
			categories[name] = fd;

			root.Draw();
		}

		private string[] Tokenize(string tag)
		{
			return tag.Split('/');
		}
	}
}
