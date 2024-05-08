using System;

using Log;


namespace logger
{
    public class Program 
    {
        static void Main(string[] args) // Тестовая программа 
        {
            Logger logger = Logger.Instance; // Создания синглтон экземпляра

            // Опиции
            logger.level = Logger.Level.ERROR;
            logger.Writemode = Logger.Mode.CONSOLE_FILE;
            logger.SeparateLogging = true;
            logger.NewFile = true;

            // Запись логов в "название проекта"\bin\Debug
            logger.LOGD("Debug Hello");
            logger.LOGI("Info Hello");
            logger.LOGW("Warn Hello");
            logger.LOGE("Error Hello");

            Console.ReadKey();
        }
    }
}
