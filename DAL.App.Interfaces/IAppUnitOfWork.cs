using DAL.App.Interfaces.Repositories;
using DAL.Interfaces;

namespace DAL.App.Interfaces
{
    public interface IAppUnitOfWork : IUnitOfWork
    {
        IQuestionRepository Questions { get; }
        IAnswerRepository Answers { get; }
    }
}