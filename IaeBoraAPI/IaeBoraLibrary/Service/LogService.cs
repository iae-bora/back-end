using IaeBoraLibrary.Model.Context;
using System.IO;
using System;

namespace IaeBoraLibrary.Service
{
    public static class LogService
    {
        public static void Write(Exception exception)
        {
            try
            {
                using (var context = new Context())
                {
                    var log = new Model.Log()
                    {
                        Message = exception.Message,
                        DateTime = DateTime.Now,
                        InnerExceptionMessage = exception.InnerException?.Message,
                        Source = exception.Source,
                        StackTrace = exception.StackTrace
                    };
                    context.Logs.Add(log);
                    context.SaveChanges();
                }
            }
            catch (Exception error)
            {
                WriteIO(error, exception);
            }
        }

        private static void WriteIO(Exception exception, Exception loggerException)
        {
            try
            {
                var text = string.Concat("Erro ao gravar o log, ", exception.Message, Environment.NewLine, "Exceção da aplicação: ", loggerException.Message);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "log");
                Directory.CreateDirectory(path);
                File.AppendAllText(path, text);
            }
            catch (Exception) { }
        }
    }
}
