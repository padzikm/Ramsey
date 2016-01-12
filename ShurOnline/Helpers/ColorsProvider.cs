using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShurOnline
{
    public class ColorsProvider
    {
        private static List<string> colors;
        public static List<string> Colors
        {
            get
            {
                if (colors == null)
                {
                    colors = new List<string>();
                    var type = typeof(System.Windows.Media.Colors);
                    var colorInfo = type.GetProperties(BindingFlags.Public |
                        BindingFlags.Static);
                    foreach (var info in colorInfo)
                    {
                        colors.Add(info.Name);
                    }
                }
                return colors;
            }
        }
    }
}
