using PropertyChanged;

namespace ShurOnline
{
    [ImplementPropertyChanged]
    public class BoardItem
    {
        public int Value { get; set; }
        public int Color { get; set; }
        public bool IsUsed { get; set; }
        public bool IsSelected { get; set; }

    }
}