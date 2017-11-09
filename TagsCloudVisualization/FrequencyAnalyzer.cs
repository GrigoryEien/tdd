using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace TagsCloudVisualization
{
    public class FrequencyAnalyzer
    {
        public static Dictionary<string, int> GetMostFrequentWords(int wordsCount)
        {


            string text = System.IO.File.ReadAllText(@"text_for_tdd.txt");

            Dictionary<string, int> count =
                    text.Split(' ')
                        .Select(s => s.TrimEnd(new char[]{'.','.'}))
                        .Where(s => s.Length > 5)
                        .GroupBy(s => s)
                        .OrderByDescending(s => s.Count())
                        .Take(wordsCount)
                        .ToDictionary(g => g.Key, g => g.Count())
                ;
            return count;
            //        foreach (var word in text){}

            //        words.GroupBy(w => w).ToDictionary(w => w.Key, w => w.Count());
        }

        public static Dictionary<string, int> NormalizeDictionary(Dictionary<string, int> dict)
        {
            var ratio = 100.0 / dict.Values.Max();
            var normalizedList = dict.ToDictionary(x => x.Key, x => ConvertValue((int)(x.Value * ratio)));
            return normalizedList;
        }

        private static int ConvertValue(int value)
        {
            var OldRange = (100 - 0);
            var NewRange = (50 - 10);
            return (((value - 0) * NewRange) / OldRange) + 10;
        }
    }
}