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
    public class Player
    {
        public string Name { get; set; }
        public Board board { get; set; }
        public Dictionary<Coordinate, ShipPlacement> ShipHistory;
        

        public Player()
        {
            
            board = new Board();
            ShipHistory = new Dictionary<Coordinate, ShipPlacement>();
            
        }
    }
}
