using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.App.Contracts;

namespace Game.App.Entities
{
    public class Question : IQuestion
    {
        public Question(string question, string rightAnswer, string closeAnswer,
            string[] answers)
        {
            this.QuestionText = question;
            this.RightAnswer = rightAnswer;
            this.CloseAnswer = closeAnswer;
            this.Answers = answers;
        }

        public string QuestionText { get; }

        public string RightAnswer { get; }

        public string[] Answers {get; private set; }

        public string CloseAnswer { get; }

        public void Shuffle()
        {
            var rnd = new Random();
            this.Answers = this.Answers.OrderBy(item => rnd.Next()).ToArray();
        }

        public bool IsRightAnswer(string text)
        {
            bool result = text.Equals(this.RightAnswer);

            return result;
        }

        public bool IsCloseAnswer(string text)
        {
            bool result = text.Equals(this.CloseAnswer);

            return result;
        }

    }
}
