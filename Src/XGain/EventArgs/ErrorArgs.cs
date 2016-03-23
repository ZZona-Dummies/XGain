using System;

namespace XGain
{
    public class ErrorArgs : EventArgs
    {
        public ErrorArgs(Exception ex)
        {
            Date = DateTime.Now;
            Exception = ex;
        }

        public DateTime Date { get; }
        public Exception Exception { get; }
    }
}
