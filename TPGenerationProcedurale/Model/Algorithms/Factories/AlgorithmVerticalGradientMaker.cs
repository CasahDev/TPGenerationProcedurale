using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPGenerationProcedurale.Model.Algorithms.Realisations;
using TPGenerationProcedurale.Model.Images;

namespace TPGenerationProcedurale.Model.Algorithms.Factories
{
    /// <summary>
    /// Maker of the Vertical Gradient Algorithm
    /// </summary>
    public class AlgorithmVerticalGradientMaker : IAlgorithmMaker
    {
        public IAlgorithm Create()
        {
            return new AlgorithmVerticalGradient();
        }
    }
}
