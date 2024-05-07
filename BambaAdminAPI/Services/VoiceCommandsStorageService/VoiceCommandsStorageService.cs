using BambaAdminAPI.Config;
using BambaAdminAPI.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BambaAdminAPI.Services.VoiceCommandsStorageService
{
    public class VoiceCommandsStorageService : IVoiceCommandsStorageService
    {
        private readonly ILogger<VoiceCommandsStorageService> _logger;
        private readonly ApplicationConfig _appConfig;

        private readonly IEnumerable<VoiceCommand> voiceCommands;

        private string[] EXTS = new string[]{ "*.mp3", "*.wav" };

        public VoiceCommandsStorageService(
            ILogger<VoiceCommandsStorageService> logger,
            IOptions<ApplicationConfig> appConfigOptions)
        {
            _logger = logger;
            _appConfig = appConfigOptions.Value;

            string audioDirFullPath = Path.Join("Assets", "Audio", _appConfig.AudioSubdir);
            var audioFilesPaths = EXTS.SelectMany(ext => Directory.EnumerateFiles(audioDirFullPath, ext));

            voiceCommands = audioFilesPaths.Select((path, index) => new VoiceCommand
            {
                Id = index,
                Title = Path.GetFileNameWithoutExtension(path),
                AudioPath = path
            });
        }

        public IEnumerable<VoiceCommand> Find(string title)
        {
            return voiceCommands.Where(action => action.Title.Contains(title))
                .Select(action => new VoiceCommand { Title = action.Title });
        }

        public VoiceCommand Get(int id)
        {
            return voiceCommands.FirstOrDefault(action => action.Id == id);
        }

        public IEnumerable<VoiceCommand> GetAll()
        {
            _logger.LogInformation("Total voice commands available: {0}", voiceCommands.Count());
            return voiceCommands;
        }
    }
}
