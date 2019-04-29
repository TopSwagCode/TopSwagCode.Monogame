using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Uranus.Models;

namespace Uranus.Sprites
{
    public class Explosion : Sprite
    {
        private float _timer = 0f;

        public Explosion(Dictionary<string, Animation> animations) : base(animations)
        {

        }

        public override void Update(GameTime gameTime)
        {
            _animationManager.Update(gameTime);

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > _animationManager.CurrentAnimation.FrameCount * _animationManager.CurrentAnimation.FrameSpeed)
                IsRemoved = true;
        }
    }
}
