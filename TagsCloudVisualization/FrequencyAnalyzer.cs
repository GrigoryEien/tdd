using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace TagsCloudVisualization
{
    public class FrequencyAnalyzer
    {
        public static Dictionary<string, int> GetMostFrequentWords(string textFilePath, int wordsCount)
        {
            Dictionary<string, int> count =
                    ReadLines(textFilePath).SelectMany(x => x.Split(' '))
                        .Select(s => s.ToLower().Trim(new[] {'.', ',', '\'','!'}))
                        .Where(s => s.Length > 3)
                        .GroupBy(s => s)
                        .OrderByDescending(s => s.Count())
                        .Take(wordsCount)
                        .ToDictionary(g => g.Key, g => g.Count())
                ;
            return count;
        }

        public static IEnumerable<string> ReadLines(string textFilePath)
        {
            System.IO.StreamReader file =
                new System.IO.StreamReader(textFilePath, Encoding.UTF8);
            var line = file.ReadLine();
            while (line != null)
            {
                yield return line;
                line = file.ReadLine();
            }

            file.Close();
        }

        public static Dictionary<string, int> NormalizeDictionary(Dictionary<string, int> dict)
        {
            var ratio = 100.0 / dict.Values.Max();
            var normalizedList = dict.ToDictionary(x => x.Key, x => ConvertValue((int) (x.Value * ratio)));
            return normalizedList;
        }

        private static int ConvertValue(int value)
        {
            var OldRange = (100 - 0);
            var NewRange = (100 - 10);
            return (((value - 0) * NewRange) / OldRange) + 10;
        }
    }
}