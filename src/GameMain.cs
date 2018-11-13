using System;
using SwinGameSDK;
using System.IO;

namespace BattleShipGame.src
{
    public class GameMain
    {
               
        public static void Main()
        {
            Panel ocean = SwinGame.LoadPanel("panel.txt");
            SwinGame.ActivatePanel(ocean);
            //Open the game window
            SwinGame.OpenGraphicsWindow("GameMain", 800, 600);
            //SwinGame.ShowSwinGameSplashScreen();

            GameControll controll = GameControll.GetInstance();
            GameControll controll2 = GameControll.GetInstance();
            Timer aircraft = new Timer();
            aircraft.Start();

            SwinGame.OpenAudio();
           
            
            //Run the game loop
            while(false == SwinGame.WindowCloseRequested())
            {
                
                //Fetch the next batch of UI interaction
                SwinGame.ProcessEvents();
                
                //Clear the screen and draw the framerate
                SwinGame.ClearScreen(Color.White);
                SwinGame.DrawFramerate(0,0);
                SwinGame.ShowPanel(ocean);
                SwinGame.DrawInterface();

                SwinGame.DrawInterface();

                controll.Draw();
                controll.MoveSubmarine();
                
                controll.MoveTorpedo();
                controll.MoveDepthCharge();
                controll.EraseDissapper();
                controll.MoveAirCraft();
                controll.MoveSupply();

                //Draw onto the screen
                SwinGame.RefreshScreen(60);
               
                if (SwinGame.KeyDown(KeyCode.LeftKey)|| SwinGame.KeyDown(KeyCode.RightKey))
                {
                    controll.MoveMyShip();
                }

                if (SwinGame.KeyTyped(KeyCode.DKey) || SwinGame.KeyTyped(KeyCode.DKey))
                {
                    controll.ThrowDepthCharge();
                }

                if(aircraft.Ticks > 5000)
                {
                    aircraft.Reset();
                    aircraft.Start();
                    controll.DispatchAirPlane();
                }

            }
        }
    }
}