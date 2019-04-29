using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Uranus.States;

namespace Uranus.Sprites
{
    public class Enemy : Ship
    {
        private float _timer;

        private float HitResetTimer = 0.5f;
        public double ShootingTimer = 0;
        public double _hitTimer;

        public Enemy(Texture2D texture)
        : base(texture)
        {
            Speed = 2f;
        }

        public override void Update(GameTime gameTime)
        {


            if (gameTime.TotalGameTime.TotalMilliseconds >= ShootingTimer)
            {
                Shoot(-10f);
                ShootingTimer = gameTime.TotalGameTime.TotalMilliseconds + 1500;
            }

            if (_hitTimer < gameTime.TotalGameTime.TotalMilliseconds)
            {
                Colour = Color.White;
            }

            Position += new Vector2(-Speed, 0);

            // if the enemy is off the left side of the screen
            if (Position.X < -_texture.Width)
                IsRemoved = true;
        }

        public override void OnCollide(Sprite sprite, GameTime gameTime)
        {
            // If we crash into a player that is still alive
            if (sprite is Player && !((Player)sprite).IsDead)
            {
                ((Player)sprite).Score.Value++;

                // We want to remove the ship completely
                IsRemoved = true;

                // Shake screen after death
                GameState.ShakeScreen();
            }

            if (sprite is Astroid)
            {
                Health--;
                _hitTimer = gameTime.TotalGameTime.TotalMilliseconds + 100;

                Colour = Color.Gray;
                if (Health <= 0)
                {
                    IsRemoved = true;
                    // Shake screen after death
                    GameState.ShakeScreen();
                }
            }

            // If we hit a bullet that belongs to a player      
            if (sprite is Bullet && ((Bullet)sprite).Parent is Player)
            {
                Health--;
                _hitTimer = gameTime.TotalGameTime.TotalMilliseconds + 100;

                Colour = Color.Gray;
                if (Health <= 0)
                {
                    IsRemoved = true;
                    // Shake screen after death
                    GameState.ShakeScreen();
                    ((Player)sprite.Parent).Score.Value++;
                }
            }
        }
    }
}
