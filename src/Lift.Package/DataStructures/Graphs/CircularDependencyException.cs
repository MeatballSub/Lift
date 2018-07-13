using System;
using System.Text;

namespace Lift.DataStructures.Graphs
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
                var msg = new StringBuilder(this.InnerException?.Message);
                if (msg.Length != 0)
                {
                    msg.AppendLine();
                    msg.Append("  ");
                }

                msg.Append(base.Message);
                return msg.ToString();
            }
        }
    }
}
