using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPGenerationProcedurale.Model.Algorithms.Realisations;

namespace TPGenerationProcedurale.Model.Algorithms.Factories
{
    /// <summary>
    /// Maker of the Horizontal Gradient Algorithm
    /// </summary>
    public class AlgorithmHorizontalGradientMaker : IAlgorithmMaker
    {
        public IAlgorithm Create()
        {
            return new AlgorithmHorizontalGradient();
        }
    }
}
