using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.App.Entities;

namespace Game.App.Contracts
{
    public interface ILevel
    {
        IList<IQuestion> Questions { get; }

        IQuestion Current { get; }

        bool IsEndOfQuestions { get; }

        int CurrentCount { get; }

        void AddQuestion(IQuestion question);

        void ChangeQuestion();
    }
}
