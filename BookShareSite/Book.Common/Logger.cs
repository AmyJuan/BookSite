using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Book.Common
{
    public class Logger
    {
        private ILog log4net;
        private Type callerType;
        //static Logger()
        //{
        //    var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
        //    XmlConfigurator.ConfigureAndWatch(logCfg);
        //}
        private Logger(Type type)
        {
            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            log4net = LogManager.GetLogger(type);
            callerType = type;
        }
        public static Logger GetInstance(Type type)
        {
            return new Logger(type);
        }
        public void Debug(string log)
        {
            log4net.Debug(log);
        }

        public void Info(string log)
        {
            log4net.Info(log);
        }

        public void Warn(string log)
        {
            log4net.Warn(log);
        }

        public void Error(string log, Exception e = null)
        {
            log4net.Error(log);
        }

    }

    public static class LoggerExtender
    {
        public static void Error(this Logger logger, string msg, params object[] args)
        {
            logger.Error(string.Format(msg, args));
        }
        public static void Info(this Logger logger, string msg, params object[] args)
        {
            logger.Info(string.Format(msg, args));
        }

        public static void Debug(this Logger logger, string msg, params object[] args)
        {
            logger.Debug(string.Format(msg, args));
        }

        public static void Warn(this Logger logger, string msg, params object[] args)
        {
            logger.Warn(string.Format(msg, args));
        }
    }

}
