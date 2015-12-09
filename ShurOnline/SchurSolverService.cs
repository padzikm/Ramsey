using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShurOnline
{
    public class SchurSolverService
    {
        public bool CheckSchur(Dictionary<int, List<BoardItem>> dictionary)
        {
            foreach (var boardList in dictionary)
            {
                if (boardList.Value.Count < 3) continue;
                var combinations = boardList.Value.Combinations(3);
                foreach (var combination in combinations)
                {
                    var combinationList = combination.OrderBy(b => b.Value).ToList();
                    if (combinationList[0].Value + combinationList[1].Value == combinationList[2].Value)
                        return true;
                }
            }
            return false;
        }
    }
}
