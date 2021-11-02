using BambaAdminAPI.Services.ActionsService;
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
        private readonly IActionsService _actionsService;
        private readonly ILogger<ActionsController> _logger;

        public ActionsController(
            IActionsService actionsService,
            ILogger<ActionsController> logger)
        {
            _actionsService = actionsService;
            _logger = logger;
        }

        [HttpGet]
        [Route("getAll")]
        public IEnumerable<Models.Action> GetAll()
        {
            return _actionsService.GetAll();
        }

        [HttpPost]
        [Route("launch")]
        public void Launch(int id)
        {
            var actionToExecute = _actionsService.Get(id);
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
