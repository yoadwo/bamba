using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BambaAdminAPI.Services.ActionsService
{
    public interface IActionsService
    {
        IEnumerable<Models.Action> Find(string title);
        Models.Action Get(string title);
        IEnumerable<Models.Action> GetAll();
    }
}
