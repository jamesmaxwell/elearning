using ServiceStack;
using ServiceStack.Logging;

namespace ELearning.Services
{
    public class ServiceBase : Service
    {
        protected ILog Log
        {
            get
            {
                return LogManager.LogFactory.GetLogger("Service");
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            Log.Debug("service disposing");
        }
    }
}