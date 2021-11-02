using BambaAdminAPI.Services.VoiceCommandsStorageService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BambaAdminAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoiceCommandsController : ControllerBase
    {
        private readonly IVoiceCommandsStorageService _voiceCommandsStorageService;
        private readonly ILogger<VoiceCommandsController> _logger;

        public VoiceCommandsController(
            IVoiceCommandsStorageService vcStgService,
            ILogger<VoiceCommandsController> logger)
        {
            _voiceCommandsStorageService = vcStgService;
            _logger = logger;
        }

        [HttpGet]
        [Route("voiceCommand")]
        public IEnumerable<Models.VoiceCommand> GetAll()
        {
            return _voiceCommandsStorageService.GetAll();
        }

        [HttpPost]
        [Route("activate")]
        public void Activate(int id)
        {
            var actionToExecute = _voiceCommandsStorageService.Get(id);
            if (actionToExecute == null)
            {
                return;
            }
            var audioPath = "Assets\\Audio\\";
            using (var audioFile = new AudioFileReader(audioPath + actionToExecute.AudioPath))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
    }
}
