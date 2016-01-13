using System.Collections.Generic;
using System.Linq;

namespace ShurOnline
{
    public class SchurSolverService
    {
        public bool CheckSchur(Dictionary<int, List<BoardItem>> colorBoardItemDictionary, BoardItem selectedItem)
        {
            foreach (var position in colorBoardItemDictionary)
            {
                if (position.Value.Count < 2)
                {
                    selectedItem.Color = position.Key;
                    position.Value.Add(selectedItem);
                    return false;
                }
            }

            foreach (var position in colorBoardItemDictionary)
            {
                var found = true;
                position.Value.Add(selectedItem);
                var combinations = position.Value.Combinations(3);

                foreach (var combination in combinations)
                {
                    var combinationList = combination.OrderBy(b => b.Value).ToList();
                    if (combinationList[0].Value + combinationList[1].Value == combinationList[2].Value)
                    {
                        found = false;
                        break;
                    }
                }
                if (!found)
                    position.Value.Remove(selectedItem);
                else
                {
                    selectedItem.Color = position.Key;
                    return false;
                }
            }

            return true;
        }
    }
}
