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
using System.Windows.Threading;

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += Timer_Tick;

            Galaxy.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            Galaxy.Arrange(new Rect(0.0, 0.0, Galaxy.DesiredSize.Width, Galaxy.DesiredSize.Height));
           


        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
           
            for (int i = 0; i < numberOfCellsHeigh; i++)
            {
                for (int j = 0; j < numberOfCellsWidth; j++)
                {

                    Rectangle r = new Rectangle();
                    r.Width = Galaxy.ActualWidth / numberOfCellsWidth - 1.5;
                    r.Height = Galaxy.ActualHeight / numberOfCellsHeigh - 1.5;
                    r.Fill = Brushes.Cyan;
                    Galaxy.Children.Add(r);
                    Canvas.SetLeft(r, j * Galaxy.ActualWidth / numberOfCellsWidth);
                    Canvas.SetTop(r, i * Galaxy.ActualHeight / numberOfCellsHeigh);
                    r.MouseDown += R_MouseDown;

                    felder[i, j] = r;
                }
            }

        }

        const int numberOfCellsWidth = 60;
        const int numberOfCellsHeigh = 60;
        Rectangle[,] felder = new Rectangle[numberOfCellsWidth, numberOfCellsHeigh];
        DispatcherTimer timer = new DispatcherTimer();
       

       

        private void R_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((Rectangle)sender).Fill = (((Rectangle)sender).Fill == Brushes.Cyan) ? Brushes.Red : Brushes.Cyan;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int[,] numberOfNeighbors = new int[numberOfCellsHeigh, numberOfCellsWidth]; 
            for (int i = 0; i < numberOfCellsHeigh; i++)
            {
                for (int j = 0; j < numberOfCellsWidth; j++)
                {
                    int neighbors = 0;
                    int count = i - 1;
                    if (count < 0)
                    {
                        count = numberOfCellsHeigh - 1;
                    }
                    int including = i + 1;
                    if (including == numberOfCellsHeigh)
                    {
                        including = 0;
                    }
                    int jLeft = j - 1;
                    if (jLeft < 0)
                    {
                        jLeft = numberOfCellsWidth - 1;
                    }
                    int jRight = j + 1;
                    if (jRight >= numberOfCellsWidth)
                    {
                        jRight = 0;
                    }
                    if (felder[count, jLeft].Fill ==Brushes.Red)
                    {
                        neighbors++;
                    }
                    if (felder[count, j].Fill == Brushes.Red)
                    {
                        neighbors++;
                    }
                    if (felder[count, jRight].Fill == Brushes.Red)
                    {
                        neighbors++;
                    }
                    if (felder[i, jLeft].Fill == Brushes.Red)
                    {
                        neighbors++;
                    }
                    if (felder[i, jRight].Fill == Brushes.Red)
                    {
                        neighbors++;
                    }
                    if (felder[including, jLeft].Fill == Brushes.Red)
                    {
                        neighbors++;
                    }
                    if (felder[including, j].Fill == Brushes.Red)
                    {
                        neighbors++;
                    }
                    if (felder[including, jRight].Fill == Brushes.Red)
                    {
                        neighbors++;
                    }
                    numberOfNeighbors[i, j] = neighbors;
                }
            }
            for (int i = 0; i < numberOfCellsHeigh; i++)
            {
                for (int j = 0; j < numberOfCellsWidth; j++)
                {
                    if (numberOfNeighbors[i, j] < 2 || numberOfNeighbors[i, j] > 3)
                    {
                        felder[i, j].Fill = Brushes.Cyan;
                    }
                    else if (numberOfNeighbors[i, j] == 3)
                    {
                        felder[i, j].Fill = Brushes.Red;
                    }
                }
            }
        }

        bool run = false;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                run = false;
                StartAndStopToLive.Content = "StartToLive";
            }
            else
            {
                timer.Start();
                run = true;
                StartAndStopToLive.Content = "StopToLive";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            {
                for (int i = 0; i < numberOfCellsHeigh; i++)
                {
                    for (int j = 0; j < numberOfCellsWidth; j++)
                    {

                        Random dice = new Random();
                        felder[i, j].Fill = (dice.Next(0, 2) == 1) ? Brushes.Cyan : Brushes.Red;

                        
                    }
                }
            }
        }
    }
}
