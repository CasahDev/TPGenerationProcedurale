using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using TPGenerationProcedurale.Model.Algorithms;
using TPGenerationProcedurale.Model.Algorithms.Factories;
using TPGenerationProcedurale.Model.Images;
using TPGenerationProcedurale.Model.Palettes;
using TPGenerationProcedurale.Model.Palettes.Realisations;

namespace TPGenerationProcedurale.ViewModel
{
    /// <summary>
    /// View model of the pixel image
    /// </summary>
    public class ViewModel : INotifyPropertyChanged
    {
        //Model
        private PixelImage image;           //Image (model)
        private IAlgorithm algorithm;       //Chosen algorithm
        private IPalette colorPalette;      //Chosen color palette
        private int size;                   //Chosen size
        private string seed;                //Chosen seed
        private string algorithmName;       //Name of the chosen algorithm
        private AlgorithmState state;       //Current state of the algorithm
        private DispatcherTimer timer;      //Timer for the loop

        #region Properties for binding

        /// <summary>
        /// State of the algorithm
        /// </summary>
        public AlgorithmState State
        {
            get => state;
            set
            {
                if (this.colorPalette == null) state = AlgorithmState.UNKNOWN;
                else state = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Seed for the random generator
        /// </summary>
        public string Seed
        {
            get => seed;
            set
            {
                seed = value;
                NotifyPropertyChanged();
                if (this.State == AlgorithmState.FINISHED) ReloadAlgorithm();
            }
        }
        /// <summary>
        /// Size of the map
        /// </summary>
        public int Size { get => size; 
            set
            {
                size = value;
                NotifyPropertyChanged();
                if (this.algorithmName != "") this.ReloadAlgorithm();
            }
        }

        #endregion

        #region observable lists for the combobox
        //List of the algorithm
        private ObservableCollection<string> algorithmsList;
        public ObservableCollection<string> AlgorithmsList
        {
            get => algorithmsList;
            private set
            {
                algorithmsList = value;
                NotifyPropertyChanged();
            }
        }

        //List of the palette
        private ObservableCollection<IPalette> palettesList;
        public ObservableCollection<IPalette> PalettesList
        {
            get => palettesList;
            private set
            {
                palettesList = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public ViewModel()
        {
            //Initialization of the algorithms
                //Initialization of the factory
            AlgorithmFactory.Register("Vertical Gradient Algorithm", new AlgorithmVerticalGradientMaker());
            AlgorithmFactory.Register("Horizontal Gradient Algorithm", new AlgorithmHorizontalGradientMaker());


            //Initialization of the list
            this.algorithmsList = new ObservableCollection<string>(AlgorithmFactory.Types);
            //Initialization of the algorithm name
            this.algorithmName = "";

            //Initialization of the color palette
            this.palettesList = new ObservableCollection<IPalette>();
            this.colorPalette = null;

            //Initialization of the size
            this.Size = 257;

            //Initialization of the seed
            this.Seed = "";

            //Initialization of the State
            this.State = AlgorithmState.UNKNOWN;

        }


        #region Reaction to the modification of the hmi

        /// <summary>
        /// Reset the image
        /// </summary>
        private void ResetImage()
        {
            //Creation of the model
            this.image = new PixelImage(size, size);

            //Registration as an observer of each pixel of the image
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    this.image.GetPixels(x, y).PropertyChanged += PixelUpdated;

            //Reload the image
            this.ReloadImage();
        }

        /// <summary>
        /// Notify the view of the change of all the pixels
        /// </summary>
        private void ReloadImage()
        {
            //Reload the image
            for (int x = 0; x < image.Width; x++)
                for (int y = 0; y < image.Height; y++)
                    if (image.GetPixels(x, y) != null) NotifyPropertyChanged("PIXEL/" + x.ToString() + "/" + y.ToString() + "/" + image.GetPixels(x, y).Nuance.ToString());
        }

        
        /// <summary>
        /// Change of the algorithm
        /// </summary>
        /// <param name="newAlgortihmName">New algorthme name</param>
        public void ChangeAlgorithm(String newAlgortihmName)
        {
            //Create the algorithm
            this.State = AlgorithmState.UNKNOWN;
            this.algorithmName = newAlgortihmName;
            this.algorithm = AlgorithmFactory.Create(newAlgortihmName);
            this.algorithm.PropertyChanged += Algorithm_PropertyChanged;
            //Change the size if needed by the algorithm
            if(this.algorithm.ClosestValideSize(size) != size)
            {
                this.Size = this.algorithm.ClosestValideSize(size);
                NotifyPropertyChanged("FORCEDSIZE");
            }
            //Reset the image
            this.ResetImage();
            //Charge the list of palette
            this.PalettesList = new ObservableCollection<IPalette>(algorithm.Palettes);
            this.algorithm.setImage(image);
        }

        public void ReloadAlgorithm()
        {
            //Create the algorithm
            this.algorithm = AlgorithmFactory.Create(this.algorithmName);
            this.algorithm.PropertyChanged += Algorithm_PropertyChanged;
            //Change the size if needed by the algorithm
            if (this.algorithm.ClosestValideSize(size) != size)
            {
                this.Size = this.algorithm.ClosestValideSize(size);
                NotifyPropertyChanged("FORCEDSIZE");
            }
            //Reset the image
            this.ResetImage();
            this.algorithm.setImage(image);
            if(this.colorPalette != null) this.State = this.algorithm.State;
        }

        /// <summary>
        /// Change of the palette
        /// </summary>
        /// <param name="newPalette">New palette</param>
        public void ChangePalette(IPalette newPalette)
        {
            //Change the color palette
            this.colorPalette = newPalette;
            //Reload the image
            this.ReloadImage();
            if (this.algorithm != null) this.State = this.algorithm.State;
        }

        /// <summary>
        /// Next step for the algorithm
        /// </summary>
        public void NextStepAlgorithm()
        {
            switch (this.state)
            {
                case AlgorithmState.NOTSTARTED:
                    if (this.algorithm != null)
                    {
                        this.algorithm.setSeed(this.seed);
                        this.algorithm.Start();
                    }
                    break;
                case AlgorithmState.RUNNING: if (this.algorithm != null) this.algorithm.NextStep(); break;
                case AlgorithmState.LOOP: 
                    if (this.algorithm != null)
                    {
                        if (this.algorithm.State == AlgorithmState.NOTSTARTED)
                        {
                            this.algorithm.setSeed(this.seed);
                            this.algorithm.Start();
                        }
                        else this.algorithm.NextStep();
                    }
                    break;
            }
        }

        /// <summary>
        /// Start a timed loop for the algorithm
        /// </summary>
        public void StartLoop()
        {
            this.State = AlgorithmState.LOOP;
            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromMilliseconds(10);
            this.timer.Tick += Timer_Tick;
            this.timer.Start();
        }

        /// <summary>
        /// Action at a tick of the timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (this.algorithm == null || this.State != AlgorithmState.LOOP)
            {
                this.timer.Stop();
            }
            else this.NextStepAlgorithm();
        }

        #endregion


        #region Reactions of the model modification
        /// <summary>
        /// The algorithm change its state
        /// </summary>
        /// <param name="sender">The algorithm</param>
        /// <param name="e">Event</param>
        /// <exception cref="NotImplementedException"></exception>
        private void Algorithm_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (this.State != AlgorithmState.LOOP || this.algorithm.State == AlgorithmState.FINISHED) this.State = this.algorithm.State;
        }

        /// <summary>
        /// Called when a pixel nuance is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">The propertyname contains "x/y"</param>
        /// <exception cref="NotImplementedException"></exception>
        private void PixelUpdated(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                //Transtype the sender as a pixel
                Pixel pixel = (Pixel) sender;
                //Notify the vue of the change to do
                NotifyPropertyChanged("PIXEL/"+pixel.X.ToString() + "/" + pixel.Y.ToString() + "/" + pixel.Nuance.ToString());

            }
            catch(Exception _)
            {
                MessageBox.Show("Observation link broken !","Internal error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Return the color (R,G,B) associated to the given nuance in the choosen palette
        /// </summary>
        /// <param name="nuance">The nuance</param>
        /// <returns>The associated color</returns>
        public Color GetColor(int nuance)
        {
            Color res = Colors.White;
            if(colorPalette != null) res = colorPalette.GetColor(nuance);
            return res;
        }
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
