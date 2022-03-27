using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KonusarakOgrenQuiz.Models
{
    public class Container
    {
        public IEnumerable<Quiz>quiz { get; set; }
        public IEnumerable<Wired> wired { get; set; }

        public IEnumerable<Questions> questions { get; set; }
    }
}
