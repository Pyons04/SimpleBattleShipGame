using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;
using System.IO;

namespace BattleShipGame.src
{
    public class Aircraft :Vessels
    {
        
        private Timer _timer;

        public Aircraft(int numOfLife)
        {
            NumOfWeapons = numOfLife;
            Image = SwinGame.LoadBitmap(Directory.GetCurrentDirectory() + @"\Resources\images\Aircraft.png");
            PositionX = 20;
            PositionY = 10;
            _timer = new Timer();
            _timer.Start();
        }

        public override void Move()
        {
            PositionX = PositionX + 6;
            if(PositionX > SwinGame.WindowWidth("GameMain") * 0.9)
            {
                Disappear = true;
            }
        }

        public override void Draw()
        {
            SwinGame.DrawBitmap(Image, PositionX, PositionY);
        }

        public override void ThrowWeapon(List<GameObject> list)
        {
            if (_timer.Ticks > new System.Random().Next(300, 30000) && NumOfWeapons > 0 )
            {
                NumOfWeapons = NumOfWeapons - 1;
                list.Add(new Supply(PositionX, PositionY + SwinGame.BitmapHeight(Image) / 2));
                _timer.Reset();
                _timer.Start();
            }
        }
    }
}
