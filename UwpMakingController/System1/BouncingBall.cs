using Microsoft.Graphics.Canvas;
using System;
using System.Numerics;
using UwpMakingController.util;
using Windows.Foundation;
using Windows.UI;

namespace UwpMakingController
{
    public sealed partial class MyUserControl1
    {
        class BouncingBall
        {
            Color color;
            public float mass;
            static int id = 0; 

            Vector2 Velocity { get; set; }
            Vector2 Acceleration { get; set; }
            public Vector2 loc { get; private set; }
            public float Radius { get; private set; }

            public BouncingBall(Color color)
            {
                this.color = color;

                Velocity = new Vector2(Utils.RandomBetween(-2, 2), Utils.RandomBetween(-2, 2));
                Radius = Utils.RandomBetween(10, 30);
                mass = Radius;
                loc = new Vector2((float)id*20, (float)id*20);
                id++;
            }

            public void Update(Size controlSize)
            {
                // Move the ball.

                loc += Velocity;

                // Bounce if we hit the edge.
                Vector2 topLeft = new Vector2(Radius);
                Vector2 bottomRight = controlSize.ToVector2() - new Vector2(Radius);
                Vector2 limited_Position = this.loc.limit(256);

                this.color = Color.FromArgb(200, (byte)limited_Position.X, (byte)limited_Position.Y, 0);
                float bounceX = (loc.X < topLeft.X || loc.X > bottomRight.X) ? -1 : 1;
                float bounceY = (loc.Y < topLeft.Y || loc.Y > bottomRight.Y) ? -1 : 1;

                Velocity *= new Vector2(bounceX, bounceY);
                Velocity += Acceleration;
                Velocity = Velocity.limit(18);
                Acceleration *= 0;
                loc = Vector2.Clamp(loc, topLeft, bottomRight);
            }

            public void Draw(CanvasDrawingSession ds, float alpha = 1)
            {
                ds.FillCircle(loc, Radius, color);
            }
            public void Force(Vector2 force)
            {
                this.Acceleration += (force/this.mass);
            }

        }
    }
}
