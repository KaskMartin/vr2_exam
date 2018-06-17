using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Answer
    {
        public int AnswerId { get; set; }
        [MaxLength(2000)]
        public string Text { get; set; }
        [Required]
        public bool IsCorrect {get; set; }
        [Required]
        public virtual Question Question { get; set; }
        public int QuestionId { get; set; }
    }
}
