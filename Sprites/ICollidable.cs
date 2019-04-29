using Microsoft.Xna.Framework;

namespace Uranus.Sprites
{
    public interface ICollidable
    {
        void OnCollide(Sprite sprite, GameTime gameTime);
    }
}
