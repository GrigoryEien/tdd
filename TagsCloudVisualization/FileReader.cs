using System.Collections.Generic;
using System.Text;

namespace TagsCloudVisualization
{
	public static class FileReader
	{
		public static IEnumerable<string> ReadLines(string textFilePath) {
			System.IO.StreamReader file =
				new System.IO.StreamReader(textFilePath, Encoding.UTF8);
			var line = file.ReadLine();
			while (line != null) {
				yield return line;
				line = file.ReadLine();
			}

			file.Close();
		}
	}
}