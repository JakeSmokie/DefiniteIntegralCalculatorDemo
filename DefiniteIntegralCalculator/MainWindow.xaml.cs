using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

// 1. 12.61162414650274
// 2. 12.61161685147804
// 3. 12.61161688403468
// 4. 
// Reference: 12.61161680006573

namespace DefiniteIntegralCalculator
{
    public partial class MainWindow : Window
    {
        // render parameters
        private const double renderDeltaX = 0.05;
        private const double xScale = 150;
        private const double yScale = 75;
        private const double markWidth = 5;
        private const int marksCount = 20;

        // integral parameters
        // a
        private const double xIntegralStart = 0.5 * Math.PI;
        // b
        private const double xIntegralEnd = 1.5 * Math.PI;
        // dXi
        private const double xIntegralDelta = (xIntegralEnd - xIntegralStart) / 1000;
        
        // Function to compute integral 
        private delegate double MathFunction(double x);
        private MathFunction function = (x) => Math.Sqrt(1 + 36 * Math.Pow(Math.Sin(2 * x), 2));

        public MainWindow()
        {
            InitializeComponent();
            DrawAxis();

            DrawFunction(function);
            DrawAndCalculateIntegral(function, xIntegralStart, xIntegralEnd);
        }

        private void DrawAndCalculateIntegral(MathFunction function, double xStart, double xEnd)
        {
            // integral value
            double sum = 0;

            for (double x = xIntegralStart; x < xIntegralEnd; x += xIntegralDelta)
            {
                // f(e)
                var y = function(x);
                sum += y * xIntegralDelta;

                var rectGeometry = new RectangleGeometry(
                    new Rect(
                        Width / 2 + (x - xIntegralDelta) * xScale,
                        Height / 2 - y * yScale,
                        xIntegralDelta * xScale,
                        y * yScale
                        )
                    );

                var path = new Path()
                {
                    Data = rectGeometry,
                    Fill = Brushes.WhiteSmoke,
                    Opacity = 1
                };


                grid.Children.Add(path);
            }

            var label = new Label()
            {
                Content = $"Integral value: {sum}",
                Foreground = Brushes.White,
                FontSize = 20,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness()
                {
                    Left = Width / 4,
                    Top = Width / 2,
                    Right = 0,
                    Bottom = 0,
                },
            };

            grid.Children.Add(label);
        }

        private void DrawFunction(MathFunction function)
        {
            for (double x = -Width / 2; x <= Width / 2; x += renderDeltaX)
            {
                var y = function(x);
                var nextY = function(x + renderDeltaX);

                var line = new Line()
                {
                    X1 = Width / 2 + x * xScale,
                    X2 = Width / 2 + (x + renderDeltaX) * xScale,
                    Y1 = Height / 2 - y * yScale,
                    Y2 = Height / 2 - nextY * yScale,
                    Stroke = Brushes.AntiqueWhite
                };

                grid.Children.Add(line);
            }
        }

        private void DrawAxis()
        {
            var xAxis = new Line()
            {
                X1 = 0,
                X2 = Width,
                Y1 = Height / 2,
                Y2 = Height / 2,
                Stroke = Brushes.LimeGreen
            };

            var yAxis = new Line()
            {
                X1 = Width / 2,
                X2 = Width / 2,
                Y1 = 0,
                Y2 = Height,
                Stroke = Brushes.LimeGreen
            };

            grid.Background = Brushes.Black;
            grid.Children.Add(xAxis);
            grid.Children.Add(yAxis);

            for (int mark = -marksCount / 2; mark < marksCount / 2; mark++)
            {
                var xLine = new Line()
                {
                    X1 = Width / 2 - xScale * mark,
                    X2 = Width / 2 - xScale * mark,
                    Y1 = Height / 2 + markWidth,
                    Y2 = Height / 2 - markWidth,
                    Stroke = Brushes.MintCream
                };

                var yLine = new Line()
                {
                    X1 = Width / 2 + markWidth,
                    X2 = Width / 2 - markWidth,
                    Y1 = Height / 2 - yScale * mark,
                    Y2 = Height / 2 - yScale * mark,
                    Stroke = Brushes.MintCream
                };

                grid.Children.Add(xLine);
                grid.Children.Add(yLine);
            }
        }
    }
}
