using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miinaharava
{
    public class Tile : Button
    {
        public string position;
        public int adjacentMines;
        public bool isMine = false;
        public bool isFlagged = false;
    }
}
