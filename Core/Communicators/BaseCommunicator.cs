using Core.Utils;

namespace Core.Communicators
{
    public abstract class BaseCommunicator
    {
        protected readonly SecurityProfiler SecurityProfiler;
        public BaseCommunicator(SecurityProfiler securityProfiler)
        {
            SecurityProfiler = securityProfiler;
        }
    }
}
