using System;
using System.Linq;
using System.Collections.Generic;

struct Tests
{
    static public void EqualNode(String name, Node a, Node b)
    {
        if (!(a.coor[0] == b.coor[0] && a.coor[1] == b.coor[1]))
            Tests.Complain(name);
    }

    static public void EqualInt(String name, int a, int b)
    {
        if (!(a == b))
            Tests.Complain(name);
    }

    static public void EqualHexArray(String name, List<Node> a, List<Node> b)
    {
        Tests.EqualInt(name, a.Count, b.Count);
        for (int i = 0; i < a.Count; i++)
            Tests.EqualNode(name, a[i], b[i]);
    }


    static public void TestHexList()
    {
        EqualHexArray("EqualHexArray", new List<Node> { new Node(new int[2] {0, 1})}, new List<Node> {new Node(new int[2] {0, 1})});
    }

    static public void TestAll()
    {
        TestHexList();
    }

    static public void Main()
    {
        Tests.TestAll();
    }

    static public void Complain(String name)
    {
        Console.WriteLine("FAIL " + name);
    }

}