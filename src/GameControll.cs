using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;
using System.IO;

namespace BattleShipGame.src
{
    class GameControll
    {
        private List<GameObject> GameObjects;
        private Destroyer myShip;
        private Bitmap _gameOver;

        private static GameControll instance;
        private GameControll()
        {
            GameObjects = new List<GameObject>();
            myShip = Destroyer.GetInstance(5, 5);
            
            Submarine firstSub = new Submarine(3);
            GameObjects.Add(myShip);
            GameObjects.Add(firstSub);
            _gameOver = SwinGame.LoadBitmap(Directory.GetCurrentDirectory() + @"\Resources\images\GameOver.png");
        }

        public static GameControll GetInstance()
        {
            if (instance == null)           
                instance = new GameControll();
                return instance;          
        }

        public void DispatchAirPlane()
        {
            Aircraft cargo = new Aircraft(1);
            GameObjects.Add(cargo);
        }

        public void MoveAirCraft()
        {
            for (int i = GameObjects.Count - 1; i >= 0; i--)
            {
                if (GameObjects[i] is Aircraft)
                {
                    Aircraft ac = GameObjects[i] as Aircraft;
                    ac.Move();
                    ac.ThrowWeapon(GameObjects);
                }
            }
        }

        public void MoveSupply()
        {
            for (int i = GameObjects.Count - 1; i >= 0; i--)
            {
                if (GameObjects[i] is Supply)
                {
                    Supply sp = GameObjects[i] as Supply;
                    sp.WeaponsControll(GameObjects,myShip);                   
                }
            }
        }

        public void MoveSubmarine()
        {
            if (GameObjects.Count(n => n is Submarine) < 2)
            {
                GameObjects.Add(new Submarine(5));
            }

            for (int i = GameObjects.Count - 1; i >= 0; i--)
            {
                if (GameObjects[i] is Submarine)
                {
                    Submarine sm = GameObjects[i] as Submarine;
                    sm.Move();
                    sm.ThrowWeapon(GameObjects);
                }
            }
        }

        public void MoveTorpedo()
        {
            for (int i = GameObjects.Count - 1; i >= 0; i--)
            {
                if (GameObjects[i] is Torpedo)
                {
                    Torpedo tp = GameObjects[i] as Torpedo;
                    tp.WeaponsControll(GameObjects, myShip);
                }
            }
        }

        public void MoveDepthCharge()
        {
            for (int i = GameObjects.Count - 1; i >= 0; i--)
            {
                if (GameObjects[i] is DepthCharge)
                {
                    DepthCharge dp = GameObjects[i] as DepthCharge;
                    dp.WeaponsControll(GameObjects,myShip);
                }
            }
        }

        public void MoveMyShip()
        {         
            myShip.Move();
        }

        public void ThrowDepthCharge()
        {
            myShip.ThrowWeapon(GameObjects);
        }

        public void EraseDissapper()
        {
            for (int i = GameObjects.Count - 1; i >= 0; i--)
            {
                    if (GameObjects[i].Disappear)
                    {
                        GameObjects.Remove(GameObjects[i]);//要素の削除
                    }               
            }
        }

        public void Draw()
        {            
            
            if (myShip.Hp <= 0)
            {
                SwinGame.DrawBitmap(_gameOver, 0, 0);
            }
            foreach (GameObject obj in GameObjects)
            {
                obj.Draw();
            }
            SwinGame.DrawText("Hp          :" + myShip.Hp.ToString(), Color.Red, 630, 10);
            SwinGame.DrawText("Depth Charge:" + myShip.NumOfWeapons.ToString(), Color.Green, 630, 18);
            SwinGame.DrawText("Destroyed   :" + myShip.Hit.ToString(), Color.Blue, 630, 26);
        }

    }
}
