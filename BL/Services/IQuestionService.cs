using System.Collections.Generic;
using BL.DTO;

namespace BL.Services
{
    public interface IQuestionService
    {
        IEnumerable<QuestionDTO> GetAllQuestions();
        IEnumerable<QuestionDTO> SearchQuestionsByTitle(string searhTerm);
        IEnumerable<QuestionDTO> SearchQuestionsByDescription(string searhTerm);
        QuestionDTO GetQuestionById(int qid);
        void UpdateQuestion(QuestionDTO qdto, int qid);
        void DeleteQuestion(int qid);
        void MakePrivate(int qid);
        void MakePublic(int qid);
    }
}