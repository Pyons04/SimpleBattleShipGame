using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipGame.src
{
    public abstract class Weapons : GameObject
    {
        public abstract bool CollisionDetection(int targetX, int targetY,int target_width, int target_height);
        public abstract void WeaponsControll   (List<GameObject> GameObjects, Destroyer myShip);
    }
}
