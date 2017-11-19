using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailServer.UI
{
    interface IForm
    {
        void Start();
        void Pause();
        void Save();
    }
}
