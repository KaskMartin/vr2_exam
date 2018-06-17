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
    [Route("api/Questions")]
    public class QuestionsController : Controller
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        // GET: api/Questions
        [HttpGet]
        [ProducesResponseType(typeof(List<QuestionDTO>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        public IActionResult GetAll()
        {
            return Ok(_questionService.GetAllQuestions());
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(QuestionDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        public IActionResult Get(int id)
        {
            var q = _questionService.GetQuestionById(id);
            if (q == null) return NotFound();
            return Ok(q);
        }

        // POST: api/Questions
        [HttpPost]
        [ProducesResponseType(typeof(QuestionDTO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromBody]QuestionDTO question)
        {
            if (!ModelState.IsValid) return BadRequest();

            var p = _questionService.GetQuestionById(question.QuestionId);

            if (p != null)
            {
                return BadRequest("Such person already exists.");
            }

            var newQuestion = _questionService.CreateNewQuestion(question);
            
            return CreatedAtAction("Get", new { id = newQuestion.QuestionId }, newQuestion);
        }

        //PUT: api/Questions/5
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        public IActionResult Put(int id, [FromBody]QuestionDTO question)
        {
            if (!ModelState.IsValid) return BadRequest();
            var p = _questionService.GetQuestionById(id);

            if (p == null) return NotFound();
            if (p.IsPublic) return BadRequest("Public question cannot be modified");
            _questionService.UpdateQuestion(question, id);

            return NoContent();
        }

        //DELETE: api/Questions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Delete(int id)
        {
            var p = _questionService.GetQuestionById(id);
            if (p == null) return NotFound();

            _questionService.DeleteQuestion(id);
            return NoContent();
        }
    }
}