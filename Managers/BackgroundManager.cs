using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Uranus.Managers
{
    public class BackgroundManager
    {
        private float _timer;

        private Texture2D _layer1;
        private Texture2D _layer2;
        private int _layer1Velocity;
        private int _layer2Velocity;
        private int _layer1CurrentX;
        private int _layer2CurrentX;

        public BackgroundManager(ContentManager content)
        {

            _layer1 = content.Load<Texture2D>("Background/NebulaBlue");
            _layer2 = content.Load<Texture2D>("Background/StarsSmall1");

            _layer1Velocity = 1;
            _layer2Velocity = 2;

            _layer1CurrentX = 0;
            _layer2CurrentX = 0;
        }

        public void Update(GameTime gameTime)
        {
            // Change direction and * -1 to transform to other way. This is looking stupid
            _layer1CurrentX -= (int)_layer1Velocity;
            _layer1CurrentX %= _layer1.Width;
            _layer2CurrentX -= (int)_layer2Velocity;
            _layer2CurrentX %= _layer2.Width;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_layer1, new Vector2(_layer1CurrentX, 0), Color.White);
            spriteBatch.Draw(_layer1, new Vector2(_layer1CurrentX + _layer1.Width, 0), Color.White);
            spriteBatch.Draw(_layer2, new Vector2(_layer2CurrentX, 0), Color.White);
            spriteBatch.Draw(_layer2, new Vector2(_layer2CurrentX + _layer2.Width, 0), Color.White);

        }
    }
}
