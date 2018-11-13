using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SwinGameSDK;

namespace BattleShipGame.src
{
    class DepthChargeUnitTest
    {
        List<GameObject> list;
        Submarine sub;
        Destroyer dest;
        DepthCharge dept;

        [SetUp]
        public void Initialize()
        {
            dest = Destroyer.GetInstance(5, 5);
            sub = new Submarine(10);//Initital Position X=20,Y=200~570
            list = new List<GameObject>();
            dept = new DepthCharge(dest);
            sub.PositionY = 170;
            list.Add(sub);
        }

        [TestCase]
        public void MoveAndDisapper()
        {
            dept.PositionX = 100;
            dept.PositionY = 200;
            SwinGame.OpenGraphicsWindow("GameMain", 800, 600);
            for (int i = 1; i < 100; i++)
            {
                dept.WeaponsControll(list,dest);
            }
            Assert.True(dept.Disappear);
        }

        [TestCase]
        public void Move()
        {
            dept.PositionX = 100;
            dept.PositionY = 200;
            SwinGame.OpenGraphicsWindow("GameMain", 800, 600);            
            dept.Move();
            Assert.True(dept.PositionY == 200+4);
        }

        [TestCase]
        public void HitSubmarine()
        {
            //sub position 20,170
            dept.PositionX = 21;
            dept.PositionY = 151;
            
            for (int i = 1; i < 6; i++)
            {
                dept.WeaponsControll(list,dest);
            }
            Assert.True(dept.CollisionDetection(sub.PositionX, sub.PositionY, sub.Image.Width, sub.Image.Height));           
        }      

    }
}
