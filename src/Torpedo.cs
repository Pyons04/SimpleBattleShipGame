using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;
using System.IO;

namespace BattleShipGame.src
{
    public class Torpedo : Weapons, IEffect
    {
        private int _speed;
        public Timer _explodeTime;
        public bool _explode;
        public Bitmap _explodeImage;
        public SoundEffect _explodeSound;


        public Torpedo(int x, int y)
        {
            PositionX = x;
            PositionY = y;
            _speed = new System.Random().Next(1, 7);
        }

        public void HitEffect()
        {
            _explodeSound = SwinGame.LoadSoundEffect(Directory.GetCurrentDirectory() + @"\Resources\sounds\Splash.wav");
            _explodeImage = SwinGame.LoadBitmap(Directory.GetCurrentDirectory() + @"\Resources\images\Splash.png");
            _explodeTime = new Timer();
            _explode = true;
            _explodeSound.Play();
            _explodeTime.Start();
        }

        public override bool CollisionDetection(int targetX, int targetY, int target_width, int target_height)
        {

            if (PositionX > targetX && PositionX < targetX + target_width && targetY + target_height >= PositionY)
            {
                this.HitEffect();
                return true;
            }
            else if (PositionY <= targetY + target_height && _explode == false)
            {
                Disappear = true;//水面に到達したかどうかの判定
                return false;
            }

            else
            {
                return false;
            }
        }

        public override void Move()
        {
            if (_explode == false)
            {
                PositionY = PositionY - _speed;
            }

            if (_explodeTime != null && _explodeTime.Ticks > 600 && _explode)
            {
                Disappear = true;
            }
        }

        public override void Draw()
        {
            if (_explode)
            {
                SwinGame.DrawBitmap(_explodeImage, PositionX, 88 + 101 - _explodeImage.Height);
            }
            else
            {
                SwinGame.FillRectangle(Color.Yellow, PositionX, PositionY, 13, 5);
            }
        }

        public override void WeaponsControll(List<GameObject> GameObjects, Destroyer myShip)
        {
            this.Move();

            if (_explodeTime == null && this.Disappear == false && this.CollisionDetection(myShip.PositionX, myShip.PositionY, SwinGame.BitmapWidth(myShip.Image), SwinGame.BitmapHeight(myShip.Image)))
            {
                myShip.Hp--;
            }

        }
    }
}

