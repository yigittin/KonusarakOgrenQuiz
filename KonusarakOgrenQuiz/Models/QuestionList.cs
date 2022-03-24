using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KonusarakOgrenQuiz.Models
{
    public class QuestionList
    {
        public int id { get; set; }
        public List<Questions> questions { get; set; }

        public Quiz quiz { get; set; }
        public int quizId { get; set; }
    }
}
