using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;

namespace BattleShip.UI.Utility
{
    public class WorkFlow
    {
        Dictionary<int, Player> Players = new Dictionary<int, Player>();
        static Player playerOne = new Player();
        static Player playerTwo = new Player();
        
        static DisplayBoards board = new DisplayBoards();
        
        #region Prompt and Display Methods

        public static void Display(string message)
        {
            Console.WriteLine(message);
        }

        public static string PromptString(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
   
        public static bool IsNullOrEmpty(string str)
        {
            if (str == "")
            {
                return true;
            }
            else return false;

        }

        public static string PromptCoordinate(string message)

        {
            string userInput = PromptString(message).ToUpper();
            
            bool valid = false;
            bool xValid = false;
            bool yValid = false;
            char userX;
            //need to deal with out of range / null exception
            do
            {

                do
                {
                    if (IsNullOrEmpty(userInput))
                    {
                        userInput = PromptString(message).ToUpper();
                    }
                    else break;
                } while (IsNullOrEmpty(userInput));
                
                    userX = userInput[0];
               
           
                if ((int)userX - 64 > 0 &&
                     (int)userX - 64 < 11)
                {
                    xValid = true;
                }
                //change to tryparse
                if (userInput.Substring(1) == "1" || userInput.Substring(1) == "2" || userInput.Substring(1) == "3" ||
                    userInput.Substring(1) == "4"
                    || userInput.Substring(1) == "5" || userInput.Substring(1) == "6" || userInput.Substring(1) == "7" ||
                    userInput.Substring(1) == "8"
                    || userInput.Substring(1) == "9" || userInput.Substring(1) == "10")
                {
                    yValid = true;
                }
                if (xValid && yValid)
                {
                    valid = true;
                }
                else
                {
                    
                    message = "Invalid - Enter one letter A - J and one number 1 - 10 eg D7:    ";
                    userInput = PromptString(message).ToUpper();

                }


            } while (!valid);

            return userInput;
        }

        #endregion

        //links to requests
        public static Coordinate GetCoordinate(string userInput)
        {
            int x;
            int y;

            x = Convert.ToInt32(userInput[0]) - 64;
            y = int.Parse(userInput.Substring(1));

            return new Coordinate(x, y);
        }

        //links to requests
        public static ShipDirection GetDirection()
        {
            bool isValid = true;
            string userInput;
            do
            {
                userInput = PromptString("\n\nInput: 1 = up, 2 = down, 3 = left, 4 = right\t\t");
                //ifstatements
                if (userInput == "")
                {
                    userInput = PromptString("\nInput: 1 = up, 2 = down, 3 = left, 4 = right\t\t");
                    isValid = false;
                }
                switch (userInput)
                {
                    case "1":
                        isValid = true;
                        return ShipDirection.Up;
                    case "2":
                        isValid = true;
                        return ShipDirection.Down;
                    case "3":
                        isValid = true;
                        return ShipDirection.Left;
                    case "4":
                        isValid = true;
                        break;
                    default:
                        Display("Invalid Entry - Try Again");
                        break;
                }
            } while (!isValid);

            return ShipDirection.Right;

        }

        //game instructions
        public void StartMenu()
        {
            BattleShipLogo();
            Display("");
            Display("\n\t How to Play:");
            Display("\n\t Place your ships horizontally or vertically on the grid");
            Display("\n\t Enter the coordinates to fire against your opponents ship");
            Display("\n\t Hits will be marked 'H', Misses will be marked  'M'");
            Display("\n\t The first player to sink all of their opponents ships wins!");
            Display("\n\t Player One goes first");
            Display("\n\n\t Press Enter to begin!");

            Console.ReadLine();
            
        }

        public void BattleShipLogo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Display("============================================================================");
            Console.WriteLine();
            Display("                               BATTLESHIP");
            Console.WriteLine();
            Display("============================================================================");
        }
           
