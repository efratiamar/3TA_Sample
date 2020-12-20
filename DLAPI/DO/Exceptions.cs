using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [Serializable]
    public class BadPersonIdException : Exception
    {
        public int ID;
        public BadPersonIdException(int id) : base() => ID = id;
        public BadPersonIdException(int id, string message) : 
            base(message) => ID = id;
        public BadPersonIdException(int id, string message, Exception innerException) : 
            base(message, innerException) => ID = id;
        public override string ToString() => base.ToString() + $", bad person id: {ID}";
    }
}
