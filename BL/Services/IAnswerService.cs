using System.Collections.Generic;
using BL.DTO;

namespace BL.Services
{
    public interface IAnswerService
    {
        IEnumerable<AnswerDTO> SearchByText(string text);

        IEnumerable<AnswerDTO> GetAllByQuestionId(int qid);

        AnswerDTO AddNewAnswer(AnswerDTO adto);
    }
}