using System;
using System.Data;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Logging;

namespace ELearning.Repository
{
    public interface IRepository
    {
    }

    public class Repository : IDisposable
    {
        public IDbConnectionFactory ConnFactory { get; set; }

        protected ILog Log
        {
            get
            {
                return LogManager.LogFactory.GetLogger("Repository");
            }
        }

        public void Dispose()
        {
            Log.Debug("Repository Disposing");
        }
    }
}