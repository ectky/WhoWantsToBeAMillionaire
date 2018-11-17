using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.App.Contracts
{
    public interface IQuestionFactory
    {
        IQuestion Create(string question, string rightAnswer, string closeAnswer,
            params string[] answers);
    }
}
