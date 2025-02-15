namespace DesignGuidelines.Extensibities.SealingMember
{
    class Parent
    {
        public virtual void Show() { }
    }
    class Child : Parent
    {
        public sealed override void Show() { }
    }

    class GrandChild : Child
    {
        //'GrandChild.Show()': cannot override inherited member 'Child.Show()' because it is sealed
        public override void Show() { }
    }

    class Son : Parent
    {
        /*
         * DO NOT declare protected or virtual members on sealed types.
         * By definition, sealed types cannot be inherited from. 
         * This means that protected members on sealed types cannot be called, 
         * and virtual methods on sealed types cannot be overridden.
         */
        protected virtual sealed override void Show() { }
    }

    public class SealedMember
    {
    }
}
