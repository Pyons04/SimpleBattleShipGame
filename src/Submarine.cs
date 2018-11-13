using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;
using System.IO;

namespace BattleShipGame.src
{
    public class Submarine : Vessels
    {
        private int _speed;
        private Timer _timer;
        private bool _headLeft  = false;

        public Submarine(int setWeapns)
        {
            NumOfWeapons = setWeapns;
            Image = SwinGame.LoadBitmap(Directory.GetCurrentDirectory() + @"\Resources\images\Submarine.png");
            PositionX = 20;
            PositionY = new System.Random().Next(0, 370) + 200;
            //Generate Random Speed
            _speed     = new System.Random().Next(1,5);

            //Generate Timer for Torpedo
            _timer = new Timer();
            _timer.Start();

            if(new System.Random().Next(1, 100) < 50)
            {
                _headLeft = true;
                PositionX = (short)(SwinGame.WindowWidth("GameMain") * 0.9);
                Image = SwinGame.LoadBitmap(Directory.GetCurrentDirectory() + @"\Resources\images\Submarine2.png");
            }
            SwinGame.ColorTransparent();
        }

        public override void Move()
        {
            if (_headLeft)
            {
                PositionX = PositionX - _speed;
                if (PositionX <10)
                {
                    Disappear = true;
                }
            }
            else
            {
                PositionX = PositionX + _speed;
                if (PositionX >= SwinGame.WindowWidth("GameMain") * 0.9)
                {
                    Disappear = true;//要素の削除
                }
            }
        }

        public override void Draw()
        {
            SwinGame.DrawBitmap(Image, PositionX, PositionY);
        }

        public override void ThrowWeapon(List<GameObject> list)
        {
            if (_timer.Ticks > 900 && NumOfWeapons > 0 && new System.Random().Next(1, 100) < 30 )
            {
                NumOfWeapons = NumOfWeapons - 1;
                list.Add(new Torpedo(PositionX, PositionY + SwinGame.BitmapHeight(Image)/2));
                _timer.Reset();
            }
        }

    }
}
