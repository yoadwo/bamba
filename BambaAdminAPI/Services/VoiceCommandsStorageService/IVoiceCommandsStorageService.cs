using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BambaAdminAPI.Services.VoiceCommandsStorageService
{
    public interface IVoiceCommandsStorageService
    {
        IEnumerable<Models.VoiceCommand> Find(string title);
        Models.VoiceCommand Get(int id);
        IEnumerable<Models.VoiceCommand> GetAll();
    }
}
