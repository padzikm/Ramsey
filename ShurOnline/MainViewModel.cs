using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PropertyChanged;

namespace ShurOnline
{
    [ImplementPropertyChanged]
    public class MainViewModel
    {
        public int BoardSize { get; set; }
        public int ColorsCount { get; set; }
        public ObservableCollection<Color> Colors { get; set; }
        public BoardItem SelectedBoardItem { get; set; }
        public Color SelectedColor { get; set; }
        public ObservableCollection<BoardItem> BoardItems { get; set; }
        public int RoundsCount { get; set; }

        public void OnSelectedBoardItemChanged()
        {
            if (SelectedBoardItem != null)
            {
                SelectedBoardItem.IsUsed = true;
                if (SelectedColor != null)
                    SelectedBoardItem.Color = SelectedColor.Index;
            }
        }

        public void StartGame()
        {
            
        }

        public void OnColorsCountChanged()
        {
            var integerList = Enumerable.Range(0, ColorsCount);
            var newColors = integerList.Select(i => new Color { Index = i }).ToList();
            Colors = new ObservableCollection<Color>(newColors);
        }

        public void OnBoardSizeChanged()
        {
            var integerList = Enumerable.Range(0, BoardSize);
            var items = integerList.Select(i => new BoardItem { Value = i }).ToList();
            BoardItems = new ObservableCollection<BoardItem>(items);
        }
    }
    [ImplementPropertyChanged]
    public class BoardItem
    {
        public int Value { get; set; }
        public int Color { get; set; }
        public bool IsUsed { get; set; }

    }

    public class Color
    {
        public int Index { get; set; }
    }
}
