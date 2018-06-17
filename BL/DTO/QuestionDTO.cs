using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Domain;

namespace BL.DTO
{
    public class QuestionDTO
    {
        public int QuestionId { get; set; }
        [MaxLength(2000)]
        public string Description { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        public List<AnswerDTO> Answers { get; set; } = new List<AnswerDTO>();
        public bool IsPublic { get; set; } = false;

        public static QuestionDTO CreateFromDomain(Question q)
        {
            if (q == null) return null;

            return new QuestionDTO()
            {
                QuestionId = q.QuestionId,
                Description = q.Description,
                Title = q.Title,
                IsPublic = q.IsPublic
            };
        }

        public static QuestionDTO CreateFromDomainWithAnswers(Question q)
        {
            var question = CreateFromDomain(q);
            if (question == null) return null;

            question.Answers = q?.Answers?.Select(AnswerDTO.CreateFromDomain).ToList();
            return question;
        }
    }
}
