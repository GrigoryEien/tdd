using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fclp;

namespace TagsCloudVisualization
{
    class Program
    {
        static void Main(string[] args)
        {
            var count = 0;
            string destination = null;
            string source = null;

            var p = new FluentCommandLineParser();
            p.Setup<int>("c", "count").Callback(x => count = x);
            p.Setup<string>("s", "source").Callback(x => source = x).Required();
            p.Setup<string>("d", "destination").Callback(x => destination = x).Required();
            p.Parse(args);

            if (source is null || destination is null)
            {
                Console.WriteLine("Destination and source are required");
                return;
            }

            if (count == 0)
            {
                count = 100;
                Console.WriteLine("Default words count is 100");
            }

            IEnumerable<string> lines;
            try
            {
                lines = File.ReadLines(source);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File " + source + " not found");
                return;
            }
            
            var cloud = CloudBilder.BuildCloud(lines, count);
            
            cloud.Save(destination);
            Console.WriteLine("Saved to " + destination);
        }
    }
}