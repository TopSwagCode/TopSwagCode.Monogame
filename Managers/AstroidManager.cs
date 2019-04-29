using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Uranus.Sprites;

namespace Uranus.Managers
{
    public enum AstroidType
    {
        Default,
        Small,
        Medium,
        Large
    }


    public class AstroidManager
    {
        public Explosion Explosion { get; }
        private float _timer;

        public float SpawnTimer { get; set; }

        public bool CanAdd { get; set; }
        public int MaxAstroids { get; set; }

        public AstroidManager(ContentManager content)
        {
            Explosion = new Explosion(new Dictionary<string, Models.Animation>()
            {
                {"Explode", new Models.Animation(content.Load<Texture2D>("Explosion"), 3) {FrameSpeed = 0.1f,}}
            })
            {
                Layer = 0.5F
            };

            Astroids = new List<Texture2D>()
            {content.Load<Texture2D>("Asteroids/Asteroid01"),content.Load<Texture2D>("Asteroids/Asteroid02"),content.Load<Texture2D>("Asteroids/Asteroid03"),content.Load<Texture2D>("Asteroids/Asteroid04")
            };
            MaxAstroids = 20;
            SpawnTimer = 0.5f;
        }

        public List<Texture2D> Astroids { get; set; }

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
        public Astroid GetAstroid()
        {
            int astroidType = Game1.Random.Next(0, Astroids.Count);
            var astroid = new Astroid(Astroids[astroidType])
            {
                Colour = Color.White,
                Layer = 0.2f,
                Position = new Vector2(Game1.ScreenWidth + Astroids[astroidType].Width, Game1.Random.Next(0, Game1.ScreenHeight)),
                Velocity = new Vector2(-5 - (float)Game1.Random.NextDouble(), Game1.Random.Next(-5, 5)),
                LifeSpan = 5f,
                Explosion = Explosion

            };

            return astroid;
        }
    }
}


