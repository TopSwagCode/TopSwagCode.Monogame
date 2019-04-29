using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Uranus.Sprites;

namespace Uranus.Managers
{
    public enum PlayerColour
    {
        Red = 0,
        Blue = 1,
        Green = 2
    }

    public enum PlayerControls
    {
        Wasd = 0,
        Arrow = 1,
        GamePad1 = 2,
        GamePad2 = 3,
        GamePad3 = 4,
        GamePad4 = 5
    }

    public class PlayerManger
    {
        private readonly BulletManager _bulletManager;
        public static List<Player> Players { get; set; }
        private Texture2D GreenPlayerShipTexture2D { get; set; }
        public Texture2D GreenPlayerShipTexture2DRight { get; set; }
        public Texture2D GreenPlayerShipTexture2DLeft { get; set; }
        private Texture2D RedPlayerShipTexture2D { get; set; }
        public Texture2D RedPlayerShipTexture2DRight { get; set; }
        public Texture2D RedPlayerShipTexture2DLeft { get; set; }
        private Texture2D BluePlayerShipTexture2D { get; set; }
        public Texture2D BluePlayerShipTexture2DRight { get; set; }
        public Texture2D BluePlayerShipTexture2DLeft { get; set; }


        public PlayerManger(ContentManager content, BulletManager bulletManager)
        {
            _bulletManager = bulletManager;
            RedPlayerShipTexture2D = content.Load<Texture2D>("Ships/PlayerRed_Center");
            RedPlayerShipTexture2DLeft = content.Load<Texture2D>("Ships/PlayerRed_Left");
            RedPlayerShipTexture2DRight = content.Load<Texture2D>("Ships/PlayerRed_Right");

            BluePlayerShipTexture2D = content.Load<Texture2D>("Ships/PlayerBlue_Center");
            BluePlayerShipTexture2DLeft = content.Load<Texture2D>("Ships/PlayerBlue_Left");
            BluePlayerShipTexture2DRight = content.Load<Texture2D>("Ships/PlayerBlue_Right");

            GreenPlayerShipTexture2D = content.Load<Texture2D>("Ships/PlayerGreen_Center");
            GreenPlayerShipTexture2DLeft = content.Load<Texture2D>("Ships/PlayerGreen_Left");
            GreenPlayerShipTexture2DRight = content.Load<Texture2D>("Ships/PlayerGreen_Right");

            if (Players == null)
            {
                Players = new List<Player>();
            }
        }

        public Player GetPlayer(PlayerColour colour, PlayerControls controls, string playerName)
        {
            Player player;

            if (colour == PlayerColour.Red)
            {
                player = new Player(RedPlayerShipTexture2D, RedPlayerShipTexture2DLeft, RedPlayerShipTexture2DRight);
                player.Bullet = _bulletManager.GetBullet(BulletType.Minigun);
            }
            else if (colour == PlayerColour.Blue)
            {
                player = new Player(BluePlayerShipTexture2D, BluePlayerShipTexture2DLeft, BluePlayerShipTexture2DRight);
                player.Bullet = _bulletManager.GetBullet(BulletType.Plasma);
            }
            else
            {
                player = new Player(GreenPlayerShipTexture2D, GreenPlayerShipTexture2DLeft, GreenPlayerShipTexture2DRight);
                player.Bullet = _bulletManager.GetBullet(BulletType.Laser);
            }

            if (controls == PlayerControls.Wasd)
            {
                player.Input = new Models.Input()
                {
                    Up = Keys.W,
                    Down = Keys.S,
                    Left = Keys.A,
                    Right = Keys.D,
                    Shoot = Keys.Space,
                };
            }
            else
            {
                player.Input = new Models.Input()
                {
                    Up = Keys.Up,
                    Down = Keys.Down,
                    Left = Keys.Left,
                    Right = Keys.Right,
                    Shoot = Keys.Enter,
                };
            }


            player.Colour = Color.White;
            player.Position = new Vector2(100, 50);
            player.Layer = 0.3f;

            player.Health = 10;
            player.Score = new Models.Score()
            {
                PlayerName = playerName,
                Value = 0,
            };

            Players.Add(player);

            return player;
        }
    }
}
