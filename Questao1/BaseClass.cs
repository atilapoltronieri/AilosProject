using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questao1
{
    public class BaseClass
    {
        public int Id {  get; set; }

        public BaseClass()
        {
            Id = new Random().Next();
        }
    }
}
