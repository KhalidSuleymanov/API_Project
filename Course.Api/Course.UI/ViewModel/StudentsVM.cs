namespace Course.UI.ViewModel
{
    public class StudentsVM
    {
        public List<StudentGetVM> Students { get; set; }
    }
    public class StudentGetVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public decimal Point { get; set; }
        public StudentGroup Group { get; set; }
    }
    public class StudentGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