        //Set Up Player Info
        public void InitialSetUp()
        {

            for (int i = 0; i < 2; i++)
            {
                
                 //adding players to dictionary if empty
                if (!Players.ContainsKey(0) && !Players.ContainsKey(1))
                {
                    Players.Add(0, playerOne);
                    Players.Add(1, playerTwo);
                }

                //clearing board and ship history
                if (Players.ContainsKey(0) && Players.ContainsKey(1))
                {
                    Players[0].board= new Board();
                    Players[0].ShipHistory = new Dictionary<Coordinate, ShipPlacement>();
                    Players[1].board = new Board();
                    Players[1].ShipHistory = new Dictionary<Coordinate, ShipPlacement>();
                }

                //get player names
                do
                {
                    BattleShipLogo();
                    Players[i].Name = PromptString($"\n\n\tEnter Name for Player {i + 1}:    ");
                } while (IsNullOrEmpty(Players[i].Name));
            }


            //looping through each player for ship placement
                for (int i = 0; i < 2; i++)
            {
                
                BattleShipLogo();
                board.ShipBoard(Players[i].ShipHistory);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkCyan;

                Display($"\n{(Players[i].Name)}, follow the prompts to place your ships!\nPress Enter to begin!");
                Console.ReadLine();
               
                //loop to place individual ships
                for (int j = 0; j < 5; j++)
                {
                    bool validPlacement = false;
                    string userShipInput = "";
                    PlaceShipRequest placeShip = new PlaceShipRequest();
                    BattleShipLogo();
                    board.ShipBoard(Players[i].ShipHistory);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    //validates input
                    userShipInput = PromptCoordinate($"\n\n{Players[i].Name},  Enter a coordinate to place your {(ShipType)j}:\t");

                    do
                    {
                        
                        Console.Clear();
                        BattleShipLogo();
                        board.ShipBoard(Players[i].ShipHistory);
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        //turns valid input into coordinate obj
                        placeShip.Coordinate = GetCoordinate(userShipInput); 
                        placeShip.Direction = GetDirection();
                        placeShip.ShipType = (ShipType)j; 
                        
                        ShipPlacement response = Players[i].board.PlaceShip(placeShip);
                       
                        //validating placement
                        switch (response)
                        {
                            case ShipPlacement.NotEnoughSpace:
                                Display("Not Enough Space\n");
                                userShipInput = PromptCoordinate($"\nEnter a coordinate to place your {(ShipType)j}:\t\t");
                                Console.ReadLine();
                                break;
                            case ShipPlacement.Overlap:
                                Display("Overlaps\n");
                                userShipInput =
                                PromptCoordinate($"\nEnter a coordinate to place your {(ShipType) j}:\t\t");
                                Console.ReadLine();
                                break;
                            case ShipPlacement.Ok:
                                Console.Clear();
                                 validPlacement = true;
                                break;
                            default:
                                Display("Try Again");
                                userShipInput = PromptCoordinate($"\nEnter a coordinate to place your {(ShipType)j}:\t\t");
                                break;
                        }
                    } while (!validPlacement);
                    
                    Coordinate oldCoord = GetCoordinate(userShipInput);
                    int xcoord = oldCoord.XCoordinate - 1;
                    int ycoord = oldCoord.YCoordinate - 1;
                    Coordinate newCoord = new Coordinate(ycoord, xcoord);
                 
                    #region PlacingDestroyer
                    
                    if ((ShipType) j == ShipType.Destroyer)
                    {
                        for (int k = 0; k < 2; k++)
                        {
                            Players[i].ShipHistory[newCoord] = ShipPlacement.Ok;
                            
                            switch (placeShip.Direction)
                            {
                                case ShipDirection.Right:
                                    xcoord++;
                                    newCoord = new Coordinate(ycoord, xcoord);
                                    break;
                                case ShipDirection.Left:
                                    xcoord--;
                                    newCoord = new Coordinate(ycoord, xcoord);
                                    break;
                                case ShipDirection.Down:
                                    ycoord++;
                                    newCoord = new Coordinate(ycoord, xcoord);
                                    break;
                                case ShipDirection.Up:
                                    ycoord--;
                                    newCoord = new Coordinate(ycoord, xcoord);
                                    break;
                            }
                        }
                    }
                    
                    #endregion

                    #region PlacingSub

                    if ((ShipType) j == ShipType.Submarine)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            Players[i].ShipHistory[newCoord] = ShipPlacement.Ok;
                            
                            switch (placeShip.Direction)
                            {
                                case ShipDirection.Right:
                                    xcoord++;
                                    newCoord = new Coordinate(ycoord, xcoord);
                                    break;
                                case ShipDirection.Left:
                                    xcoord--;
                                    newCoord = new Coordinate(ycoord, xcoord);
                                    break;
                                case ShipDirection.Down:
                                    ycoord++;
                                    newCoord = new Coordinate(ycoord, xcoord);
                                    break;
                                case ShipDirection.Up:
                                    ycoord--;
                                    newCoord = new Coordinate(ycoord, xcoord);
                                    break;
                            }
                        }
                    }

                    #endregion

                    #region PlacingCruiser

                    if ((ShipType) j == ShipType.Cruiser)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            Players[i].ShipHistory[newCoord] = ShipPlacement.Ok;
                            
                            switch (placeShip.Direction)
                            {
                                case ShipDirection.Right:
                                    xcoord++;
                                    newCoord = new Coordinate(ycoord, xcoord);
                                    break;
                                case ShipDirection.Left:
                                    xcoord--;
                                    newCoord = new Coordinate(ycoord, xcoord);
                                    break;
                                case ShipDirection.Down:
                                    ycoord++;
                                    newCoord = new Coordinate(ycoord, xcoord);
                                    break;
                                case ShipDirection.Up:
                                    ycoord--;
                                    newCoord = new Coordinate(ycoord, xcoord);
                                    break;
                            }
                        }
                    }

