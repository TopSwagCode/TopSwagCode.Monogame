using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Uranus.Sprites
{
    public class Astroid : Sprite, ICollidable
    {
        private float _timer;

        public Explosion Explosion;
        private double _timeToRotate;

        public float LifeSpan { get; set; }

        public Vector2 Velocity { get; set; }

        public Astroid(Texture2D texture)
          : base(texture)
        {

        }

        public override void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer >= LifeSpan)
                IsRemoved = true;

            if (this.Position.X > Game1.ScreenWidth + _texture.Width + 20 || this.Position.X < 0 - _texture.Width - 20)
                IsRemoved = true;

            Position += Velocity;
            if (gameTime.TotalGameTime.TotalMilliseconds > _timeToRotate)
            {
                Rotation+=0.25f;
                _timeToRotate = gameTime.TotalGameTime.TotalMilliseconds + 45;
                // Add random rotation direction and random speed.
            }

        }

        public void OnCollide(Sprite sprite, GameTime gameTime)
        {
            // Can't hit a player if they're dead
            if (sprite is Player && ((Player)sprite).IsDead)
                return;

            if (sprite is Enemy)
            {
                IsRemoved = true;
                AddExplosion();
            }

            if (sprite is Player)
            {
                IsRemoved = true;
                AddExplosion();
            }

            if (sprite is Bullet)
            {
                IsRemoved = true;
                AddExplosion();
            }
        }

        private void AddExplosion()
        {
            if (Explosion == null)
                return;

            var explosion = Explosion.Clone() as Explosion;
            explosion.Position = this.Position;

            Children.Add(explosion);
        }
    }
}
