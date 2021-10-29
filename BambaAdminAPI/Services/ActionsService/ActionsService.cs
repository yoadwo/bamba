using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BambaAdminAPI.Services.ActionsService
{
    public class ActionsService : IActionsService
    {
        private static readonly Models.Action[] actions = new[]
        {
            new Models.Action { Title = "במבה", AudioPath = "Assets\\Audio\\just-bamba.wma" },
            new Models.Action { Title = "לא", AudioPath = "Assets\\Audio\\just-no.wma" },
            new Models.Action { Title = "למטה", AudioPath = "Assets\\Audio\\just-down.wma" },
            new Models.Action { Title = "במבה לא", AudioPath = "Assets\\Audio\\bamba-no.wma" },
            new Models.Action { Title = "במבה למטה", AudioPath = "Assets\\Audio\\bamba-down.wma" }
            
        };

        public IEnumerable<Models.Action> Find(string title)
        {
            return actions.Where(action => action.Title.Contains(title))
                .Select(action => new Models.Action { Title = action.Title });
        }

        public Models.Action Get(string title)
        {
            return actions.FirstOrDefault(action => action.Title.Contains(title));
        }

        public IEnumerable<Models.Action> GetAll()
        {
            return actions;
        }
    }
}
