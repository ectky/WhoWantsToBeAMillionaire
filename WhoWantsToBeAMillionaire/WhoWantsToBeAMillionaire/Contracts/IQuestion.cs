using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.App.Contracts
{
    public interface IQuestion
    {
        string QuestionText { get; }

        string RightAnswer { get; }

        string CloseAnswer { get; }

        string[] Answers { get; }

        void Shuffle();

        bool IsRightAnswer(string text);

        bool IsCloseAnswer(string text);
    }
}
