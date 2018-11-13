using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SwinGameSDK;

namespace BattleShipGame.src
{
    class TorpedoUnitTestcs
    {
        Destroyer dest;
        Torpedo torp;
        List<GameObject> list;
        Submarine subm;
        
        [SetUp]
        public void Initialize()
        {
            torp = new Torpedo(10, 10);
            list = new List<GameObject>();
            dest = Destroyer.GetInstance(2,2);
            subm = new Submarine(5);
            list.Add(dest);
            list.Add(subm);
        }

        [TestCase]
        public void SurfaceDisapper()
        {
            for (int i = 0; i < 10; i++)
            {
                torp.WeaponsControll(list,dest);
            }
            Assert.True(torp.Disappear);
        }

        [TestCase]
        public void TestHitDestroyer()
        {
            Torpedo torp2 = new Torpedo(105,200);
            int previous = dest.Hp;
            for (int i = 0; i < 20; i++)
            {
                torp2.WeaponsControll(list, dest);
            }
            
            Assert.True(dest.Hp < previous);
        }

        [TestCase]
        public void TestMove()
        {
            int previous = torp.PositionY;
            torp.Move();
            Assert.True(torp.PositionY < previous);
        }

        [TestCase]
        public void TestExplode()
        {
            Torpedo torp2 = new Torpedo(105, 200);            
            for (int i = 0; i < 20; i++)
            {
                torp2.WeaponsControll(list, dest);
            }
            
            Assert.True(torp2._explode);
        }

    }
}

