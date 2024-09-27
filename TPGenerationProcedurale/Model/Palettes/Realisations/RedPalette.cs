using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TPGenerationProcedurale.Model.Palettes.Realisations
{
    /// <summary>
    /// Shades of red palette
    /// </summary>
    public class RedPalette : IPalette
    {
        public string Name => "Shades of red";

        public Color GetColor(int nuance)
        {
            Color color = new Color();

            if (nuance == -1) color = Colors.White; 
            else
            {
                color.R = BitConverter.GetBytes(nuance)[0];
                color.G = BitConverter.GetBytes(0)[0];
                color.B = BitConverter.GetBytes(0)[0];
            }

            return color;
        }
    }
}
