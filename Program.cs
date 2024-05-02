using System;

namespace Log
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Logger logger = Logger.Instance;

            // Опции 
            logger.level = Logger.Level.TRACE;
            logger.Writemode = Logger.Mode.CONSOLE_FILE;
            logger.SeparateLogging = true;
            logger.NewFile = true;

            //Запись логов в Log\bin\Debug

            logger.LOGD("Debug Hello");
            logger.LOGI("Info Hello");
            logger.LOGW("Warn Hello");
            logger.LOGE("Error Hello");

            Console.ReadKey();
        }
        
    }
}
