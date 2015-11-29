using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diagnostiq.Worker.Controllers;
using Topshelf;

namespace Diagnostiq.Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>                                 
            {
                x.Service<IWorkerController>(s =>                        
                {
                    s.ConstructUsing(name => new CounterWorkerController(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5)));     
                    s.WhenStarted(tc => tc.Start());              
                    s.WhenStopped(tc => tc.Stop());               
                });
                x.RunAsLocalSystem();                            

                x.SetDescription("Collects local configurable performance counter data for the Diagnostiq engine");        
                x.SetDisplayName("Diagnostiq Worker");                       
                x.SetServiceName("DiagnostiqWorker");                       
            });
        }
    }
}
