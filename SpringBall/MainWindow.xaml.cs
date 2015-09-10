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

namespace SpringBall
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        double vx = 140.0;
        double vy = 210.0;
        int pleft = 0, pright = 0;
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(0.05);
            timer.Tick += animate;
            timer.IsEnabled = true;
            this.Title = pleft.ToString() + " : " + pright.ToString();

        }

        private void animate(object sender, EventArgs e)
        {
            Double x = Canvas.GetLeft(ball);
            Double y = Canvas.GetTop(ball);
            x += vx * timer.Interval.TotalSeconds;
            // linker Schläger
            if (x <= leftPaddle.Width && y >= Canvas.GetTop(leftPaddle) && y + ball.Height <= Canvas.GetTop(leftPaddle) + leftPaddle.Height)
            {
                vx  = - vx;
                
            }
            // rechter Schläger
            if (x + ball.Width  >= myCanvas.ActualWidth - rightPaddle.Width   && y >= Canvas.GetTop(rightPaddle) && y + ball.Height <= Canvas.GetTop(rightPaddle) + rightPaddle.Height)
            {
                vx = -vx;

            }
            Canvas.SetLeft(ball, x);
            // Beinnpunkte
            if (x <= 0)
            {
                pright += 1;
                this.Title = pleft.ToString() + " : " + pright.ToString();
                Canvas.SetLeft(ball, myCanvas.ActualWidth / 2.0);
            }
            if (x + ball.Width >= myCanvas.ActualWidth )
            {
                pleft += 1;
                this.Title = pleft.ToString() + " : " + pright.ToString();
                Canvas.SetLeft(ball, myCanvas.ActualWidth / 2.0);
            }

            //End
            y += vy * timer.Interval.TotalSeconds;
            if(y <= 0.0  || y >= myCanvas.ActualHeight  - ball.Height)
            {
                vy = -vy;
            }
            Canvas.SetTop(ball, y);

        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(this);
            //this.Title = p.X.ToString();
            Canvas.SetTop(leftPaddle, p.Y);
            Canvas.SetTop(rightPaddle, p.Y);
        }
    }
}
