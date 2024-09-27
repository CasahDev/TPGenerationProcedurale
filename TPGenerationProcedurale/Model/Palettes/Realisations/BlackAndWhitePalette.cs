using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TPGenerationProcedurale.Model.Palettes.Realisations
{
    /// <summary>
    /// Black and white palette (filter version)
    /// </summary>
    public class BlackAndWhitePalette : IPalette
    {
        public string Name => "Black and white";

        public Color GetColor(int nuance)
        {
            Color res = Colors.Red;
            if (nuance > -1 && nuance < 163) res = Colors.Black;
            else if (nuance >= 163) res = Colors.White;
            return res;
        }
    }
}
