using System;

namespace board
{
    class BoardExpection : Exception
    {
        public BoardExpection(string msg) : base(msg)
        {
        } 
    }
}
