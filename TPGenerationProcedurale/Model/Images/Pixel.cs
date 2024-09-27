using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TPGenerationProcedurale.Model.Images
{
    /// <summary>
    /// Pixel in a PixelImage
    /// </summary>
    public class Pixel : INotifyPropertyChanged
    {

        /// <summary>
        /// column of the pixel (from left to right)
        /// </summary>
        public int X => x;
        private int x;

        /// <summary>
        /// Row of the pixel (from top to bottom)
        /// </summary>
        public int Y => y;
        private int y;

        /// <summary>
        /// Value of the pixel (between 0 and 255 or -1 for undifined) 
        /// </summary>
        public int Nuance
        {
            get => nuance;
            set
            {
                nuance = value;
                NotifyPropertyChanged();
            }
        }
        private int nuance;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">Column of the pixel</param>
        /// <param name="y">Row of the pixel</param>
        public Pixel(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.nuance = -1;
        }

        #region Observable pattern
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
