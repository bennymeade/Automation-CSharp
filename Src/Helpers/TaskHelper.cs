using System;
using System.Threading;

namespace FordTA2_AAT.Helpers
{
    public class TaskHelper
    {
        public static void ExecuteTask(Action action, int retry = 3, Action onFail = null)
        {
            bool success = false;
            do
            {
                try
                {
                    retry--;
                    action();
                    success = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Attempt failed. Retries left: {0}. Error message: '{1}'", retry, e.Message);

                    if (retry == 0 && onFail != null)
                    {
                        try
                        {
                            onFail();
                            return;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            throw;
                        }
                    }

                    if (retry == 0)
                    {
                        Console.WriteLine(e.ToString());
                        throw;
                    }
                    Thread.Sleep(1000);
                }

            }
            while (!success && retry >= 0);
        }
    }
}
