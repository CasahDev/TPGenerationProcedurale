using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TPGenerationProcedurale.Model.Palettes
{
    /// <summary>
    /// Interface of a color palette
    /// </summary>
    public interface IPalette
    {
        /// <summary>
        /// Name of the color palette
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Return the color (R,G,B) associated to the given nuance in the palette
        /// nuance = -1 => undefined color
        /// </summary>
        /// <param name="nuance">The nuance</param>
        /// <returns>The associated color</returns>
        Color GetColor(int nuance);

    }
}
