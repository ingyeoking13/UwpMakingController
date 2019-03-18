using Microsoft.Graphics.Canvas;
using System.Numerics;
using UwpMakingController.util;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace UwpMakingController
{
    public sealed partial class MyUserControl1
    {
        class Attractor
        {
            Vector2 loc;
            static float G = 3.2f;
            float mass = 5.0f;
            public Attractor(Size controlSize)
            {
                loc = new Vector2(controlSize.ToVector2().X/2, controlSize.ToVector2().Y/2);
            }

            public void Update(Pointer pointer)
            {
                var position = Windows.UI.Core.CoreWindow.GetForCurrentThread().PointerPosition;
                if (position == null) return;

                loc.X = (float)(position.X-Window.Current.Bounds.X);
                loc.Y = (float)(position.Y-Window.Current.Bounds.Y);
            }

            public void Draw(CanvasDrawingSession ds)
            {
                ds.FillCircle(loc, 15.0f, Colors.Blue);
            }

            public Vector2 Attract(BouncingBall mover)
            {
                var vv = new Vector2(this.loc.X - mover.loc.X, this.loc.Y - mover.loc.Y);
                var dist = vv.Length();
                vv.limit(1);
                dist = Utils.constrain(dist, 10.0f, 40.0f);
                var force = (G * mover.mass * this.mass) / (dist * dist);
                vv *= force;
                return vv;
            }
        }
    }
}
