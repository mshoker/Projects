using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;

namespace BattleShip.UI.Utility
{
    class DisplayBoards
    {
        public void GameBoard(Dictionary<Coordinate, ShotHistory> boardGame)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n     A  B  C  D  E  F  G  H  I  J");
            Console.Write("-----------------------------------");
            string tilda = "~";

            for (int i = 1; i < 11; i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("\n{0, -5}", i);
                for (int j = 1; j < 11; j++)
                {

                    ShotHistory shotHistory;
                    if (boardGame.TryGetValue(new Coordinate(j , i), out shotHistory) == false)
                    {
                        shotHistory = ShotHistory.Unknown;
                    }

                switch (shotHistory)
                    {
                        case ShotHistory.Hit:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("{0, -3}", "H");
                            break;
                        case ShotHistory.Miss:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("{0, -3}", "M");
                            break;
                        default:    
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("{0, -3}", tilda);
                            break;
                    }

                }
            }

          Console.WriteLine();  
        }

        public void ShipBoard(Dictionary<Coordinate, ShipPlacement>ShipHistory)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n     A  B  C  D  E  F  G  H  I  J");
            Console.Write("-----------------------------------");
            string tilda = "~";
            for (int i = 0; i < 10; i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("\n{0, -5}", i + 1);
                for (int j = 0; j < 10; j++)
                {

                    ShipPlacement shipPlacement;
                    ShipHistory.TryGetValue(new Coordinate(i, j), out shipPlacement);

                    switch (shipPlacement)
                    {
                        case ShipPlacement.NotEnoughSpace:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("{0, -3}", tilda);
                            break;
                        case ShipPlacement.Overlap:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("{0, -3}", tilda);
                            break;
                        case ShipPlacement.Ok:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("{0, -3}", "S");
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("{0, -3}", tilda);
                            break;
                    }


                }
            

                }
            }

        }
    }

