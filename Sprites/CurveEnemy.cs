using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Uranus.Sprites
{
    public class CurveEnemy : Enemy
    {
        private float _timer;
        private double testDouble;

        public CurveEnemy(Texture2D texture) : base(texture)
        {
            Speed = 2f;
            testDouble = 0;
        }

        public override void Update(GameTime gameTime)
        {
            testDouble += 0.10;
            if (gameTime.TotalGameTime.TotalMilliseconds >= ShootingTimer)
            {
                Shoot(-10f);
                ShootingTimer = gameTime.TotalGameTime.TotalMilliseconds + 1500;
            }

            if (_hitTimer < gameTime.TotalGameTime.TotalMilliseconds)
            {
                Colour = Color.White;
            }

            float yMaxSpeed = 1f;

            var test = Math.Sin(testDouble);

            Position += new Vector2(-Speed, (float)test * Speed);

            // if the enemy is off the left side of the screen
            if (Position.X < -_texture.Width)
                IsRemoved = true;
        }
    }
}
