using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Uranus.Sprites;

namespace Uranus.Managers
{
    public class EnemyManager
    {
        private float _timer;

        private List<Texture2D> _textures;

        public bool CanAdd { get; set; }

        public Bullet Bullet { get; set; }

        public int MaxEnemies { get; set; }

        public float SpawnTimer { get; set; }

        public EnemyManager(ContentManager content)
        {

            // Change logic to be Object based and not texture based
            _textures = new List<Texture2D>()
      {
            content.Load<Texture2D>("Ships/EnemyGreen"),
            content.Load<Texture2D>("Ships/EnemyGreen2"),
            content.Load<Texture2D>("Ships/EnemyBlue"),
            content.Load<Texture2D>("Ships/EnemyBlue2"),
            content.Load<Texture2D>("Ships/EnemyTeal"),
            content.Load<Texture2D>("Ships/EnemyTeal2"),
      };

            BlueLeft = content.Load<Texture2D>("Ships/EnemyBlueLeft");
            BlueRight = content.Load<Texture2D>("Ships/EnemyBlueRight");
            BlueLeft2 = content.Load<Texture2D>("Ships/EnemyBlue2Left");
            BlueRight2 = content.Load<Texture2D>("Ships/EnemyBlue2Right");

            MaxEnemies = 20;
            SpawnTimer = 1.5f;
        }

        public Texture2D BlueRight2 { get; set; }

        public Texture2D BlueLeft2 { get; set; }

        public Texture2D BlueRight { get; set; }

        public Texture2D BlueLeft { get; set; }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            CanAdd = false;

            if (_timer > SpawnTimer)
            {
                CanAdd = true;

                _timer = 0;
            }
        }

        public Enemy GetEnemy()
        {
            int enemyType = Game1.Random.Next(0, _textures.Count);
            var texture = _textures[enemyType];

            if (enemyType == 0)
            {
                return new Enemy(texture)
                {
                    Colour = Color.White,
                    Bullet = Bullet,
                    Health = 2,
                    Layer = 0.2f,
                    Position = new Vector2(Game1.ScreenWidth + texture.Width, Game1.Random.Next(0, Game1.ScreenHeight)),
                    Speed = 5 + (float)Game1.Random.NextDouble(),
                    ShootingTimer = 1.5f + (float)Game1.Random.NextDouble(),
                };
            }
            else if (enemyType == 1)
            {
                return new Enemy(texture)
                {
                    Colour = Color.White,
                    Bullet = Bullet,
                    Health = 5,
                    Layer = 0.2f,
                    Position = new Vector2(Game1.ScreenWidth + texture.Width, Game1.Random.Next(0, Game1.ScreenHeight)),
                    Speed = 5 + (float)Game1.Random.NextDouble(),
                    ShootingTimer = 1.5f + (float)Game1.Random.NextDouble(),
                };

            }
            else if (enemyType == 2)
            {
                return new TrackingEnemy(texture, BlueLeft, BlueRight)
                {
                    Colour = Color.White,
                    Bullet = Bullet,
                    Health = 2,
                    Layer = 0.2f,
                    Position = new Vector2(Game1.ScreenWidth + texture.Width, Game1.Random.Next(0, Game1.ScreenHeight)),
                    Speed = 5 + (float)Game1.Random.NextDouble(),
                    ShootingTimer = 1.5f + (float)Game1.Random.NextDouble(),
                };
            }
            else if (enemyType == 3)
            {
                return new TrackingEnemy(texture, BlueLeft2, BlueRight2)
                {
                    Colour = Color.White,
                    Bullet = Bullet,
                    Health = 5,
                    Layer = 0.2f,
                    Position = new Vector2(Game1.ScreenWidth + texture.Width, Game1.Random.Next(0, Game1.ScreenHeight)),
                    Speed = 5 + (float)Game1.Random.NextDouble(),
                    ShootingTimer = 1.5f + (float)Game1.Random.NextDouble(),
                };
            }
            else if (enemyType == 4)
            {
                return new CurveEnemy(texture)
                {
                    Colour = Color.White,
                    Bullet = Bullet,
                    Health = 2,
                    Layer = 0.2f,
                    Position = new Vector2(Game1.ScreenWidth + texture.Width, Game1.Random.Next(0, Game1.ScreenHeight)),
                    Speed = 5 + (float)Game1.Random.NextDouble(),
                    ShootingTimer = 1.5f + (float)Game1.Random.NextDouble(),
                };
            }
            else if (enemyType == 5)
            {
                return new CurveEnemy(texture)
                {
                    Colour = Color.White,
                    Bullet = Bullet,
                    Health = 5,
                    Layer = 0.2f,
                    Position = new Vector2(Game1.ScreenWidth + texture.Width, Game1.Random.Next(0, Game1.ScreenHeight)),
                    Speed = 5 + (float)Game1.Random.NextDouble(),
                    ShootingTimer = 1.5f + (float)Game1.Random.NextDouble(),
                };
            }

            return new Enemy(texture)
            {
                Colour = Color.White,
                Bullet = Bullet,
                Health = 5,
                Layer = 0.2f,
                Position = new Vector2(Game1.ScreenWidth + texture.Width, Game1.Random.Next(0, Game1.ScreenHeight)),
                Speed = 2 + (float)Game1.Random.NextDouble(),
                ShootingTimer = 1.5f + (float)Game1.Random.NextDouble(),
            };


        }
    }
}
