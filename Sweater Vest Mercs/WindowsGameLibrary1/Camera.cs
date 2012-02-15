using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SVMLib
{
    public class Camera
    {
        private const float MAX_WORLD_SCALE = .6f;
        private const float MIN_WORLD_SCALE = 2f;

        public static Vector2 Position;
        public static float Zoom;
        public static float Rotation;
        public static Vector2 Origin;
        private static bool UpdateMatrix;
        public static float CameraSpeed;

        public static float MaxZoom { get; set; }
        public static float MinZoom { get; set; }
        public static Rectangle Viewport { get; set; }
        public static Rectangle WorldRect { get; set; }

        public static Matrix Transform = Matrix.Identity;

        public static void Initialize( Rectangle view, Rectangle world )
        {
            CameraSpeed = 4f;

            Rotation = 0.0f;

            Position = new Vector2( 0, 0 );

            ZoomBy( 1f );

            Viewport = view;
            WorldRect = world;

            Origin = new Vector2( Viewport.Width / 2, Viewport.Height / 2 );

        }

        public static void ZoomBy(float amount)
        {
            Zoom += amount;

            Zoom = MathHelper.Clamp(Zoom, MaxZoom, MinZoom);

            UpdateMatrix = true;
        }

        public static void Move(Vector2 pos)
        {
            Position += pos;
            UpdateMatrix = true;

            //if (Position.X < Viewport.Left / Zoom)
            //    Position.X = Viewport.Left / Zoom;

            //if (Position.Y < Viewport.Top / Zoom)
            //    Position.Y = Viewport.Top / Zoom;

            //if ( ( WorldRect.Width - Viewport.Right / Zoom ) < Position.X )
            //    Position.X = WorldRect.Width - Viewport.Right / Zoom;

            //if ( ( WorldRect.Height - Viewport.Bottom / Zoom ) < Position.Y )
            //    Position.Y = WorldRect.Height - Viewport.Bottom / Zoom;
        }

        public static Matrix TransformMatrix()
        {
            if ( UpdateMatrix )
            {
                Transform = Matrix.CreateTranslation( new Vector3( -Position, 0 ) );
                            //Matrix.CreateRotationZ( Rotation ) *
                            //Matrix.CreateScale( new Vector3( Zoom, Zoom, 1 ) ) *
                            //Matrix.CreateTranslation( new Vector3( Origin, 0 ) );

                UpdateMatrix = false;
            }

            return Transform;
        }

        public static Vector3 ToWorldLocation( Vector3 position )
        {
            return Vector3.Transform( position, Matrix.Invert( Transform ) );
        }

        public static Vector3 ToScreenPosition( Vector3 position )
        {
            return Vector3.Transform( position, Transform );
        }

        //public static void Update()
        //{
        //    if (IsKeyDown(Keys.Left))
        //    {
        //        Position += new Vector2(-CameraSpeed, 0);
        //        UpdateMatrix = true;
        //    }

        //    if (IsKeyDown(Keys.Right))
        //    {
        //        Position += new Vector2(CameraSpeed, 0);
        //        UpdateMatrix = true;
        //    }

        //    if (IsKeyDown(Keys.Up))
        //    {
        //        Position += new Vector2(0, -CameraSpeed);
        //        UpdateMatrix = true;
        //    }

        //    if (IsKeyDown(Keys.Down))
        //    {
        //        Position += new Vector2(0, CameraSpeed);
        //        UpdateMatrix = true;
        //    }

        //    if (IsKeyDown(Keys.W))
        //        Zoom(0.03f);

        //    if (IsKeyDown(Keys.S))
        //        Zoom(-0.03f);

        //    if (IsKeyDown(Keys.D))
        //    {
        //        Rotation += MathHelper.ToRadions(1);
        //        UpdateMatrix = true;
        //    }

        //    if (IsKeyDown(Keys.A))
        //    {
        //        Rotation -= MathHelper.ToRadions(1);
        //        UpdateMatrix = true;
        //    }

        //    if (Position.X < Viewport.Left / Zoom)
        //        Position.X = Viewport.Left / Zoom;

        //    if (Position.Y < Viewport.Top / Zoom)
        //        Position.Y = Viewport.Top / Zoom;

        //    if (Position.X > WorldRect.Width – Viewport.Right / Zoom)
        //        Position.X = WorldRect.Width – Viewport.Right / Zoom;

        //    if (Position.Y > WorldRect.Height – Viewport.Bottom / Zoom)
        //        Position.Y = WorldRect.Height – Viewport.Bottom / Zoom;
        //}
    }
}
