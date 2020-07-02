namespace ReviewApp.Domain.Views
{
    public class DeleteView
    {
        public DeleteView(long id, string name)
        {
            Id = id;
            Name = name;
        }
        public long Id { get; }
        public string Name { get; }
    }
}