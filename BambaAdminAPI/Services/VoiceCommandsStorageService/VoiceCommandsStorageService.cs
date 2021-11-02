using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BambaAdminAPI.Services.VoiceCommandsStorageService
{
    public class VoiceCommandsStorageService : IVoiceCommandsStorageService
    {
        private static readonly Models.VoiceCommand[] actions = new[]
        {
            new Models.VoiceCommand { Id = 0, Title = "במבה", AudioPath = "just-bamba.wma" },
            new Models.VoiceCommand { Id = 1, Title = "לא", AudioPath = "just-no.wma" },
            new Models.VoiceCommand { Id = 2, Title = "למטה", AudioPath = "just-down.wma" },
            new Models.VoiceCommand { Id = 3, Title = "במבה לא", AudioPath = "bamba-no.wma" },
            new Models.VoiceCommand { Id = 4, Title = "במבה למטה", AudioPath = "bamba-down.wma" },
            new Models.VoiceCommand { Id = 5, Title = "כלבה טובה", AudioPath = "good-dog.wma" },
            new Models.VoiceCommand { Id = 6, Title = "יופי במבה", AudioPath = "yofi-bamba.wma" },
            
        };

        public IEnumerable<Models.VoiceCommand> Find(string title)
        {
            return actions.Where(action => action.Title.Contains(title))
                .Select(action => new Models.VoiceCommand { Title = action.Title });
        }

        public Models.VoiceCommand Get(int id)
        {
            return actions.FirstOrDefault(action => action.Id == id);
        }

        public IEnumerable<Models.VoiceCommand> GetAll()
        {
            return actions;
        }
    }
}
