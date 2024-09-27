using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPGenerationProcedurale.Model.Palettes;
using TPGenerationProcedurale.Model.Palettes.Realisations;

namespace TPGenerationProcedurale.Model.Algorithms.Realisations
{
    /// <summary>
    /// Algorithm that produce a horizontal gradient
    /// </summary>
    public class AlgorithmHorizontalGradient : AbstractAlgorithm
    {
        private int nbStep; //Number of the step in the algorithm

        public AlgorithmHorizontalGradient() : base()
        {
            this.nbStep = 0;
        }

        public override List<IPalette> Palettes => new List<IPalette>() { new RedPalette(), new BlackAndWhitePalette()};


        public override int ClosestValideSize(int size)
        {
            //This algorithm work no matter the size of the image is
            return size;
        }

        public override void NextStep()
        {
            for (int column = 0; column < Image.Width; column++)
                Image.GetPixels(nbStep, column).Nuance = (nbStep * 255)/Image.Height;

            nbStep++;
            if (nbStep >= Image.Height) this.End();
        }

        /// <summary>
        /// Initialisation of the algorithm 
        /// </summary>
        public override void Start()
        {
            base.Start();
            for (int line = 0; line < Image.Height; line++)
                for (int column = 0; column < Image.Width; column++)
                    Image.GetPixels(line, column).Nuance = 0;
        }
    }
}
