using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using SwinGameSDK;

namespace BattleShipGame.src
{
    class AirplaneUnitTest
    {
        Destroyer dest;
        Aircraft airc;
        List<GameObject> list;
        

        [SetUp]
        public void Initialize()
        {
            list = new List<GameObject>();
            airc = new Aircraft(10);
            dest = Destroyer.GetInstance(2, 2);
            dest.name = "This is a instance of Aircraft Unit Test";
        }

        [TestCase]
        public void TestMove()
        {
            int previous = airc.PositionX;
            airc.Move();
            Assert.True(previous == airc.PositionX - 6);
        }

        [TestCase]
        public void TestDisapper()
        {
            SwinGame.OpenGraphicsWindow("GameMain", 800, 600);
            airc.PositionX = (short)(800 * 0.9);
            airc.Move();
            Assert.True(airc.Disappear);
        }

        [TestCase]
        public void TestThrowSupply()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                airc.ThrowWeapon(list);
            }
            Assert.True(airc.NumOfWeapons < 10);
        }



    }
}
