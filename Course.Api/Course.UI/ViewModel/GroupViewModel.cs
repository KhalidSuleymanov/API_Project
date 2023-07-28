namespace Course.UI.ViewModel
{
    public class GroupViewModel
    {
        public List<GroupViewModelItem> Groups { get; set; }
    }
    public class GroupViewModelItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
