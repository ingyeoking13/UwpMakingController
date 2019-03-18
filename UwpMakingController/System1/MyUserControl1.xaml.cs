using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System.Collections.Generic;
using UwpMakingController.util;
using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace UwpMakingController
{
    public sealed partial class MyUserControl1 : UserControl
    {
        BouncingBall[] bouncingBalls = new BouncingBall[]
        {
            new BouncingBall(Colors.Red), new BouncingBall(Colors.Lime),
            new BouncingBall(Colors.Blue),
            new BouncingBall(Colors.Yellow), new BouncingBall(Colors.Cyan),
            new BouncingBall(Colors.Magenta), new BouncingBall(Colors.LightGray),
            new BouncingBall(Colors.DarkGray), new BouncingBall(Colors.Brown),
        };
        Attractor attractor;

        static CanvasTextFormat textFormat = new CanvasTextFormat
        {
            HorizontalAlignment = CanvasHorizontalAlignment.Center,
            VerticalAlignment = CanvasVerticalAlignment.Center,
        };

        CanvasRadialGradientBrush gradientBrush;

        public MyUserControl1()
        {
            this.InitializeComponent();
        }

        void CreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            attractor = new Attractor(canvas.Size);
            // Create a gradient brush, used to control layer opacity.
            var gradientStops = new CanvasGradientStop[]
            {
                new CanvasGradientStop { Position = 0,     Color = Colors.White       },
                new CanvasGradientStop { Position = 0.25f, Color = Colors.White       },
                new CanvasGradientStop { Position = 1,     Color = Colors.Transparent },
            };

            gradientBrush = new CanvasRadialGradientBrush(sender, gradientStops);
        }

        void Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            foreach (var ball in bouncingBalls)
            {
                var vv = attractor.Attract(ball);
                ball.Force(vv);
                ball.Update(sender.Size);
            }
        }

        void Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            var ds = args.DrawingSession;

            attractor.Draw(ds);
            foreach (var ball in bouncingBalls)
            {
                ball.Draw(ds);
            }
        }

        private void control_Unloaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // Explicitly remove references to allow the Win2D controls to get garbage collected
            canvas.RemoveFromVisualTree();
            canvas = null;
        }

        private void Canvas_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            attractor.Update(e.Pointer);
        }
    }
}
