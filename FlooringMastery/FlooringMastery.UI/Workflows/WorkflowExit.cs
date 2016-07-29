using System;

namespace FlooringMastery.UI.Workflows
{
    public class WorkflowExit: IWorkflow
    {
        ConsoleIO ci = new ConsoleIO();

        public void Execute()
        {
            ci.Display("Goodbye!");
            Console.ReadLine();
        }
    }
}
