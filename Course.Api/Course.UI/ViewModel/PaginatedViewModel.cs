namespace Course.UI.ViewModel
{
    public class PaginatedViewModel<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
    }
}
