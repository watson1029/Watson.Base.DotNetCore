using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Watson.Base.DotNetCore
{
    public class LogHelper
    {
        private readonly static Context.LogDbContext _context;
        private readonly static string _file;
        private static object obj = new object();
        static LogHelper()
        {
            if (string.IsNullOrEmpty(ConfigurationHelper.Configuration.GetSection("WatsonBaseSettings")["SysLog"]))
            {
                DbContextOptions<Context.LogDbContext> dbContextOption = new DbContextOptions<Context.LogDbContext>();
                DbContextOptionsBuilder<Context.LogDbContext> dbContextOptionBuilder = new DbContextOptionsBuilder<Context.LogDbContext>(dbContextOption);
                _context = new Context.LogDbContext(dbContextOptionBuilder.UseSqlServer(ConfigurationHelper.Configuration.GetSection("WatsonBaseSettings")["SysLog"]).Options);
            }

            if (string.IsNullOrEmpty(ConfigurationHelper.Configuration.GetSection("LogFile").ToString()))
            {
                _file = ConfigurationHelper.Configuration.GetSection("WatsonBaseSettings")["LogFile"].EndsWith("\\")
                    ? ConfigurationHelper.Configuration.GetSection("WatsonBaseSettings")["LogFile"]
                    : ConfigurationHelper.Configuration.GetSection("WatsonBaseSettings")["LogFile"] + "\\";
            }
        }

        public static async void WriteDbAsync(Exception ex)
        {
            DataBase.SysLog log = new DataBase.SysLog();
            log.LogID = Guid.NewGuid();
            log.LogMsg = ex.Message;
            log.LogDetail = ex.ToString();
            log.LogTime = DateTime.Now;
            _context.SysLog.Add(log);
            await _context.SaveChangesAsync();
        }

        public static async void WriteDbAsync(string logMsg, string logDetail)
        {
            DataBase.SysLog log = new DataBase.SysLog();
            log.LogID = Guid.NewGuid();
            log.LogMsg = logMsg;
            log.LogDetail = logDetail;
            log.LogTime = DateTime.Now;
            _context.SysLog.Add(log);
            await _context.SaveChangesAsync();
        }

        public static void WriteFile(Exception ex)
        {
            string path = string.Concat(_file, DateTime.Now.ToString("yyyyMMdd"), ".txt");
            lock (obj)
            {
                if (File.Exists(path))
                {
                    using (StreamWriter sr = File.AppendText(path))
                    {
                        sr.WriteLine(string.Concat(Environment.NewLine, "LogTime:", DateTime.Now.ToString()));
                        sr.WriteLine(string.Concat("LogMsg:", ex.Message));
                        sr.WriteLine(string.Concat("LogDetail:", ex.ToString()));
                        sr.Close();
                    }
                }
                else
                {
                    using (StreamWriter sr = File.CreateText(path))
                    {
                        sr.WriteLine(string.Concat(Environment.NewLine, "LogTime:", DateTime.Now.ToString()));
                        sr.WriteLine(string.Concat("LogMsg:", ex.Message));
                        sr.WriteLine(string.Concat("LogDetail:", ex.ToString()));
                        sr.Close();
                    }
                }
            }
        }

        public static void WriteFile(string logMsg, string logDetail)
        {
            string path = string.Concat(_file, DateTime.Now.ToString("yyyyMMdd"), ".txt");
            lock (obj)
            {
                if (File.Exists(path))
                {
                    using (StreamWriter sr = File.AppendText(path))
                    {
                        sr.WriteLine(string.Concat(Environment.NewLine, "LogTime:", DateTime.Now.ToString()));
                        sr.WriteLine(string.Concat("LogMsg:", logMsg));
                        sr.WriteLine(string.Concat("LogDetail:", logDetail));
                        sr.Close();
                    }
                }
                else
                {
                    using (StreamWriter sr = File.CreateText(path))
                    {
                        sr.WriteLine(string.Concat(Environment.NewLine, "LogTime:", DateTime.Now.ToString()));
                        sr.WriteLine(string.Concat("LogMsg:", logMsg));
                        sr.WriteLine(string.Concat("LogDetail:", logDetail));
                        sr.Close();
                    }
                }
            }
        }
    }
}
