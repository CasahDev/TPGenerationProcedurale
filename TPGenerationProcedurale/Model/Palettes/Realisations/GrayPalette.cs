using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TPGenerationProcedurale.Model.Palettes.Realisations
{
    /// <summary>
    /// Shades of gray palette
    /// </summary>
    public class GrayPalette : IPalette
    {
        public string Name => "Shades of gray";

        public Color GetColor(int nuance)
        {
            Color color = new Color();

            if (nuance == -1) color = Colors.Red;
            else
            {
                color.R = BitConverter.GetBytes(nuance)[0];
                color.G = BitConverter.GetBytes(nuance)[0];
                color.B = BitConverter.GetBytes(nuance)[0];
            }

            return color;
        }
    }
}
