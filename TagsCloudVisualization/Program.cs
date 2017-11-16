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
            var mostFrequentWords = FrequencyAnalyzer.GetMostFrequentWords(@"book1.txt",200);
            mostFrequentWords = FrequencyAnalyzer.NormalizeDictionary(mostFrequentWords);
//            foreach (var mostFrequentWord in mostFrequentWords)
//            {
//                Console.WriteLine(mostFrequentWord);
//            }
            var cloudVisualizer = new CloudSaver(new Point(800, 800));
            cloudVisualizer.SaveBitmapToFile(mostFrequentWords,"song.bmp");
        }
    }
}
