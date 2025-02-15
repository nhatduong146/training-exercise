namespace DesignGuidelines.Extensibities.VirtualMembers
{
    public class Parent
    {
        public virtual void Display()
        {
            Console.WriteLine("Parent Display");
        }

        public void Write()
        {
            Console.WriteLine("Parent Write");
        }
    }

    public class Child : Parent
    {
        public override void Display()
        {
            Console.WriteLine("Child Display");
        }

        public new void Write()
        {
            Console.WriteLine("Child Write");
        }
    }


    public class VirtualMemberCalling
    {
        public void Main()
        {
            Parent parent = new Parent();
            parent.Display(); // Output: Parent Display
            parent.Write(); // Output: Parent Write

            Child child = new Child();
            child.Display(); // Output: Child Display
            child.Write(); // Output: Child Write

            Parent parent1 = new Child();
            parent1.Display(); // Output: Child Display
            parent1.Write(); // Output: Parent Write
        }
    }
}
