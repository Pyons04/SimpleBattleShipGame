using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;
using System.IO;


namespace BattleShipGame.src
{
    public class Destroyer : Vessels, IEffect
    {
        private int _hp;
        private int _hit;
        private Bitmap _sinkShip;

        private Timer _effectTime;
        private Bitmap _effectImage;
        private SoundEffect _effectSound;
        public  string name;

        private static Destroyer instance;

        public int Hp { get  => _hp; set { if (_hp > 0) { _hp = value; } } }
        //画面上に出力するためにgetが必要。setはTorpedoからのアクセスに必要。
        public int Hit { get => _hit; set => _hit = value; }
        //画面上に出力するためにgetが必要　setはTorpedoからのアクセスに必要。
        
        private Destroyer(int numOfWeapns,int hp)
        {
            NumOfWeapons = numOfWeapns;
            Image = SwinGame.LoadBitmap(Directory.GetCurrentDirectory() + @"\Resources\images\BattleShip.png");
            _sinkShip = SwinGame.LoadBitmap(Directory.GetCurrentDirectory() + @"\Resources\images\SinkShip.png");
            PositionX = 100;
            PositionY = 88;
            _hp = hp;
            Hit= 0; 
        }

        public static Destroyer GetInstance(int numOfWeapns, int hp)
        {
            if(instance == null)
                instance = new Destroyer(numOfWeapns,hp);
                return instance;            
        }


        public void HitEffect()
        {
            _effectTime = new Timer();
            _effectTime.Start();
            _effectSound = SwinGame.LoadSoundEffect(Directory.GetCurrentDirectory() + @"\Resources\sounds\GetSupply.wav");
            _effectSound.Play();
            _effectImage = SwinGame.LoadBitmap(Directory.GetCurrentDirectory() + @"\Resources\images\GetSupply.png");
            Image = _effectImage;
        }

        public override void Move()
        {
            if (SwinGame.KeyDown(KeyCode.RightKey)&& _hp > 0)
            {
                PositionX = PositionX + 1;
            }
            else if (SwinGame.KeyDown(KeyCode.LeftKey) && _hp > 0 )
            {
                PositionX = PositionX - 1;
            }
        }

        public override void Draw()
        {
            if (Hp > 0)
            {
                SwinGame.DrawBitmap(Image, PositionX, PositionY);
            }
            else
            {
                
                SwinGame.DrawBitmap(_sinkShip, PositionX, PositionY);
            }

            if (_effectTime != null && _effectTime.Ticks > 500)
            {
                _effectTime.Stop();
                _effectTime.Reset();
                Image = SwinGame.LoadBitmap(Directory.GetCurrentDirectory() + @"\Resources\images\BattleShip.png");
            }

        }

        public override void ThrowWeapon(List<GameObject> list)
        {
            if (NumOfWeapons > 0 && Hp > 0)
            {
                NumOfWeapons = NumOfWeapons - 1;
                list.Add(new DepthCharge(this));
                SwinGame.LoadSoundEffect(Directory.GetCurrentDirectory() + @"\Resources\sounds\ThrowDepthCharge.wav").Play();
            }
        }

    }
}
