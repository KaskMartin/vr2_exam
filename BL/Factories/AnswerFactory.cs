using System;
using System.Collections.Generic;
using System.Text;
using BL.DTO;
using Domain;

namespace BL.Factories
{
    public interface IAnswerFactory
    {
        AnswerDTO Create(Answer a);
        Answer Create(AnswerDTO adto);
    }

    public class AnswerFactory : IAnswerFactory
    {
        public AnswerDTO Create(Answer a)
        {
            return AnswerDTO.createFromDomain(a);
        }

        public Answer Create(AnswerDTO adto)
        {
            return new Answer()
            {
                AnswerId = adto.AnswerId,
                IsCorrect = adto.IsCorrect,
                QuestionId = adto.QuestionId,
                Text = adto.Text
            };
        }
    }
}
