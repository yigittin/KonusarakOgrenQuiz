using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KonusarakOgrenQuiz.Models
{
    public class Questions
    {
        public int id { get; set; }
        public string Question { get; set; }
        public string answer1 { get; set; }
        public string answer2 { get; set; }
        public string answer3 { get; set; }
        public string answer4 { get; set; }
        public int trueAnswer { get; set; }

        public QuestionList questionList { get; set; }
        public int questionListId { get; set; }



    }
}
