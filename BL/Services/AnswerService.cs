using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BL.DTO;
using BL.Factories;
using DAL.App.EF;
using DAL.App.Interfaces;
using Domain;

namespace BL.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IAppUnitOfWork _uow;
        private readonly IAnswerFactory _answerFactory;

        public AnswerService(IAppUnitOfWork uow, IAnswerFactory answerFactory)
        {
            _uow = uow;
            _answerFactory = answerFactory;
        }

        public IEnumerable<AnswerDTO> SearchByText(string text)
        {
            if (String.IsNullOrEmpty(text)) return null;

            return _uow.Answers.All().Where(x => x.Text.Contains(text))
                .Select(ans => _answerFactory.Create(ans)).ToList();
        }

        public IEnumerable<AnswerDTO> GetAllByQuestionId(int qid)
        {
            return _uow.Answers.All().Where(a => a.QuestionId == qid)
                .Select(ans => _answerFactory.Create(ans));
        }

        public AnswerDTO AddNewAnswer(AnswerDTO adto)
        {
            var newAnswer = _answerFactory.Create(adto);
            _uow.Answers.Add(newAnswer);
            _uow.SaveChanges();

            return _answerFactory.Create(newAnswer);
        }
    }
}
