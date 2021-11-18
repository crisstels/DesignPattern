using System;
using System.Collections.Generic;

namespace Composite
{
    // The base Component class declares common operations for both simple and
    // complex objects of a composition.
    abstract class Order
    {
        public Order() { }

        // The base Component may implement some default behavior or leave it to
        // concrete classes (by declaring the method containing the behavior as
        // "abstract").
        public abstract int GetPrice(int preis);

        // In some cases, it would be beneficial to define the child-management
        // operations right in the base Component class. This way, you won't
        // need to expose any concrete component classes to the client code,
        // even during the object tree assembly. The downside is that these
        // methods will be empty for the leaf-level components.
        public virtual void Add(Order component)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(Order component)
        {
            throw new NotImplementedException();
        }

        // You can provide a method that lets the client code figure out whether
        // a component can bear children.
        public virtual bool IsComposite()
        {
            return true;
        }
    }

    // The Leaf class represents the end objects of a composition. A leaf can't
    // have any children.
    //
    // Usually, it's the Leaf objects that do the actual work, whereas Composite
    // objects only delegate to their sub-components.
    class Artikel : Order
    {
        public override int GetPrice(int preis)
        {
            return preis;
        }

        public override bool IsComposite()
        {
            return false;
        }
    }

    // The Composite class represents the complex components that may have
    // children. Usually, the Composite objects delegate the actual work to
    // their children and then "sum-up" the result.
    class Box : Order
    {
        protected List<Order> _children = new List<Order>();

        public override void Add(Order component)
        {
            this._children.Add(component);
        }

        public override void Remove(Order component)
        {
            this._children.Remove(component);
        }

        // The Composite executes its primary logic in a particular way. It
        // traverses recursively through all its children, collecting and
        // summing their results. Since the composite's children pass these
        // calls to their children and so forth, the whole object tree is
        // traversed as a result.
        public override int GetPrice(int preis)
        {
            int i = 0;
            int result =0;

            foreach (Order component in this._children)
            {
                result += component.GetPrice(preis);
                i++;
            }

            return result;
        }
    }

    class Client
    {
        // The client code works with all of the components via the base
        // interface.
        public void ClientCode(Order leaf, int preis)
        {
            Console.WriteLine($"Price: {leaf.GetPrice(preis)}\n");
        }

        // Thanks to the fact that the child-management operations are declared
        // in the base Component class, the client code can work with any
        // component, simple or complex, without depending on their concrete
        // classes.
        public void ClientCode2(Order component1, Order component2, int preis)
        {
            if (component1.IsComposite())
            {
                component1.Add(component2);
            }

            Console.WriteLine($"Price: {component1.GetPrice(preis)}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();

            // This way the client code can support the simple leaf
            // components...
            Artikel leaf = new Artikel();
            int preis = 2;
            Console.WriteLine("Client: I get a Mobile phone:");
            client.ClientCode(leaf, preis);

            // ...as well as the complex composites.
            Box tree = new Box();
            Box branch1 = new Box();
            Box branch2 = new Box();

            branch1.Add(new Artikel());
            branch1.Add(new Artikel());

            branch2.Add(new Artikel());

            tree.Add(branch1);
            tree.Add(branch2);
            Console.WriteLine("Client: Now I've got 3 mobile phones:");
            client.ClientCode(tree, preis);

            Console.Write("Client: Order with 4 mobile Phones:\n");
            client.ClientCode2(tree, leaf, preis);
        }
    }
}
