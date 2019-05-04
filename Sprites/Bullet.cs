using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Uranus.Sprites
{
    public class Bullet : Sprite, ICollidable
    {
        private float _timer;

        public Explosion Explosion;

        public float LifeSpan { get; set; }

        public Vector2 Velocity { get; set; }

        public Bullet(Texture2D texture)
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
        }

        public void OnCollide(Sprite sprite, GameTime gameTime)
        {
            if (sprite is Bullet)
                return;

            if (sprite is Enemy && this.Parent is Enemy)
                return;

            if (sprite is Player && this.Parent is Player)
                return;

            if (sprite is Player && ((Player)sprite).IsDead)
                return;

            if (sprite is Astroid)
            {
                IsRemoved = true;
                AddExplosion();
            }

            if (sprite is Enemy && this.Parent is Player)
            {
                IsRemoved = true;
                AddExplosion();
            }

            if (sprite is Player && this.Parent is Enemy)
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
