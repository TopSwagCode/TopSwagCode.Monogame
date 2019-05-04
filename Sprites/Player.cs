using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Uranus.Models;

namespace Uranus.Sprites
{
    public class Player : Ship
    {
        private readonly Texture2D _shipTexture2DCenter;
        private readonly Texture2D _shipTexture2DLeft;
        private readonly Texture2D _shipTexture2DRight;
        private KeyboardState _currentKey;

        private KeyboardState _previousKey;

        private float _shootTimer = 0;

        public bool IsDead
        {
            get
            {
                return Health <= 0;
            }
        }

        public Input Input { get; set; }

        public Score Score { get; set; }

        public Player(Texture2D texture, Texture2D shipTexture2DLeft, Texture2D shipTexture2DRight)
          : base(texture)
        {
            _shipTexture2DCenter = texture;
            _shipTexture2DLeft = shipTexture2DLeft;
            _shipTexture2DRight = shipTexture2DRight;
            Speed = 3f;
        }

        public override void Update(GameTime gameTime)
        {
            if (IsDead)
                return;

            _previousKey = _currentKey;
            _currentKey = Keyboard.GetState();

            var velocity = Vector2.Zero;
            _rotation = 0;
            _texture = _shipTexture2DCenter;

            if (_currentKey.IsKeyDown(Input.Up))
            {
                velocity.Y = -Speed;
                _texture = _shipTexture2DLeft;
                //_rotation = MathHelper.ToRadians(-15);
            }
            else if (_currentKey.IsKeyDown(Input.Down))
            {
                velocity.Y += Speed;
                _texture = _shipTexture2DRight;
                //_rotation = MathHelper.ToRadians(15);
            }

            if (_currentKey.IsKeyDown(Input.Left))
            {
                velocity.X -= Speed;
            }
            else if (_currentKey.IsKeyDown(Input.Right))
            {
                velocity.X += Speed;
            }

            _shootTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_currentKey.IsKeyDown(Input.Shoot) && _shootTimer > 0.25f)
            {

                //Shoot(10f, -2f); Shoot left
                Shoot(10f);
                //Shoot(10f, 2f); shoot right

                _shootTimer = 0f;
            }

            Position += velocity;

            Position = Vector2.Clamp(Position, new Vector2(0, 0), new Vector2(Game1.ScreenWidth, Game1.ScreenHeight));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (IsDead)
                return;

            base.Draw(gameTime, spriteBatch);
        }

        public override void OnCollide(Sprite sprite, GameTime gameTime)
        {
            if (IsDead)
                return;

            if (sprite is Bullet && ((Bullet)sprite).Parent is Enemy)
                Health--;

            if (sprite is Enemy)
                Health -= 3;

            if (sprite is Astroid)
                Health--;
        }
    }
}
