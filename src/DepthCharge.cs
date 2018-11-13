using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;
using System.IO;

namespace BattleShipGame.src
{
    public class DepthCharge : Weapons,IEffect
    {
        public Timer _explodeTime;
        public bool  _explode;
        public Bitmap _explodeImage;
        public SoundEffect _explodeSound;

        public DepthCharge(Destroyer myShip)
        {
            PositionX = myShip.PositionX;
            PositionY = myShip.PositionY + myShip.Image.Height;
        }

        public void HitEffect()
        {
            _explodeSound = SwinGame.LoadSoundEffect(Directory.GetCurrentDirectory() + @"\Resources\sounds\explode.wav");
            _explodeImage = SwinGame.LoadBitmap(Directory.GetCurrentDirectory() + @"\Resources\images\explode.png");
            _explodeTime = new Timer();
            _explode = true;
            _explodeSound.Play();
            _explodeTime.Start();
        }

        public override void Move()
        {
            if (_explode == false)
            {
                PositionY = PositionY + 4;
            }

            if (PositionY >= SwinGame.WindowHeight("GameMain") * 0.95)
            {
                Disappear = true;//要素の削除
            }

            if(_explodeTime != null && _explodeTime.Ticks > 300 && _explode)
            {              
                Disappear = true;
            }
        }

        public override void Draw()
        {
            if (_explode)
            {
                SwinGame.DrawBitmap(_explodeImage, PositionX, PositionY);
            }
            else
            {
                SwinGame.FillRectangle(Color.Green, PositionX, PositionY, 5, 13);
            }
        }

        public override bool CollisionDetection(int targetX, int targetY, int target_width, int target_height)
        {
            if (PositionX > targetX && PositionX < targetX + target_width*0.9 && targetY <= PositionY)
            {
                    this.HitEffect();
                    return true;
            }
            else
            {
                return false;
            }
        }


        public override void WeaponsControll(List<GameObject> GameObjects, Destroyer myShip)
        {    
             this.Move();
             //このCollisionDetectionをGameObjects List内のすべてのSubmarineに対して行う必要がある。
             for (int j = GameObjects.Count - 1; j >= 0; j--)
                {
                   if (GameObjects[j] is Submarine && this.Disappear == false)
                   {
                       Submarine sub = GameObjects[j] as Submarine;
                       if (this.CollisionDetection(GameObjects[j].PositionX, GameObjects[j].PositionY, SwinGame.BitmapWidth(sub.Image), SwinGame.BitmapHeight(sub.Image)))
                       {               
                           sub.Disappear = true;
                           myShip.Hit++;
                       }
                    }
                 }
              }
    }
        }

