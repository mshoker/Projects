using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.Requests;


namespace BattleShip.UI
{
    public class DisplayPrompts
    {
        public static void Display(string message)
        {
            Console.Write(message);
        }

        public string PromptString(string message)
        {
            Display(message);
            return Console.ReadLine();
        }

        
/*
        //return valid coordinates for board
        public Coordinate PromptCoordinate(string message)
        {
            Display(message);
            string CoordinateString = Console.ReadLine().ToUpper();
            bool invalid = true;


            do
            {
                Display("Invalid Coodinate must be A-J, 1-10 eg B7");
                Display(message);
                CoordinateString = Console.ReadLine().ToUpper();
                //validity conditions
                if (CoordinateString.Length > 3)
                {
                    invalid = true;
                }
                if (CoordinateString.Length == 3 && CoordinateString.Substring(1) != "10")
                {
                    invalid = true;
                }

                string xCoord = CoordinateString.Substring(0, 1);
                string yCoord = CoordinateString.Substring(1);
                int YCoordinate;
                bool validY;

                //chacking that Y coordinates are int
                validY = int.TryParse(yCoord, out YCoordinate);

                if (!validY)
                {
                    invalid = true;
                }
                //Check Y is in Range

                if (YCoordinate < 1 || YCoordinate > 10)
                {
                    invalid = true;
                }

                YCoordinate--;

                //Assigning X Coordinate an Int Value and check Valid Entry
                int XCoordinate;

                char[] letterCoordinate = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };

                if (letterCoordinate.Contains(xCoord[0]))
                {
                    for (int i = 0; i < letterCoordinate.Length; i++)
                    {
                        if (xCoord[0] == letterCoordinate[i])
                        {
                            XCoordinate = i;
                        }

                    }
                }
                else
                {
                    invalid = true;
                }

                Coordinate obj = new Coordinate(XCoordinate, YCoordinate);
                return obj;

            } while (invalid);


            //create coordinate object with properties x and y, then we can return two values
        } */
    }
}
