using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using SwinGameSDK;
using System.IO;

namespace BattleShipGame.src
{
    class SubmarineUnitTest
    {
        Destroyer dest;
        DepthCharge dept;
        List<GameObject> list;
        Submarine subm;

        [SetUp]
        public void Initialize()
        {
            list = new List<GameObject>();
            subm = new Submarine(10);
            dest = Destroyer.GetInstance(10, 10);
            dept = new DepthCharge(dest);
        }

        [TestCase]
        public void TestThrowWeapon()
        {
            for(int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                subm.ThrowWeapon(list);
            }
            Assert.True(subm.NumOfWeapons < 10);
        }

        [TestCase]
        public void HitDepthCharge()
        {
            list.Add(dept);
            list.Add(subm);
            
            subm.PositionX = 2;
            subm.PositionY = 2;
            subm.Image = SwinGame.LoadBitmap(Directory.GetCurrentDirectory() + @"\Resources\images\Submarine.png");

            dept.PositionX = 3;
            dept.PositionY = 3;
            
            dept.WeaponsControll(list,dest);
            Assert.True(dept.Disappear);
        }

        [TestCase]
        public void MoveAndDisapper()
        {
            subm.PositionX = 8;
            subm.PositionY = 0;
            subm.Move();
            Assert.True(subm.Disappear);
        }

        public void Move()
        {
            subm.PositionX = 150;
            subm.Move();
            Assert.False(subm.PositionX == 150);
        }

    }
            
}
