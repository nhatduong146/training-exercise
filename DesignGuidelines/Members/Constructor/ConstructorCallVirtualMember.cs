namespace DesignGuidelines.Members.ConstructorCallVirtualMember
{
    // AVOID calling virtual members on an object inside its constructor.
    public class ConstructorCallVirtualMember
    {
        public class BaseClass
        {
            public BaseClass()
            {
                /*
                 * Calling a virtual method inside the constructor is risky
                 * DoSomething might be called before the derived class is fully initialized, leading to potential issues
                 */
                DoSomething(); 
            }

            public virtual void DoSomething()
            {
                Console.WriteLine("Base implementation");
            }
        }

        public class DerivedClass : BaseClass
        {
            public DerivedClass() : base()
            {
            }

            public override void DoSomething()
            {
                Console.WriteLine("Derived implementation");
            }
        }
    }
}
