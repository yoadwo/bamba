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
            new Models.Action { Id = 0, Title = "במבה", AudioPath = "just-bamba.wma" },
            new Models.Action { Id = 1, Title = "לא", AudioPath = "just-no.wma" },
            new Models.Action { Id = 2, Title = "למטה", AudioPath = "just-down.wma" },
            new Models.Action { Id = 3, Title = "במבה לא", AudioPath = "bamba-no.wma" },
            new Models.Action { Id = 4, Title = "במבה למטה", AudioPath = "bamba-down.wma" },
            new Models.Action { Id = 5, Title = "כלבה טובה", AudioPath = "good-dog.wma" },
            new Models.Action { Id = 6, Title = "יופי במבה", AudioPath = "yofi-bamba.wma" },
            
        };

        public IEnumerable<Models.Action> Find(string title)
        {
            return actions.Where(action => action.Title.Contains(title))
                .Select(action => new Models.Action { Title = action.Title });
        }

        public Models.Action Get(int id)
        {
            return actions.FirstOrDefault(action => action.Id == id);
        }

        public IEnumerable<Models.Action> GetAll()
        {
            return actions;
        }
    }
}
