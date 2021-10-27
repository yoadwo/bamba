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
    public class ActionsController : ControllerBase
    {
        private static readonly Models.Action[] actions = new[]
        {
            new Models.Action { Title = "Stop", AudioPath = "Assets\\Audio\\tada.wav" },
            new Models.Action { Title = "No", AudioPath = "Assets\\Audio\\tada.wav" },            
            new Models.Action { Title = "Place", AudioPath = "Assets\\Audio\\tada.wav" }            
        };

        private readonly ILogger<ActionsController> _logger;

        public ActionsController(ILogger<ActionsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("search")]
        public IEnumerable<Models.Action> Search(string title)
        {
            return actions.Where(action => action.Title.Contains(title))
                .Select(action => new Models.Action { Title = action.Title });
        }

        [HttpGet]
        [Route("get")]
        public IEnumerable<Models.Action> Get()
        {
            return actions;
        }

        [HttpPost]
        [Route("launch")]
        public void Launch(string title)
        {
            var actionToExecute = actions.FirstOrDefault(action => action.Title.Contains(title));
            if (actionToExecute == null)
            {
                return;
            }

            using (var audioFile = new AudioFileReader(actionToExecute.AudioPath))
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
