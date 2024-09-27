using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPGenerationProcedurale.Model.Images;
using TPGenerationProcedurale.Model.Palettes;

namespace TPGenerationProcedurale.Model.Algorithms
{
    /// <summary>
    /// Interface for the algorithms
    /// </summary>
    public interface IAlgorithm : INotifyPropertyChanged
    {        
        /// <summary>
        /// List of the possible palettes for the algorithm
        /// </summary>
        List<IPalette> Palettes { get; }

        /// <summary>
        /// return the closest (upper) valide size of the image for the algorithm
        /// </summary>
        /// <param name="size">Original size</param>
        /// <returns>Closest (upper) valide size</returns>
        int ClosestValideSize(int size);

        /// <summary>
        /// Set the image the algorithm with work on
        /// </summary>
        /// <param name="image">the new image</param>
        void setImage(PixelImage image);

        /// <summary>
        /// Execute the next step of the algorithm
        /// </summary>
        void NextStep();

        /// <summary>
        /// Start the algortihm
        /// </summary>
        void Start();
        

        /// <summary>
        /// State of the algorithm
        /// </summary>
        AlgorithmState State { get; }

        /// <summary>
        /// Set the seed for the algorithm
        /// </summary>
        /// <param name="seed">Seed</param>
        void setSeed(string seed);
    }
}
