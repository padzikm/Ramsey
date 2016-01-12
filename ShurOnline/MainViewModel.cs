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
    public enum Turn
    {
        First,
        Second
    }
    [ImplementPropertyChanged]
    public class MainViewModel
    {
        private readonly IMessageBoxService messageBoxService;
        public MainViewModel()
        {
            ColorBoardItemDictionary = new Dictionary<int, List<BoardItem>>();
            messageBoxService = new MessageBoxService();
        }

        public int BoardSize { get; set; }
        public int ColorsCount { get; set; }
        public ObservableCollection<Color> Colors { get; set; }
        [DoNotCheckEquality]
        public BoardItem SelectedBoardItem { get; set; }
        
        public Color SelectedColor { get; set; }
        public ObservableCollection<BoardItem> BoardItems { get; set; }
        public int RoundsCount { get; set; }
        public int CurrentRound { get; set; }
        public Dictionary<int, List<BoardItem>> ColorBoardItemDictionary { get; set; }
        public FirstPlayer FirstPlayer { get; set; }
        public Turn CurrentTurn { get; set; }

        public void OnSelectedBoardItemChanged()
        {
            if (SelectedBoardItem != null)
            {
                if (CurrentTurn == Turn.First)
                {
                    SelectedBoardItem.IsSelected = true;
                    CurrentTurn = Turn.Second;
                    SelectedBoardItem = null;
                }
                else
                {
                    SelectedBoardItem.IsUsed = true;
                    if (SelectedColor != null)
                        SelectedBoardItem.Color = SelectedColor.Index;
                    if (ColorBoardItemDictionary.ContainsKey(SelectedBoardItem.Color))
                        ColorBoardItemDictionary[SelectedBoardItem.Color].Add(SelectedBoardItem);
                    else
                    {
                        var sameColorList = new List<BoardItem> { SelectedBoardItem };
                        ColorBoardItemDictionary.Add(SelectedBoardItem.Color, sameColorList);
                    }
                    var isSchur = new SchurSolverService().CheckSchur(ColorBoardItemDictionary);
                    if (isSchur)
                        messageBoxService.ShowMessage("Schur!", "");
                    CurrentTurn = Turn.First;
                }

            }
        }

        public void StartGame()
        {
            FirstPlayer = new FirstPlayer();
            
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
}
