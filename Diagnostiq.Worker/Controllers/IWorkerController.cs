using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagnostiq.Worker.Controllers
{
    public interface IWorkerController
    {
        void Start();
        void Stop();
    }
}
