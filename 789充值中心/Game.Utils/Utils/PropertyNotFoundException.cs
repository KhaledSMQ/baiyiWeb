namespace Game.Utils
{
    using System;

    public class PropertyNotFoundException : UCException
    {
        private string targetPropertyName;

        public PropertyNotFoundException()
        {
        }

        public PropertyNotFoundException(string propertyName) : base(string.Format("The property named '{0}' not found in Entity definition.", propertyName))
        {
            this.targetPropertyName = propertyName;
        }

        public string TargetPropertyName
        {
            get
            {
                return this.targetPropertyName;
            }
            set
            {
                this.targetPropertyName = value;
            }
        }
    }
}

