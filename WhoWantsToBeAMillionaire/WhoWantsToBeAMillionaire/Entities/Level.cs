using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.App.Contracts;

namespace Game.App.Entities
{
    public class Level : ILevel
    {
        private int currentCount;

        public Level()
        {
            this.Questions = new List<IQuestion>();
            this.currentCount = -1;
        }

        public Level(params IQuestion[] questions) : this()
        {
            this.Questions = questions;
        }

        public IList<IQuestion> Questions { get; }

        public int CurrentCount => this.currentCount;

        public IQuestion Current => !IsEndOfQuestions? this.Questions[currentCount] : null;

        public bool IsEndOfQuestions => this.currentCount == Questions.Count;

        public void AddQuestion(IQuestion question)
        {
            this.Questions.Add(question);
        }

        public void ChangeQuestion()
        {
            if (this.currentCount != this.Questions.Count)
            {
                this.currentCount++;
            }
        }
    }
}
