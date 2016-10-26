namespace Game.Kernel
{
    using Game.Utils;
    using System;

    public class DbException : UCException
    {
        public DbException(string message) : base(message)
        {
        }

        public int Number
        {
            get
            {
                return 0;
            }
        }
    }
}

