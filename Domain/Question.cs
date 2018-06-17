using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Question
    {
        public int QuestionId { get; set; }
        [MaxLength(2000)]
        public string Description { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        public List<Answer> Answers { get; set; } = new List<Answer>();
        public bool IsPublic { get; set; } = false;
    }
}
