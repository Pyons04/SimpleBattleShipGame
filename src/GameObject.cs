using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace BattleShipGame.src
{
    public abstract class GameObject
    {
        private int _positionX;
        private int _positionY;
        private bool _disappear;


        public int PositionX { get => _positionX; set => _positionX = value; }
        public int PositionY { get => _positionY; set => _positionY = value; }
        public bool Disappear { get => _disappear; set => _disappear = value; }

        public abstract void Move();
        public abstract void Draw();


    }
}
