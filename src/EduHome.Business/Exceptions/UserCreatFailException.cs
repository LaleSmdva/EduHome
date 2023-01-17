using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Business.Exceptions
{
    public class UserCreatFailException:Exception
    {
        public UserCreatFailException(string message):base(message)
        {

        }
    }
}
