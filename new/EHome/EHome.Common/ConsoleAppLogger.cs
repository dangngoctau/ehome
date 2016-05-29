using System;

namespace EHome.Common
{
    public class ConsoleAppLogger : IAppLogger
    {
        public void Error(string message, Exception ex)
        {
            Console.WriteLine("ERROR - " + message + "\n" + ex);
        }
    }
}
