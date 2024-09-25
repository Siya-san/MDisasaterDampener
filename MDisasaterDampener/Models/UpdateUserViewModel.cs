namespace MDisasaterDampener.Models
{
    public class UpdateUserViewModel
    {
        List<UserViewModel> users { get; set; }
        public UpdateUserViewModel()
        {
            users = new List<UserViewModel>();
        }
    }
}
