﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BambaAdminAPI.Services.VoiceCommandsStorageService
{
    public class VoiceCommandsStorageService : IVoiceCommandsStorageService
    {
        private static readonly Models.VoiceCommand[] voiceCommands = new[]
        {
            new Models.VoiceCommand { Id = 0, Title = "אריאה", AudioPath = "just-arya.mp3" },
            new Models.VoiceCommand { Id = 1, Title = "לא", AudioPath = "just-no.mp3" },
            new Models.VoiceCommand { Id = 2, Title = "למטה", AudioPath = "just-down.mp3" },
            new Models.VoiceCommand { Id = 3, Title = "אריאה לא", AudioPath = "arya-no.mp3" },
            new Models.VoiceCommand { Id = 4, Title = "אריאה למטה", AudioPath = "arya-down.mp3" },
            new Models.VoiceCommand { Id = 5, Title = "כלבה טובה", AudioPath = "good-dog.mp3" },
            new Models.VoiceCommand { Id = 6, Title = "יופי אריאה", AudioPath = "good-arya.mp3" }

        };

        private readonly ILogger<VoiceCommandsStorageService> _logger;

        public VoiceCommandsStorageService(ILogger<VoiceCommandsStorageService> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Models.VoiceCommand> Find(string title)
        {
            return voiceCommands.Where(action => action.Title.Contains(title))
                .Select(action => new Models.VoiceCommand { Title = action.Title });
        }

        public Models.VoiceCommand Get(int id)
        {
            return voiceCommands.FirstOrDefault(action => action.Id == id);
        }

        public IEnumerable<Models.VoiceCommand> GetAll()
        {
            _logger.LogInformation("Total voice commands available: {0}", voiceCommands.Length);
            return voiceCommands;
        }
    }
}
