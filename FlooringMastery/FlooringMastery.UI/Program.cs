using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.UI.Workflows;

namespace FlooringMastery.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            
            IWorkflow main = new WorkflowMainMenu();
            
                main.Execute();
            
        }
    }
}
