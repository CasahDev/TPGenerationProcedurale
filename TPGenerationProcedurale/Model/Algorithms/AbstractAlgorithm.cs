using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TPGenerationProcedurale.Model.Images;
using TPGenerationProcedurale.Model.Palettes;

namespace TPGenerationProcedurale.Model.Algorithms
{
    /// <summary>
    /// Abstract class for an algorithm
    /// </summary>
    public abstract class AbstractAlgorithm : IAlgorithm
    {
        /// <summary>
        /// Image used by the algorithm
        /// </summary>
        protected PixelImage Image => image;
        private PixelImage image;
        /// <summary>
        /// Seed used by the algorithm
        /// </summary>
        protected string Seed => seed;
        private string seed;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="image">Image used by the algorithm</param>
        public AbstractAlgorithm()
        {
            this.image = null;
            this.State = AlgorithmState.NOTSTARTED;
        }
        public void setImage(PixelImage image)
        {
            this.image = image;
        }
        public void setSeed(string seed)
        {
            this.seed = seed;
        }




        #region State management
        /// <summary>
        /// State of the algorithm
        /// </summary>
        private AlgorithmState state;

        public AlgorithmState State
        {
            get => state;
            set
            {
                this.state = value;
                NotifyPropertyChanged();
            }
        }

        public virtual void Start()
        {
            this.State = AlgorithmState.RUNNING;
        }
        protected void End()
        {
            this.State = AlgorithmState.FINISHED;
        }
        #endregion


        #region Not implemented rest of the interface
        public abstract List<IPalette> Palettes { get; }
        public abstract int ClosestValideSize(int size);
        public abstract void NextStep();
        #endregion


        #region Observable pattern
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
