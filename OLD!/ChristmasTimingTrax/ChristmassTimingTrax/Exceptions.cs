using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmassTimingTrax
{
    #region File Exceptions

    public class FileNotFoundException : Exception
    {
        public FileNotFoundException()
        { }

        public FileNotFoundException(string message) : base(message)
        { }

        public FileNotFoundException(string message, Exception inner) : base(message, inner)
        { }

    }

    public class InvalidFileFormatException : Exception
    {
        public InvalidFileFormatException()
        { }

        public InvalidFileFormatException(string message) : base(message)
        { }

        public InvalidFileFormatException(string message, Exception inner) : base(message, inner)
        { }
    }

    #endregion

    #region Data Exceptions
    public class BadDataRowException : Exception
    {
        public BadDataRowException()
        { }

        public BadDataRowException(string message) : base(message)
        { }

        public BadDataRowException(string message, Exception inner) : base(message, inner)
        { }
    }
    #endregion
}