                    #endregion

                    #region PlacingBattleShip

                    if ((ShipType) j == ShipType.Battleship)
                    {

                        for (int k = 0; k < 4; k++)
                        {
                            Players[i].ShipHistory[newCoord] = ShipPlacement.Ok;
                            
                            switch (placeShip.Direction)
                            {
                                case ShipDirection.Right:
                                    xcoord++;
                                    newCoord = new Coordinate(ycoord, xcoord);
                                    break;
                                case ShipDirection.Left:
                                    xcoord--;
                                    newCoord = new Coordinate(ycoord, xcoord);
                                    break;
                                case ShipDirection.Down:
                                    ycoord++;
                                    newCoord = new Coordinate(ycoord, xcoord);
                                    break;
                                case ShipDirection.Up:
                                    ycoord--;
                                    newCoord = new Coordinate(ycoord, xcoord);
                                    break;
                            }
                        }
                    }

                    #endregion

                    #region PlacingCarrier

                    if ((ShipType) j == ShipType.Carrier)
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            Players[i].ShipHistory[newCoord] = ShipPlacement.Ok;
                            
                            switch (placeShip.Direction)
                            {
                                case ShipDirection.Right:
                                    xcoord++;
                                    newCoord = new Coordinate(ycoord, xcoord);
                                    break;
                                case ShipDirection.Left:
                                    xcoord--;
                                    newCoord = new Coordinate(ycoord, xcoord);
                                    break;
                                case ShipDirection.Down:
                                    ycoord++;
                                    newCoord = new Coordinate(ycoord, xcoord);
                                    break;
                                case ShipDirection.Up:
                                    ycoord--;
                                    newCoord = new Coordinate(ycoord, xcoord);
                                    break;
                            }

                        }
                    }

                    #endregion


                    Console.Clear();
                    BattleShipLogo();
                    board.ShipBoard(Players[i].ShipHistory);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Display($"\n\n{(ShipType)j} Placed!\n");
                    Console.ReadLine();

                }
                Console.Clear();
                BattleShipLogo();
                Display($"\n\t*****{Players[i].Name}, you have successfully placed all of your ships!*****\n\n\tPress Enter to continue");
                Console.ReadLine();
            }

        }
        
        public void PlayGameLoop()
        {
            bool validFire = false;
            FireShotResponse pShot = new FireShotResponse();
            string pCoord = "";
            Player player;
            Player otherPlayer;

            do
            {
                
                for (int i = 0; i < 2; i++)
                {
                    player = Players[i];
                    if (i == 0)
                    {
                        otherPlayer = playerTwo;
                    }
                    else
                    {
                        otherPlayer = playerOne;
                    }
                    Console.Clear();
                    BattleShipLogo();
                    board.GameBoard(otherPlayer.board.ShotHistory);
                    do
                    {
                        //prompt for coordinate && validate
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        pCoord = PromptCoordinate($"\n{player.Name}, Enter a coordinate for a shot: ");
                        //validate coordinate
                        Coordinate validPCoord = GetCoordinate(pCoord);
                        //call fireshot method
                        pShot = otherPlayer.board.FireShot(validPCoord);

                        switch (pShot.ShotStatus)
                        {
                            case ShotStatus.Invalid:
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                Display("Invalid Entry - Try Again:   ");
                                validFire = false;
                                break;
                            case ShotStatus.Duplicate:
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                Display("You already fired here - Try Again: ");
                                validFire = false;
                                break;
                            case ShotStatus.Miss:
                                otherPlayer.board.ShotHistory[validPCoord] = ShotHistory.Miss;
                                SuccessfulShot(otherPlayer, ConsoleColor.Yellow, "\nMiss!");
                                validFire = true;
                                break;
                            case ShotStatus.Hit:
                                otherPlayer.board.ShotHistory[validPCoord] = ShotHistory.Hit;
                                SuccessfulShot(otherPlayer, ConsoleColor.Red, "\nHit!");
                                validFire = true;
                                break;
                            case ShotStatus.HitAndSunk:
                                otherPlayer.board.ShotHistory[validPCoord] = ShotHistory.Hit;
                                SuccessfulShot(otherPlayer, ConsoleColor.Green, $"\n{pShot.ShipImpacted} - Hit and Sunk!");
                                validFire = true;
                                break;
                            case ShotStatus.Victory:
                                otherPlayer.board.ShotHistory[validPCoord] = ShotHistory.Hit;
                                validFire = true;
                                VictoryMessage(player);
                                board.GameBoard(otherPlayer.board.ShotHistory);
                                Console.ReadLine();

                                break;
                            default:
                                Display("~");
                                break;
                        }
                    } while (!validFire);
                    
                    if (pShot.ShotStatus == ShotStatus.Victory)
                    {
                        break;
                    } 

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Display("============================================================");
                    Console.WriteLine();
                    Display($"\tEnd {player.Name}'s Turn");
                    Console.WriteLine();
                    Display("============================================================");
                    Console.WriteLine();
                    Display($"\tPress Enter to begin {otherPlayer.Name}'s turn: ");
                    Console.WriteLine();
                    Display("============================================================");
                    Console.ReadLine();
                } 

            } while (pShot.ShotStatus != ShotStatus.Victory);
        }

        public bool KeepPlaying()
        {
            string playAgain = "";
            do
            {
                BattleShipLogo();
                playAgain = PromptString("\nWould You Like to Play Again? Y/N    ").ToUpper();

            } while (IsNullOrEmpty(playAgain));

            if (playAgain == "Y")
            {
                return true;
            }

            return false;

        }

        public void ThankYouForPlaying()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Display("========================================================================");
            Console.WriteLine();
            Display("                               BATTLESHIP");
            Console.WriteLine();
            Display("========================================================================");
            Console.WriteLine();
            Display("\tThank You for Playing!");
            Display("\tHit Enter to Exit");
            Console.ReadLine();
        }

        public void SuccessfulShot(Player other, ConsoleColor statusColor, string shotMessage)
        {
            Console.Clear();
            BattleShipLogo();
            board.GameBoard(other.board.ShotHistory);
            Console.ForegroundColor = statusColor;
            Display(shotMessage);
            Console.ReadLine();
        }

        public void VictoryMessage(Player winner)
        {
            Console.Clear();
            BattleShipLogo();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Display("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine();
            Display($"\t\t\t\t{winner.Name} Wins");
            Console.WriteLine();
            Display("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
    }
           
  }



