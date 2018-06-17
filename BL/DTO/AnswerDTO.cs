using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Domain;

namespace BL.DTO
{
    public class AnswerDTO
    {
        public int AnswerId { get; set; }
        [MaxLength(2000)]
        public string Text { get; set; }
        [Required]
        public bool IsCorrect { get; set; }
        [Required]
        public virtual QuestionDTO Question { get; set; }
        public int QuestionId { get; set; }

        public static AnswerDTO CreateFromDomain(Answer a)
        {
            if (a == null) return null;

            return new AnswerDTO()
            {
                AnswerId = a.AnswerId,
                Text = a.Text,
                IsCorrect = a.IsCorrect,
                QuestionId = a.QuestionId
            };
        }
    }
}
