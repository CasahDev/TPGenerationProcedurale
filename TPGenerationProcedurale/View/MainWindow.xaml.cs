using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TPGenerationProcedurale.Model.Palettes;
using TPGenerationProcedurale.Model.Palettes.Realisations;
using TPGenerationProcedurale.ViewModel;

namespace TPGenerationProcedurale.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private WriteableBitmap writeableBitmap;
        private ViewModel.ViewModel viewModel;

        public MainWindow()
        {
            //View model initialisation
            this.viewModel = new ViewModel.ViewModel();
            this.DataContext = this.viewModel;
            this.viewModel.PropertyChanged += ViewModel_PropertyChanged;

            InitializeComponent();

            int size = this.viewModel.Size;
            //Image initialisation
            this.writeableBitmap = new WriteableBitmap((int)size, (int)size, 96, 96, PixelFormats.Bgr32, null);
            this.Image.Source = writeableBitmap;
        }


        /// <summary>
        /// Change the color of the pixel
        /// </summary>
        /// <param name="x">Column of the pixel</param>
        /// <param name="y">Row of the pixel</param>
        /// <param name="nuance">Nuance in the choosen color palette</param>
        public void ChangeColorPixel(int x,int y,int nuance)
        {
            try
            {
                //Get the color
                Color color = this.viewModel.GetColor(nuance);
                // Reserve the back buffer for updates.
                writeableBitmap.Lock();

                unsafe
                {
                    // Get a pointer to the back buffer.
                    IntPtr pBackBuffer = writeableBitmap.BackBuffer;

                    // Find the address of the pixel to draw.
                    pBackBuffer += y * writeableBitmap.BackBufferStride;
                    pBackBuffer += x * 4;

                    // Compute the pixel's color.
                    int color_data = color.R << 16; // R
                    color_data |= color.G << 8;   // G
                    color_data |= color.B << 0;   // B

                    // Assign the color data to the pixel.
                    *((int*)pBackBuffer) = color_data;
                }

                // Specify the area of the bitmap that changed.
                writeableBitmap.AddDirtyRect(new Int32Rect(x, y, 1, 1));
            }
            finally
            {
                // Release the back buffer and make it available for display.
                writeableBitmap.Unlock();
            }
        }



        #region Observation pattern

        private void ViewModel_PixelChanged(string message)
        {
            var pieces = message.Split("/");
            ChangeColorPixel(Int32.Parse(pieces[1]), Int32.Parse(pieces[2]), Int32.Parse(pieces[3]));
        }

        /// <summary>
        /// Called when the size is changed
        /// </summary>
        private void ViewModel_SizeChanged()
        {
            this.writeableBitmap = new WriteableBitmap((int)this.viewModel.Size, (int)this.viewModel.Size, 96, 96, PixelFormats.Bgr32, null);
            this.Image.Source = writeableBitmap;
        }

        /// <summary>
        /// Called when the view model force the change of the size
        /// </summary>
        private void ViewModel_ForcedSizeChanged()
        {
            MessageBox.Show("The size of the image has been changed according to the algorithm rules.","Warning",MessageBoxButton.OK,MessageBoxImage.Warning);
        }

        private void ViewModel_StateChanged()
        {
            switch (this.viewModel.State)
            {
                case Model.Algorithms.AlgorithmState.UNKNOWN:
                    ButtonGO.Content = ""; ButtonGO.IsEnabled = false;
                    ButtonCLOCK.Content = ""; ButtonCLOCK.IsEnabled = false;
                    break;
                case Model.Algorithms.AlgorithmState.NOTSTARTED: 
                    ButtonGO.Content = "Start"; ButtonGO.IsEnabled = true;
                    ButtonCLOCK.Content = "Loop"; ButtonCLOCK.IsEnabled = true;
                    break;
                case Model.Algorithms.AlgorithmState.RUNNING: 
                    ButtonGO.Content = "Next Step"; ButtonGO.IsEnabled = true; break;
                    ButtonCLOCK.Content = "Loop"; ButtonCLOCK.IsEnabled = true;
                case Model.Algorithms.AlgorithmState.FINISHED: 
                    ButtonGO.Content = "Finished"; ButtonGO.IsEnabled = false;
                    ButtonCLOCK.Content = "Finished"; ButtonCLOCK.IsEnabled = false;
                    break;
                case Model.Algorithms.AlgorithmState.LOOP:
                    ButtonGO.Content = "In Progress"; ButtonGO.IsEnabled = false;
                    ButtonCLOCK.Content = "In Progress"; ButtonCLOCK.IsEnabled = false;
                    break;
            }
        }

        /// <summary>
        /// Called when the view model is changed
        /// </summary>
        /// <param name="sender">The viewModel</param>
        /// <param name="e">the property name contains "X/Y/Nuance"</param>
        private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                if (e.PropertyName.StartsWith("PIXEL")) ViewModel_PixelChanged(e.PropertyName);
                if (e.PropertyName.StartsWith("FORCEDSIZE")) ViewModel_ForcedSizeChanged();
                if (e.PropertyName.StartsWith("State")) ViewModel_StateChanged();
                if (e.PropertyName.StartsWith("Size")) ViewModel_SizeChanged();
            }
            catch (Exception _)
            {
                MessageBox.Show("Observation link broken !", "Internal error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        /// <summary>
        /// Change of the algorithm
        /// </summary>
        /// <param name="sender">The combobox</param>
        /// <param name="e">Selection event</param>
        private void ComboAlgorithm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.viewModel.ChangeAlgorithm((String)this.ComboAlgorithm.SelectedItem);
        }

        /// <summary>
        /// Change of the color palette
        /// </summary>
        /// <param name="sender">The combobox</param>
        /// <param name="e">Selection event</param>
        private void ComboPalette_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.viewModel.ChangePalette((IPalette)this.ComboPalette.SelectedItem);
        }

        /// <summary>
        /// The user press the GO button
        /// </summary>
        /// <param name="sender">The button</param>
        /// <param name="e">The event</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.NextStepAlgorithm();
        }

        /// <summary>
        /// The user press the LOOP button
        /// </summary>
        /// <param name="sender">The button</param>
        /// <param name="e">The event</param>
        private void ButtonCLOCK_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.StartLoop();
        }
    }


}
