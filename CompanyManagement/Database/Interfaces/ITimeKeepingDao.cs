using CompanyManagement.Models;

namespace CompanyManagement.Database.Interfaces
{
    public interface ITimeKeepingDao
    {
        void Add(TimeKeeping timeKeeping);
        void Delete(TimeKeeping timeKeeping);
        void Update(TimeKeeping timeKeeping);
        TimeKeeping SearchByID(string timeKeepingID);
    }
}