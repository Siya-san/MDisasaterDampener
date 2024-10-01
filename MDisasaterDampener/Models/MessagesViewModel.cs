namespace MDisasaterDampener.Models
{
    public class MessagesViewModel
    {
        
        public int Id { get; set; }
     
        public string Body { get; set; }
        public VolunteerViewModel Vid { get; set; }
        public DateOnly DateSent { get; set; }
        public List<MessagesViewModel> Messages { get; set; }
        public MessagesViewModel()
        {
            Body = "Type Here...";
            Vid = new VolunteerViewModel();
            Messages = new List<MessagesViewModel>();
        }
    }
}
