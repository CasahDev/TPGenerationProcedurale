using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGenerationProcedurale.Model.Images
{
    /// <summary>
    /// A image composed by pixels
    /// </summary>
    public class PixelImage
    {
       
        private Pixel[,] pixels;  // pixel matrix
        private int width; //width of the image
        private int height; //height of the image

        /// <summary>
        /// Width of the image
        /// </summary>
        public int Width => width;
        /// <summary>
        /// Height of the image
        /// </summary>
        public int Height => height;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="height">Height of the image</param>
        /// <param name="width">Width of the image</param>
        public PixelImage(int height,int width)
        {
            //Initialisation of the pixel matrix
            this.pixels = new Pixel[height,width];
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    this.pixels[x, y] = new Pixel(x, y);

            //Width and height
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// Return the pixel at the given position (null if there is no pixel at this position)
        /// </summary>
        /// <param name="x">Column of the pixel</param>
        /// <param name="y">Row of the pixel</param>
        /// <returns></returns>
        public Pixel GetPixels(int x,int y)
        {
            Pixel res = null;
            if (x >= 0 && x < width && y >= 0 && y < height) res = pixels[x, y];
            return res;
        }
    }
}
