using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Uranus.Managers;

namespace Uranus.Sprites
{
    public class TrackingEnemy : Enemy
    {
        private float _timer;
        private Texture2D _center;
        private Texture2D _left;
        private Texture2D _right;

        public TrackingEnemy(Texture2D texture, Texture2D blueLeft, Texture2D blueRight) : base(texture)
        {
            Speed = 2f;
            _center = texture;
            _left = blueLeft;
            _right = blueRight;
        }

        public override void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds >= ShootingTimer)
            {
                Shoot(-10f);
                ShootingTimer = gameTime.TotalGameTime.TotalMilliseconds + 1500;
            }

            if (_hitTimer < gameTime.TotalGameTime.TotalMilliseconds)
            {
                Colour = Color.White;
            }

            float? distance = null;
            Vector2 nearestPlayer = new Vector2();

            foreach (var player in PlayerManger.Players)
            {
                var tmpDistance = Vector2.Distance(Position, player.Position);

                if (distance == null || distance.Value > tmpDistance)
                {
                    if (Position.X > player.Position.X)
                    {
                        distance = tmpDistance;
                        nearestPlayer = player.Position;
                    }
                }
            }

            float yMaxSpeed = 0.35f*Speed;

            if (nearestPlayer == new Vector2())
            {
                yMaxSpeed = 0f;
            }
            else if ((nearestPlayer.Y - Position.Y) > (yMaxSpeed))
            {
                yMaxSpeed = yMaxSpeed;
            }
            else if (nearestPlayer.Y - Position.Y < -yMaxSpeed)
            {
                yMaxSpeed = yMaxSpeed * -1;
            }

            Position += new Vector2(-Speed, yMaxSpeed);

            // if the enemy is off the left side of the screen
            if (Position.X < -_texture.Width)
                IsRemoved = true;
        }
    }
}
