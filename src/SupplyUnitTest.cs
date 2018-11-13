using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SwinGameSDK;

namespace BattleShipGame.src
{
    class SupplyUnitTest
    {
        List<GameObject> list;
        Destroyer dest;
        Supply supply;

        [SetUp]
        public void Initialize()
        {
            dest = Destroyer.GetInstance(2, 2);
            supply = new Supply(103,60);
            list = new List<GameObject>();
            list.Add(supply);
            list.Add(dest);
        }

        [TestCase]
        public void MoveAndDisapper()
        {
            for (int i = 1; i < 100; i++)
            {
                supply.WeaponsControll(list,dest);
            }
            Assert.True(supply.Disappear);
        }

        [TestCase]
        public void Move()
        {
            int previous = supply.PositionY;
            supply.Move();
            Assert.True(supply.PositionY == previous + 3);
        }

        [TestCase]
        public void HitDestroyer()
        {
            int previous = dest.Hp;
            for (int i = 1; i < 11; i++)
            {
                supply.WeaponsControll(list, dest);
            }
            //Assert.Null(supply.PositionY);
            Assert.True(dest.Hp > previous);
        }
    }
}
