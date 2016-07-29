using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;
using BattleShip.UI.Utility;

namespace BattleShip.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            bool keepPlaying;
            

            WorkFlow game = new WorkFlow();
            do
            {
                game.StartMenu();
                Console.Clear();
                game.InitialSetUp();
                Console.Clear();
                game.PlayGameLoop();
                keepPlaying = game.KeepPlaying();

            } while (keepPlaying == true);

            game.ThankYouForPlaying();
            

        }

        
    }
}
