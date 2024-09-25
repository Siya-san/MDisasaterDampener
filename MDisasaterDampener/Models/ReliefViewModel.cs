namespace MDisasaterDampener.Models
{
    public class ReliefViewModel
    {
        public List<ReliefEffortViewModel> reliefEfforts { get; set; }
        public ReliefViewModel()
        {
            reliefEfforts = new List<ReliefEffortViewModel>();
        }
    }
}
