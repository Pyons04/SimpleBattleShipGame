using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;

namespace BattleShipGame.src
{
    class DestroyerUnitTest
    {
        Destroyer des;
        List<GameObject> list;
        Submarine sub;

        [SetUp]
        public void Initialize()
        {
            des = Destroyer.GetInstance(2,2);
            list= new List<GameObject>();
            sub = new Submarine(2); 
        }

        [TestCase]
        public void CheckSingleton()
        {
            des.Hp = 10;
            Destroyer des2 = Destroyer.GetInstance(2, 2);
            des2.Hp = 20;
            Assert.True(des.Hp == des2.Hp);
        }

        [TestCase]
        public void ATestThrowWeapon()
        {
            des.ThrowWeapon(list);
            Assert.True(list[0] is DepthCharge);
        }

        [TestCase]
        public void HpLessThan0()
        {
            int previoust = des.Hp; 
            des.Hp = des.Hp - des.Hp;//turen to 0.
            des.Hp = des.Hp + 10;
            Assert.True(des.Hp == 0);
        }

    }
}
