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
            var cloudVisualizer = new CloudVisualizer(new Point(200, 200));
            cloudVisualizer.DrawBitmap();
        }
    }
}
