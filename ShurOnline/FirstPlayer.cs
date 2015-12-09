using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShurOnline
{
    public class FirstPlayer
    {
        private static readonly Random random = new Random();
        public BoardItem ChooseBoardItem(List<BoardItem> boardItems)
        {
            return boardItems[random.Next(boardItems.Count)];
        }
    }
}
