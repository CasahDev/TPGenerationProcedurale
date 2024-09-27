using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPGenerationProcedurale.Model.Images;

namespace TPGenerationProcedurale.Model.Algorithms
{
    /// <summary>
    /// Algorithm maker for the abstract factory
    /// </summary>
    public interface IAlgorithmMaker
    {
        /// <summary>
        /// Creation of the algorithm
        /// </summary>
        /// <returns>The algorithm</returns>
        IAlgorithm Create();
    }
}
