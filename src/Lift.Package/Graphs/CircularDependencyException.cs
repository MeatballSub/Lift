using System;
using System.Text;

namespace Lift.Graphs
{
    public class CircularDependencyException : Exception
    {
        public CircularDependencyException()
            : this("Circular Dependency") { }

        public CircularDependencyException(string message)
            : base(message) { }

        public CircularDependencyException(string message, Exception innerException)
            : base(message, innerException) { }


        public override string Message
        {
            get
            {
                StringBuilder msg = new StringBuilder(this.InnerException?.Message);
                if(msg.Length != 0)
                {
                    msg.Append(Environment.NewLine);
                    msg.Append("  ");
                }
                msg.Append(base.Message);
                return msg.ToString();
            }
        }
    }
}
