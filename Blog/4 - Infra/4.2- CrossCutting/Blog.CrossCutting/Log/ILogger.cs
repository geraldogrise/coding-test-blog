using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CrossCutting.Log
{
    public interface ILogger
    {
        void writeLog(Exception exception);
    }
}
