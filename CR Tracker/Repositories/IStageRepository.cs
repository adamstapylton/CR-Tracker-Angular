using CR_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CR_Tracker.Repositories
{
    public interface IStageRepository
    {
        Task<IEnumerable<Stage>> GetStages();
        Task<Stage> GetStageById(int id);
    }
}
