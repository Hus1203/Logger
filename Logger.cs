using System;
using System.IO;
using System.Runtime.CompilerServices;


namespace Log
{
    public class Logger
    {
        /** Перечисление уровней логирования */
        public enum Level
        {
            TRACE, 
            DEBUG, 
            INFO,  
            WARN,  
            ERROR 
        }
        /** Перечисление режимов ведения логов*/
        public enum Mode
        {
            CONSOLE,
            FILE,
            CONSOLE_FILE
        }
    // Переменные

        /** приватное статическое поле, которое будет хранить единственный экземпляр класса Logger */
        private static Logger instance;
        /**  свойство, которое позволяет получить доступ к этому единственному экземпляру.*/
        public static Logger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Logger();
                }
                return instance;
            }
        }
        /** Поле названия проекта */
        private static string fileName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
        /** Поле текущей даты для генерации новых файлов */
        static string dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

        /** Поле хранит текущий уровень логирования. По умолчанию TRACE */
        private  Level lvl = Level.TRACE;
        /** Поле режима введения логов. По умолчанию CONSOLE */
        private  Mode mode = Mode.CONSOLE;

        /** Хранение режима ведения журнала логов(Раздельно\вместе). По умолчанию - false */
        private  bool separateLogging = false;
        /** Поле содержит информацию о необходимости генерации новы файлов при каждом запуске программы. По умолчанию - false */
        private  bool newFileName = false;

    //Свойства
        /** Установление или получение текущего уровня логирования*/
        public Level level
        {
            get { return lvl; }
            set { lvl = value; }
        }
        /** Установление или получение режима ведения логов*/
        public Mode Writemode
        {
            get { return mode; }
            set { mode = value; }
        }
        /** Установление или получение текущего имени файла логов*/
        public static string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        /** Установление или получение опции введения раздельного введения логов*/
        public bool SeparateLogging
        {
            get { return separateLogging; }
            set { separateLogging = value; }
        }
        /** Установление или получение сведений о необходимости генерации новых имен файлов для каждого запуска программы*/
        public bool NewFile
        {
            get { return newFileName; }
            set { newFileName = value; }
        }
    //Методы
        /**
         * Метод записи логов
         * @param levelMessage - уровень лога сообщения
         * @param message - ссылка на ссобщение для логирования
         * @param filePath - ссылка на путь к файлу программы, откуда был совершен вызов
         * @param member - ссылка на название функции, откуда был совершен вызов
         * @param line - ссылка на номер строки программы, откуда был совершен вызов
         */
        private  void WriteData(Level levelMesage, ref string message, ref string filePath, ref string member, ref int line)
        {
            /** Вывоод логов соглассно установленному минимальному уровню*/
            if (levelMesage < lvl)
            {
                return;
            }
            /** Текст лога*/
            string text = $"[{DateTime.Now}] | {levelMesage} | {filePath}\\{member}:{line} -> {message}\n";

            string fileName = FileName;

            /** Формирование имени файла в зависимости от режима введения логов*/
            if (SeparateLogging)
            {
                fileName += "_" + levelMesage.ToString();
            }
            /** Формирование имени в зависимости от опции для генерации новых имен файла для каждого запуска программы*/
            if (NewFile)
            {
                fileName += "_" + dateTimeNow;
            }
            fileName += ".log";


            /**Запись лога согласно выбарнному режиму*/
            switch (mode)
            {
                /** Вывод в консоли*/
                case Mode.CONSOLE:
                    Console.Write(text); 
                    break;
                /** Вывод в файл*/
                case Mode.FILE:
                    File.AppendAllText(fileName, text); 
                    break;
                /** Вывод в консоль и файл*/
                case Mode.CONSOLE_FILE:
                    Console.Write(text);
                    File.AppendAllText(fileName, text); 
                    break;
                /** Вывод в консоль исключения*/
                default:
                    Console.Write("Неопределенный режим записи. " + text); 
                    break;
            }
        }
        /** Макрос|Метод для ввывода информации лога
            * @param message - сообщение лога.
            * @param filePath - путь до файла программы.
            * @param member - Функция, откуда совершен вызов.
            * @param line - Строка программы, откуда совершен вызов.
         */
        public  void LOGT(string message, 
                        [CallerFilePath] string filePath = "",
                        [CallerMemberName] string member = "",
                        [CallerLineNumber] int line = 0)
        {
            WriteData(Level.TRACE, ref message, ref filePath, ref member, ref line);
        }

        public  void LOGD(string message, 
                        [CallerFilePath] string filePath = "",
                        [CallerMemberName] string member = "",
                        [CallerLineNumber] int line = 0)
        {
            WriteData(Level.DEBUG, ref message, ref filePath, ref member, ref line);
        }

        public  void LOGI(string message, 
                        [CallerFilePath] string filePath = "",
                        [CallerMemberName] string member = "",
                        [CallerLineNumber] int line = 0)
        {
            WriteData(Level.INFO, ref message, ref filePath, ref member, ref line);
        }
        public  void LOGW(string message, 
                        [CallerFilePath] string filePath = "",
                        [CallerMemberName] string member = "",
                        [CallerLineNumber] int line = 0)
        {
            WriteData(Level.WARN, ref message, ref filePath, ref member, ref line);
        }
        public  void LOGE(string message, 
                        [CallerFilePath] string filePath = "",
                        [CallerMemberName] string member = "",
                        [CallerLineNumber] int line = 0)
        {
            WriteData(Level.ERROR, ref message, ref filePath, ref member, ref line);
        }
    }
}
