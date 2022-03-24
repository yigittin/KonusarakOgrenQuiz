using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KonusarakOgrenQuiz.Models
{
    public class Quiz
    {
        public int id { get; set; }
        public Wired wired { get; set; }
        public int wiredId { get; set; }      

    }
}
