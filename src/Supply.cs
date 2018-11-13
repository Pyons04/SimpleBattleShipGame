using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;
using System.IO;

namespace BattleShipGame.src
{
    public class Supply : Weapons
    {
        private Bitmap _image;

        public Supply(int x,int y)
        {
            PositionX = x;
            PositionY = y;
            _image = SwinGame.LoadBitmap(Directory.GetCurrentDirectory() + @"\Resources\images\Supply.png");
        }

        public override bool CollisionDetection(int targetX, int targetY, int target_width, int target_height)
        {
            if (PositionY >= targetY + target_height)
            {
                Disappear = true;//水面に到達したかどうかの判定
            }

            if (PositionX > targetX && PositionX < targetX + target_width && targetY  < PositionY && Disappear == false)
            {
                Disappear = true;
                return true;
            }
            else
            {
                return false;
            }          
        }

        public override void Draw()
        {
            SwinGame.DrawBitmap(_image,PositionX,PositionY);
        }

        public override void Move()
        {
            PositionY = PositionY + 3;
        }

        public override void WeaponsControll(List<GameObject> GameObjects, Destroyer myShip)
        {                                    
              this.Move();
              if (myShip.Hp > 0 && this.CollisionDetection(myShip.PositionX, myShip.PositionY, SwinGame.BitmapWidth(myShip.Image), SwinGame.BitmapHeight(myShip.Image)))
              {
                 myShip.HitEffect();
                 myShip.Hp = myShip.Hp + 2;
                 myShip.NumOfWeapons = myShip.NumOfWeapons + 2;
              }
        }

    }
}
