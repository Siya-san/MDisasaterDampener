namespace MDisasaterDampener.Models
{
    public class ReliefEffortViewModel
    {//CREATE TABLE RELIEF_EFFORT (RE_Id int IDENTITY(1,1) PRIMARY KEY, Summary VARCHAR(25), Description VARCHAR(50));
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

    }
}
