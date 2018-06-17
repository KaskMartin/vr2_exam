using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.DTO;
using BL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VR2MKaskExam.Controllers.api
{
    [Produces("application/json")]
    [Route("api/Answers")]
    public class AnswersController : Controller
    {
        private readonly IAnswerService _answerService;

        public AnswersController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        // GET api/answers/find
        [HttpGet]
        [Route("find")]
        [ProducesResponseType(typeof(List<AnswerDTO>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        public IActionResult Find(string searchTerm)
        {
            IEnumerable<AnswerDTO> answers = new List<AnswerDTO>();
            if (searchTerm != null)
            {
                answers = _answerService.SearchByText(searchTerm);
            }

            if (!answers.Any())
            {
                return NotFound();
            }

            return Ok(answers);
        }
    }
}