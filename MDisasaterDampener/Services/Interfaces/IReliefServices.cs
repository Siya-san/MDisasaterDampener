using MDisasaterDampener.Models;

namespace MDisasaterDampener.Services.Interfaces
{
    public interface IReliefServices
    {
        public void Insert(ReliefEffortViewModel reliefEffort);
        public List<ReliefEffortViewModel> Read();
        public ReliefEffortViewModel View(int id);

    }
}
