namespace Game.Kernel
{
    using System;
    using System.Collections;

    public interface IMessage
    {
        void AddEntity(ArrayList entityList);
        void AddEntity(object entity);
        void ResetEntityList();

        string Content { get; set; }

        ArrayList EntityList { get; set; }

        int MessageID { get; set; }

        bool Success { get; set; }
    }
}

