using BL.DTO;
using Domain;

namespace BL.Factories
{
    public interface IQuestionFactory
    {
        QuestionDTO Create(Question q);
        Question Create(QuestionDTO qdto);
    }

    public class QuestionFactory : IQuestionFactory
    {
        public QuestionDTO Create(Question q)
        {
            return QuestionDTO.CreateFromDomain(q);
        }

        public Question Create(QuestionDTO qdto)
        {
            return new Question()
            {
                Description = qdto.Description,
                IsPublic = qdto.IsPublic,
                QuestionId = qdto.QuestionId,
                Title = qdto.Title
            };
        }
    }
}