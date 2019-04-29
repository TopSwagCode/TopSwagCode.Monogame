using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Uranus
{
    public abstract class Component
    {
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);
    }
}
