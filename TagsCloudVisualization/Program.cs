using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TagsCloudVisualization {
    class Program {
        static void Main(string[] args)
        {
	        var lines = FileReader.ReadLines(@"text_for_tdd.txt");
            var frequentWords = FrequencyAnalyzer.GetFrequencyDict(lines);
	        var mostFrequentWords = frequentWords.OrderByDescending(x => x.Value).Take(100).ToDictionary(x => x.Key,x => x.Value);
	        mostFrequentWords = DictionaryNormalizer.NormalizeDictionary(mostFrequentWords);
	        foreach (var word in mostFrequentWords)
	        {
		        Console.WriteLine(word.Key +'|'+word.Value);
	        }
            var cloudVisualizer = new CloudSaver(new Point(800, 800));
			var layouter = new CircularCloudLayouter(new Point(800, 800));
	        var rects = layouter.CalculateRectsForWords(mostFrequentWords);
            cloudVisualizer.SaveBitmapToFile(rects,"not_song.bmp");
        }
    }
}
