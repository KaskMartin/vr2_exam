using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BL.DTO;
using BL.Factories;
using DAL.App.EF;
using Domain;

namespace BL.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly EFAppUnitOfWork _uow;
        private readonly IQuestionFactory _questionFactory;

        public QuestionService(EFAppUnitOfWork uow, IQuestionFactory questionFactory)
        {
            _uow = uow;
            _questionFactory = questionFactory;
        }
        public IEnumerable<QuestionDTO> GetAllQuestions()
        {
            return _uow.Questions
                .All()
                .Select(q => _questionFactory.Create(q))
                .ToList();
        }

        public IEnumerable<QuestionDTO> SearchQuestionsByTitle(string searhTerm)
        {
            if (String.IsNullOrEmpty(searhTerm)) return null;

            return _uow.Questions
                .All()
                .Where(x => x.Title.Contains(searhTerm))
                .Select(que => _questionFactory.Create(que))
                .ToList();
        }

        public IEnumerable<QuestionDTO> SearchQuestionsByDescription(string searhTerm)
        {
            if (String.IsNullOrEmpty(searhTerm)) return null;

            return _uow.Questions
                .All()
                .Where(x => x.Description.Contains(searhTerm))
                .Select(que => _questionFactory.Create(que))
                .ToList();
        }

        public QuestionDTO GetQuestionById(int qid)
        {
            var q = _uow.Questions.Find(qid);
            if (q == null) return null;

            return _questionFactory.Create(q);
        }

        public QuestionDTO CreateNewQuestion(QuestionDTO qdto)
        {
            var newQuestion = _questionFactory.Create(qdto);
            _uow.Questions.Add(newQuestion);
            _uow.SaveChanges();

            return _questionFactory.Create(newQuestion);
        }

        public void UpdateQuestion(QuestionDTO updatedQuestion, int qid)
        {
            Question question = _uow.Questions.Find(qid);
            question.Title = updatedQuestion.Title;
            question.Description = updatedQuestion.Description;
            _uow.Questions.Update(question);
            _uow.SaveChanges();
        }

        public void DeleteQuestion(int qid)
        {
            Question question = _uow.Questions.Find(qid);
            _uow.Questions.Remove(question);
            _uow.SaveChanges();
        }

        public void MakePrivate(int qid)
        {
            Question question = _uow.Questions.Find(qid);
            if (!question.IsPublic)
            {
                return;
            }

            question.IsPublic = false;
            _uow.Questions.Update(question);
            _uow.SaveChanges();
        }

        public void MakePublic(int qid)
        {
            Question question = _uow.Questions.Find(qid);
            if (question.IsPublic)
            {
                return;
            }

            question.IsPublic = true;
            _uow.Questions.Update(question);
            _uow.SaveChanges();
        }
    }
}
