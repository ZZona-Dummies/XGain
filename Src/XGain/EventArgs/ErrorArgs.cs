using System;

namespace XGain
{
    public class ErrorArgs : EventArgs
    {
        public ErrorArgs()
        {
        }

        public ErrorArgs(Exception ex)
        {
            Date = DateTime.Now;
            Exception = ex;
        }

        public DateTime Date { get; set; }
        public Exception Exception { get; set; }
    }
}
