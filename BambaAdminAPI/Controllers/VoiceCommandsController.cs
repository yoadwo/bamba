using BambaAdminAPI.Services.VoiceCommandsStorageService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NetCoreAudio;

namespace BambaAdminAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoiceCommandsController : ControllerBase
    {
        private readonly IVoiceCommandsStorageService _voiceCommandsStorageService;
        private readonly ILogger<VoiceCommandsController> _logger;
        private const string BASE_AUDIO_PATH = "Assets\\Audio\\Arya\\";

        public VoiceCommandsController(
            IVoiceCommandsStorageService vcStgService,
            ILogger<VoiceCommandsController> logger)
        {
            _voiceCommandsStorageService = vcStgService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Models.VoiceCommand> GetAll()
        {
            _logger.LogInformation("Get All");
            return _voiceCommandsStorageService.GetAll();
        }

        [HttpPost]
        [Route("activate")]
        public async Task<IActionResult> Activate(int id)
        {
            _logger.LogInformation("Get by Id: {0}", id);
            var voiceCommandToExecute = _voiceCommandsStorageService.Get(id);
            if (voiceCommandToExecute == null)
            {
                _logger.LogInformation("No Voice Command found with id {0}", id);
                return NotFound(id);
            }
            _logger.LogInformation("Voice Command found with id {0}, audio path '{1}'",
                id, voiceCommandToExecute.AudioPath);

            var audioPath = BASE_AUDIO_PATH + voiceCommandToExecute.AudioPath;

            var player = new Player();
            await player.Play(audioPath);

            return Ok(new { id, status = true });
        }

        [HttpGet]
        [Route("preview")]
        public async Task<IActionResult> DownloadVoiceCommandPreview(int id)
        {
            var voiceCommandToPreview = _voiceCommandsStorageService.Get(id);
            if (voiceCommandToPreview == null)
            {
                _logger.LogInformation("No Voice Command found with id {0}", id);
                return NotFound(id);
            }
            _logger.LogInformation("Voice Command found with id {0}, audio path '{1}'",
                id, voiceCommandToPreview.AudioPath);

            var audioPath = BASE_AUDIO_PATH + voiceCommandToPreview.AudioPath;
            var memory = new MemoryStream();
            try
            {
                using (var stream = new FileStream(audioPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    await stream.CopyToAsync(memory);
                }
            }
            catch (FileNotFoundException ex)
            {
                _logger.LogError(ex, "Voice command path not found");
                return Problem("path not found, check logs for full path", null, 500, "הקובץ לא נמצא");
            }
            
            memory.Position = 0;
            var types = GetMimeTypes();
            var ext = Path.GetExtension(audioPath).ToLowerInvariant();
            return File(memory, types[ext], audioPath);
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
          {
              {".wav","audio/wav" },
              {".mp3","audio/mpeg" }
          };
        }
    }
}
