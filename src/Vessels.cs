using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;


namespace BattleShipGame.src
{
    public abstract class Vessels: GameObject
    {        
        private int _numOfWeapons;
        private Bitmap _image;
        public int    NumOfWeapons { get => _numOfWeapons; set => _numOfWeapons = value; }

        public Bitmap Image        { get => _image; set => _image = value; }
       

        public abstract void ThrowWeapon(List<GameObject> list);
        
    }
}
