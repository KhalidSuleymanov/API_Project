namespace Course.UI.ViewModel
{
    public class ErrorVM
    {
        public string Message { get; set; }
        public List<ErrorVMItem> Errors { get; set; }
    }
    public class ErrorVMItem
    {
        public string Key { get; set; }
        public string ErrorMes { get; set; }
    }
}
