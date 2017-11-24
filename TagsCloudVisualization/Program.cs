using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TagsCloudVisualization {
    class Program {
        static void Main(string[] args)
        {
	        if (args.Length != 3 || args[0] == "-h")
	        {
		        Console.WriteLine("This program will take first N1 words from file N2 and save words cloud to file N3");
		        return;
	        }
	        var lines = File.ReadLines(args[1]);
            var frequentWords = FrequencyAnalyzer.GetFrequencyDict(lines);
	        var mostFrequentWords = frequentWords
				.OrderByDescending(x => x.Value)
				.Take(int.Parse(args[0]))
				.ToDictionary(x => x.Key,x => x.Value);
	        mostFrequentWords = DictionaryNormalizer.NormalizeDictionary(mostFrequentWords);
	        var rects = CloudBilder.CalculateRectsForWords(mostFrequentWords, new Point(0,0));
            var cloud = CloudDrawer.DrawMap(rects);
	        cloud.Save(args[1]);
        }
    }
}
