using CompanyManagement.Models;
using System.Collections.Generic;

namespace CompanyManagement.Database.Interfaces
{
    public interface ITimeKeepingDao
    {
        void Add(TimeKeeping timeKeeping);
        void Delete(string taskID);
        void Update(TimeKeeping timeKeeping);
        TimeKeeping SearchByID(string timeKeepingID);
        List<TimeKeeping> GetAll();
    }
}