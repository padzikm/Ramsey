using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
                if (CurrentRound >= RoundsCount)
                {
                    messageBoxService.ShowMessage("Brak Shura!", "");
                    return;
                }

                if (CurrentTurn == Turn.First)
                {
                    SelectedBoardItem.IsSelected = true;
                    CurrentTurn = Turn.Second;
                    SelectedBoardItem = null;
                }
                else
                {
                    ++CurrentRound;

                    if (!ColorBoardItemDictionary.Any())
                        for (var i = 0; i < ColorsCount; ++i)
                            ColorBoardItemDictionary[i] = new List<BoardItem>();

                    SelectedBoardItem.IsUsed = true;

                    var isSchur = new SchurSolverService().CheckSchur(ColorBoardItemDictionary, SelectedBoardItem);
                    if (isSchur)
                        messageBoxService.ShowMessage("Shur!", "");
                    else
                        CurrentTurn = Turn.First;
                }
            }
        }

        public void StartGame()
        {
            FirstPlayer = new FirstPlayer();
            if(ColorsCount == 1 && BoardSize >= 2)
                messageBoxService.ShowMessage("Shur na horyzoncie!", "");
            else if (ColorsCount == 2 && BoardSize >= 5)
                messageBoxService.ShowMessage("Shur na horyzoncie!", "");
            else if (ColorsCount == 3 && BoardSize >= 14)
                messageBoxService.ShowMessage("Shur na horyzoncie!", "");
            else if (ColorsCount == 4 && BoardSize >= 45)
                messageBoxService.ShowMessage("Shur na horyzoncie!", "");
        }

        public void OnColorsCountChanged()
        {
            var integerList = Enumerable.Range(0, ColorsCount);
            var newColors = integerList.Select(i => new Color { Index = i }).ToList();
            Colors = new ObservableCollection<Color>(newColors);
        }

        public void OnBoardSizeChanged()
        {
            var integerList = Enumerable.Range(1, BoardSize);
            var items = integerList.Select(i => new BoardItem { Value = i }).ToList();
            BoardItems = new ObservableCollection<BoardItem>(items);
        }
    }
}
