using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.App.Contracts;
using Game.App.Entities;

namespace Game.App.Factories
{
    public class QuestionFactory : IQuestionFactory
    {
        public IQuestion Create(string questionText, string rightAnswer, string closeAnswer,
            params string[] answers)
        {
            IQuestion question = new Question(questionText, rightAnswer, closeAnswer, answers);

            return question;
        }
    }
}
