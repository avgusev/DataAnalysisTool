using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Forms.Integration;
using Microsoft.Win32;
using System.IO;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Data;
using OxyPlot;

namespace DataAnalysisTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OxyPlotViewModel oxyPlot = new OxyPlotViewModel();
        private MainWindowViewModel mainView = new MainWindowViewModel();

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new
            {
                oxyPlot,
                mainView
            };
            oxyPlot.mainView = mainView;
            this.mainView.ParameterChanged += new EventHandler(UpdateNudParameters);
        }
        #endregion

        #region Window Size
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var plotView = (OxyPlot.Wpf.PlotView)this.FindName("CscanView");

            plotView.LayoutUpdated += OnLayoutUpdated;
        }

        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            var plotView = (OxyPlot.Wpf.PlotView)this.FindName("CscanView");

            if (plotView.Model != null)
            {
                if (plotView.Model.Axes != null && plotView.Model.Axes.Count != 0)
                {
                    var widthAdjustment = plotView.Model.PlotArea.Width - plotView.Model.PlotArea.Height;

                    plotView.Width = plotView.ActualWidth - widthAdjustment;
                }
            }
        }
        #region UNHANDLED
        private bool isDataLoaded = false;
        private void btnLoadData_Click(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = "c:\\";
            ofd.Filter = "Test File(*.txt)|*.txt";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog().Value)
            {
                path1 = System.IO.Path.GetDirectoryName(ofd.FileName);//File directory
                path2 = ofd.FileName;//File full path
                path3 = ofd.SafeFileName;//File name
                this.ReadCSVData(path2);
                //this.AddWearSurfacesUIs();
                //this.oxyPlot.AddWearSurface();
            }
            
            oxyPlot.UpdateView();
        }

        private void btnSaveData_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOthers_Click(object sender, RoutedEventArgs e)
        {

        }

        private bool isWithinCircleCursor(double centerX, double centerY, double mouseX, double mouseY, double radius)
        {
            double diffX = centerX - mouseX;
            double diffY = centerY - mouseY;
            return (diffX * diffX + diffY * diffY) <= radius * radius;
        }

        private double WindowHeight = 0;
        private double WindowWidth = 0;

        private void CscanView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            OxyPlot.Wpf.PlotView p = (sender as OxyPlot.Wpf.PlotView);

            if (isDataLoaded)
            {

                double windowRatio = p.Model.PlotArea.Width / p.Model.PlotArea.Height;
                WindowWidth = 100;
                WindowHeight = 100 / windowRatio;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.IsKeyDown(Key.LeftShift) | (Keyboard.IsKeyDown(Key.RightShift))))
            {

            }
        }

        private void OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if ((Keyboard.IsKeyUp(Key.LeftShift) | (Keyboard.IsKeyUp(Key.RightShift))))
            {

            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        #endregion UNHANDLED

        #endregion WindowSize

        #region Wear Surfaces
        string path1 = string.Empty;
        string path2 = string.Empty;
        string path3 = string.Empty;
        private int WearSurfaceIndex = 0;



        StackPanel stackpanel;
        CheckBox chkBox;
        Button removeButton;
        TextBlock txbWearFacePath;
        private void AddWearSurfacesUIs()
        {
            stackpanel = new StackPanel();
            stackpanel.Orientation = Orientation.Horizontal;
            stackpanel.Background = Brushes.Green;

            chkBox = new CheckBox();
          //  chkBox.VerticalAlignment = VerticalAlignment.Center;
          //  chkBox.HorizontalAlignment = HorizontalAlignment.Center;
            chkBox.IsChecked = true;
            chkBox.Checked += ChkBox_Checked;
            chkBox.Unchecked += ChkBox_Unchecked;

            removeButton = new Button();
            Image img = new Image();
            string relativePath = @"Image/delete.png";
            img.Source = new BitmapImage(new Uri(relativePath, UriKind.Relative));
            StackPanel stackPnl = new StackPanel();
            stackPnl.Orientation = Orientation.Vertical;
            stackPnl.Children.Add(img);
            removeButton.Content = stackPnl;
            removeButton.Width = 14;
            removeButton.Height = 14;
            removeButton.Click += RemoveButton_Click;
            
            txbWearFacePath = new TextBlock();
            txbWearFacePath.Text = path3;
            txbWearFacePath.ToolTip = path3;

            stackpanel.Children.Add(removeButton);
            stackpanel.Children.Add(chkBox);
            stackpanel.Children.Add(txbWearFacePath);
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ChkBox_Unchecked(object sender, RoutedEventArgs e)
        {
        }

        private void ChkBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ReadCSVData(string fileName)
        {
            string path = fileName;
            List<DataPoint> listA = new List<DataPoint>();
      

            try
            {
                using (var reader = new StreamReader(path))
                {
                    Point p = new Point();
                    int size = 2;
                    int X = System.IO.File.ReadAllLines(path).Length;

                    int Y = reader.ReadLine().Split('\t').Length - size;
                    
                    // generate 2d normal distribution
                    oxyPlot.CScanRawData = new double[100, 100];

                    oxyPlot.Original_data = new double[100, 100, 100];
                    Random random = new Random();

                    int x = 0;
                    while (!reader.EndOfStream)
                    {

                        string[] values = reader.ReadLine().Split('\t');

                        if (x >= 100) break;

                        for (int y = 0; y < Y; ++y)
                        {
                            if (y >= 100) break;
                            // data[x, y] = singleData[x] * singleData[(y) % 100] * 100;
                            for (int z = 0; z < X; ++z)
                            {
                                if (z >= 100) break;
                                oxyPlot.Original_data[x, y, z] =  Convert.ToDouble(values[y].Replace('.', ',')); //singleData[x] * singleData[(y) % 100] * 100;
                            }
                        }

                        x++;
                    }
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("ReadCSVData()");
                return;
            }

            


        }

        #endregion Wear Surfaces

        #region Event


        #region NUDEvent

        private void OnNudTextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty((sender as System.Windows.Forms.Control).Text))
                (sender as System.Windows.Forms.Control).Text = (sender as System.Windows.Forms.NumericUpDown).Value.ToString();
        }

        private void OnCircleCursorCenterXChanged(object sender, EventArgs e)
        {
            this.oxyPlot.UpdateCursors();
            this.oxyPlot.UpdateData();
            this.oxyPlot.UpdatePlot();
        }

        private void OnCircleCursorCenterYChanged(object sender, EventArgs e)
        {
            this.oxyPlot.UpdateCursors();
            this.oxyPlot.UpdateData();
            this.oxyPlot.UpdatePlot();
        }

        private void OnCircleCursorRadiusChanged(object sender, EventArgs e)
        {
            if (!this.oxyPlot.isMouseAction)
            {
                this.oxyPlot.UpdateCursors();
                this.oxyPlot.UpdateAxis();
                this.oxyPlot.UpdateData();
                this.oxyPlot.UpdatePlot();
            }
        }

        private void OnCircleCursorAngleChanged(object sender, EventArgs e)
        {

        }

        private void OnGate1ValueChanged(object sender, EventArgs e)
        {
            this.oxyPlot.UpdateCursors();
            this.oxyPlot.UpdateData();
            this.oxyPlot.UpdatePlot();
            this.mainView.GateDataRange = Math.Max(
                Math.Abs(this.mainView.Gate2Position - this.mainView.Gate1Position), 0.00).ToString("f2");
        }

        private void OnGate2ValueChanged(object sender, EventArgs e)
        {
            this.oxyPlot.UpdateCursors();
            this.oxyPlot.UpdateData();
            this.oxyPlot.UpdatePlot();
            this.mainView.GateDataRange = Math.Max(
                Math.Abs(this.mainView.Gate2Position - this.mainView.Gate1Position), 0.00).ToString("f2");
        }

        #endregion NUDEvent

        #region TextBoxEvent
        #endregion TextBoxEvent


        #region Other Events
        public void UpdateNudParameters(object sender, EventArgs e)
        {
        }
        #endregion

        #endregion Event

    }
}
