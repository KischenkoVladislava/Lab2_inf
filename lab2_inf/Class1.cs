using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace lab2_inf
{
    static class Class1
    {
        public static DirectoryInfo directory = new DirectoryInfo("D:\\");
        public static Color graphicFilesColor = Color.Red;
        public static Color oficeFilesColor = Color.Aqua;
        public static Color archiveFilesColor = Color.MediumPurple;
        public static Color exeDllFilesColor = Color.DarkGreen;
        public static Font listViewFont;
        public static bool update = true;
    }
}
