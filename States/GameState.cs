using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Uranus.Managers;
using Uranus.Sprites;

namespace Uranus.States
{
    public class GameState : State
    {
        private EnemyManager _enemyManager;
        private PlayerManger _playerManger;

        private SpriteFont _font;

        private List<Player> _players;

        private ScoreManager _scoreManager;

        private List<Sprite> _sprites;

        public int PlayerCount;
        private BackgroundManager _backgroundManager;
        private MusicManager _musicManager;
        private BulletManager _bulletManager;

        public GameState(Game1 game, ContentManager content)
        : base(game, content)
        {
        }

        public override void LoadContent()
        {

            var bulletTexture = _content.Load<Texture2D>("Bullet");

            _font = _content.Load<SpriteFont>("Font");

            _scoreManager = ScoreManager.Load();

            _sprites = new List<Sprite>();

            _bulletManager = new BulletManager(_content);

            _playerManger = new PlayerManger(_content, _bulletManager);
            _astroidManager = new AstroidManager(_content);

            if (PlayerCount >= 1)
            {
                _sprites.Add(_playerManger.GetPlayer(PlayerColour.Red, PlayerControls.Wasd, "Player 1"));
            }

            if (PlayerCount >= 2)
            {
                _sprites.Add(_playerManger.GetPlayer(PlayerColour.Green, PlayerControls.Arrow, "Player 2"));
            }

            _players = _sprites.Where(c => c is Player).Select(c => (Player)c).ToList();

            _enemyManager = new EnemyManager(_content)
            {
                Bullet = _bulletManager.GetBullet(BulletType.Proton),
            };

            _backgroundManager = new BackgroundManager(_content);
            _musicManager = new MusicManager(_content);
            _musicManager.PlayGameMusic();
        }

        public override void Update(GameTime gameTime)
        {
            _backgroundManager.Update(gameTime);
            _musicManager.UpdateGame(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))

            {
                _musicManager.Stop();
                _game.ChangeState(new MenuState(_game, _content));

            }

            foreach (var sprite in _sprites)
                sprite.Update(gameTime);

            _enemyManager.Update(gameTime);
            if (_enemyManager.CanAdd && _sprites.Where(c => c is Enemy).Count() < _enemyManager.MaxEnemies)
            {
                _sprites.Add(_enemyManager.GetEnemy());
            }


            _astroidManager.Update(gameTime);
            if (_astroidManager.CanAdd && _sprites.Where(c => c is Astroid).Count() < _astroidManager.MaxAstroids)
            {
                _sprites.Add(_astroidManager.GetAstroid());
            }


        }

        public override void PostUpdate(GameTime gameTime)
        {
            var collidableSprites = _sprites.Where(c => c is ICollidable);

            foreach (var spriteA in collidableSprites)
            {
                foreach (var spriteB in collidableSprites)
                {
                    // Don't do anything if they're the same sprite!
                    if (spriteA == spriteB)
                        continue;

                    if (spriteA is Bullet && spriteB is Bullet)
                        continue;

                    if (spriteA is Bullet && spriteB is Player && ((Bullet)spriteA).Parent is Player)
                        continue;

                    if (spriteB is Bullet && spriteA is Player && ((Bullet)spriteB).Parent is Player)
                        continue;

                    if (spriteA is Bullet && spriteB is Enemy && ((Bullet)spriteA).Parent is Enemy)
                        continue;

                    if (spriteB is Bullet && spriteA is Enemy && ((Bullet)spriteB).Parent is Enemy)
                        continue;

                    if (!spriteA.CollisionArea.Intersects(spriteB.CollisionArea))
                        continue;

                    if (spriteA.Intersects(spriteB))
                        ((ICollidable)spriteA).OnCollide(spriteB, gameTime);
                }
            }

            // Add the children sprites to the list of sprites (ie bullets)
            int spriteCount = _sprites.Count;
            for (int i = 0; i < spriteCount; i++)
            {
                var sprite = _sprites[i];
                foreach (var child in sprite.Children)
                    _sprites.Add(child);

                sprite.Children = new List<Sprite>();
            }

            for (int i = 0; i < _sprites.Count; i++)
            {
                if (_sprites[i].IsRemoved)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                shakeRadius = 5f;
                shakeScreen = true;
                shakeStartAngle = 15;
            }

            if (shakeScreen && shakeCount < maxShakes)
            {
                //shakeOffset *= -1;
                //float shakeRadius = 10f;

                shakeOffset = new Vector2((float)(Math.Sin(shakeStartAngle) * shakeRadius), (float)(Math.Cos(shakeStartAngle) * shakeRadius)); ;
                shakeRadius -= 0.25f;
                shakeStartAngle += (150 + Game1.Random.Next(60));
                shakeCount++;
            }
            else
            {
                shakeCount = 0;
                shakeScreen = false;
            }

            // If all the players are dead, we save the scores, and return to the highscore state
            if (_players.All(c => c.IsDead))
            {
                foreach (var player in _players)
                    _scoreManager.Add(player.Score);

                ScoreManager.Save(_scoreManager);
                _musicManager.Stop();
                _game.ChangeState(new HighscoresState(_game, _content));
            }
        }

        public static void ShakeScreen()
        {
            shakeRadius = 10f;
            shakeScreen = true;
            shakeStartAngle = 15;
            shakeCount = 0;
        }

        //
        static float shakeRadius = 10f;
        static bool shakeScreen = false;
        static Vector2 shakeOffset = new Vector2(15, 15);
        static int shakeCount = 0;
        static int maxShakes = 20;
        static int shakeStartAngle = 15;
        private AstroidManager _astroidManager;

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (shakeScreen)
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Matrix.CreateTranslation(shakeOffset.X, shakeOffset.Y, 0));
            else
                spriteBatch.Begin(SpriteSortMode.FrontToBack);

            _backgroundManager.Draw(spriteBatch);
            foreach (var sprite in _sprites)
                sprite.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin();

            float x = 10f;
            foreach (var player in _players)
            {
                spriteBatch.DrawString(_font, "Player: " + player.Score.PlayerName, new Vector2(x, 10f), Color.White);
                spriteBatch.DrawString(_font, "Health: " + player.Health, new Vector2(x, 30f), Color.White);
                spriteBatch.DrawString(_font, "Score: " + player.Score.Value, new Vector2(x, 50f), Color.White);

                x += 150;
            }
            spriteBatch.End();
        }
    }
}
