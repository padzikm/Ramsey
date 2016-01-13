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
                selectedItem.Color = -1;
                position.Value.Add(selectedItem);
                var combinations = position.Value.Combinations(3);

                foreach (var combination in combinations)
                {
                    var combinationList = combination.OrderBy(b => b.Value).ToList();
                    if (combinationList[0].Value + combinationList[1].Value == combinationList[2].Value)
                    {
                        selectedItem.Color = -2;
                        break;
                    }
                }
                if (selectedItem.Color == -2)
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
