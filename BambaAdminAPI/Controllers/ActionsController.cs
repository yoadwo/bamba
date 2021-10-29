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
        [Route("get")]
        public IEnumerable<Models.Action> Search(string title)
        {
            return _actionsService.Find(title);
        }

        [HttpGet]
        [Route("getAll")]
        public IEnumerable<Models.Action> GetAll()
        {
            return _actionsService.GetAll();
        }

        [HttpPost]
        [Route("launch")]
        public void Launch(string title)
        {
            var actionToExecute = _actionsService.Get(title);
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
