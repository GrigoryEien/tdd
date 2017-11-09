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
            var mostFrequentWords = FrequencyAnalyzer.GetMostFrequentWords(100);
            mostFrequentWords = FrequencyAnalyzer.NormalizeDictionary(mostFrequentWords);
            foreach (var mostFrequentWord in mostFrequentWords)
            {
                Console.WriteLine(mostFrequentWord);
            }
//            var cloudVisualizer = new CloudVisualizer(new Point(400, 400));
//            cloudVisualizer.DrawBitmap();
        }
    }
}
