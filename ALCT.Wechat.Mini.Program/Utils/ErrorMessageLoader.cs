using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ALCT.Wechat.Mini.Program.Utils
{
    public class ErrorMessageLoader
    {
        private static ErrorMessageLoader instance;
        private string Language;
        private string Path;

        private IDictionary<string, string> ErrorMessages;

        private ErrorMessageLoader(string path, string language)
        {
            this.Path = path;
            this.Language = language;
            LoadMessage();
        }

        private void LoadMessage()
        {
            if (Directory.Exists(Path))
            {
                var files = Directory.GetFiles(Path, $"*{Language}*.json");
                if (files.Length > 0)
                {
                    using(StreamReader sr = new StreamReader(new FileStream(files[0], FileMode.Open, FileAccess.Read)))
                    {
                        var stringValue = sr.ReadToEnd();
                        ErrorMessages = JsonConvert.DeserializeObject<IDictionary<string, string>>(stringValue);
                    }
                }
            }
            else
            {
                ErrorMessages = new Dictionary<string, string>();
            }
        }

        public static ErrorMessageLoader CreateLoader(string path, string language)
        {
            if (instance == null || instance.Language != language)
            {
                instance = new ErrorMessageLoader(path, language);
            }
            return instance;
        }

        public static ErrorMessageLoader LoadLoader()
        {
            if (instance == null)
            {
                throw new NullReferenceException("Error message loader hasn't be initialized.");
            }
            return instance;
        }

        public string GetMessage(string errorCode)
        {
            return GetMessage(errorCode, errorCode);
        }

        public string GetMessage(string errorCode, string defaultValue)
        {
            if (ErrorMessages.Keys.Contains(errorCode))
            {
                return ErrorMessages[errorCode];
            }
            else
            {
                return defaultValue;
            }
        }

    }
}