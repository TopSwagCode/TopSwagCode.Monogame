using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Uranus.Sprites
{
    public class Ship : Sprite, ICollidable
    {
        public int Health { get; set; }

        public Bullet Bullet { get; set; }

        public float Speed;

        public Ship(Texture2D texture) : base(texture)
        {
        }

        protected void Shoot(float speed, float angle = 0f)
        {
            var bullet = Bullet.Clone() as Bullet;
            bullet.Position = this.Position;
            bullet.Colour = Color.White;
            bullet.Layer = 0.1f;
            bullet.LifeSpan = 5f;
            bullet.Velocity = new Vector2(speed, angle);
            bullet.Parent = this;

            Children.Add(bullet);
        }

        public virtual void OnCollide(Sprite sprite, GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
