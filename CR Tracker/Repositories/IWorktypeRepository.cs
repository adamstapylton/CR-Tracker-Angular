using CR_Tracker.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CR_Tracker.Repositories
{
    public interface IWorktypeRepository
    {
        Task<IEnumerable<Worktype>> GetWorktypesAsync();
    }
}
