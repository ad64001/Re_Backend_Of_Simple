namespace Re_Backend.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class InjectableAttribute : Attribute
    {
        public bool IsSingleton { get; set; }

        public InjectableAttribute(bool isSingleton = false)
        {
            IsSingleton = isSingleton;
        }
    }
}
