using System;

namespace Log
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            // Опции 
            Logger.level = Logger.Level.TRACE;
            Logger.Writemode = Logger.Mode.CONSOLE_FILE;
            Logger.NewFile = true;
            Logger.SeparateLogging = true;

            //Запись логов в Log\bin\Debug

            Logger.LOGD("Debug Hello");
            Logger.LOGI("Info Hello");
            Logger.LOGW("Warn Hello");
            Logger.LOGE("Error Hello");

            Console.ReadKey();
        }
        
    }
}
