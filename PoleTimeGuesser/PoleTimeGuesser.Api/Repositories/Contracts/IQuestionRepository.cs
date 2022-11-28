﻿using PoleTimeGuesser.Library.Models;

namespace PoleTimeGuesser.Api.Repositories.Contracts
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<QuestionModel>> GetQuestions();
    }
}