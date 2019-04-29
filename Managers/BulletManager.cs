using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Uranus.Sprites;

namespace Uranus.Managers
{
    public enum BulletType
    {
        Normal,
        Laser,
        Minigun,
        Plasma,
        Proton
    }

    public class BulletManager
    {
        public Texture2D Proton { get; set; }
        public Explosion Explosion { get; }
        public Texture2D Plasma { get; set; }

        public Texture2D Minigun { get; set; }

        public Texture2D Laser { get; set; }

        public Texture2D NormalBullet { get; set; }

        public BulletManager(ContentManager content)
        {
            var rootDir = "Bullets/";
            NormalBullet = content.Load<Texture2D>(rootDir + "Bullet");
            Laser = content.Load<Texture2D>(rootDir + "Laser_Large");
            Minigun = content.Load<Texture2D>(rootDir + "Minigun_Large");
            Plasma = content.Load<Texture2D>(rootDir + "Plasma_Large");
            Proton = content.Load<Texture2D>(rootDir + "Proton_Large");

            Explosion = new Explosion(new Dictionary<string, Models.Animation>()
            {
                {"Explode", new Models.Animation(content.Load<Texture2D>("Explosion"), 3) {FrameSpeed = 0.1f,}}
            })
            {
                Layer = 0.5F
            };
        }

        public Bullet GetBullet(BulletType bulletType = BulletType.Normal)
        {
            if (bulletType == BulletType.Normal)
                return new Bullet(NormalBullet)
                {
                    Explosion = Explosion
                };
            else if (bulletType == BulletType.Laser)
                return new Bullet(Laser)
                {
                    Explosion = Explosion
                };
            else if (bulletType == BulletType.Minigun)
                return new Bullet(Minigun)
                {
                    Explosion = Explosion
                };
            else if (bulletType == BulletType.Plasma)
                return new Bullet(Plasma)
                {
                    Explosion = Explosion
                };
            else if (bulletType == BulletType.Proton)
                return new Bullet(Proton)
                {
                    Explosion = Explosion
                };

            return new Bullet(NormalBullet)
            {
                Explosion = Explosion
            };
        }

    }
}
