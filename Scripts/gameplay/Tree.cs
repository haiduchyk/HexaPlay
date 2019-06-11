using System.Collections.Generic;
using System.Linq;

public class Tree 
{
    public Node root = null;
    static public void checkConnection(Node node, List<Node> allConnects) 
    {
        allConnects.Add(node);
        Node[] n = node.nodes;

        for (int i = 0; i < n.Length; i++)
        {
            if (n[i] != null && !n[i].visited && !allConnects.Contains(n[i])) 
                checkConnection(n[i], allConnects); 
        }
    }
    public Node insert()
    {
        if (root == null) root = new Node(new int[2]) ;
        return root;
    }
    IEnumerable<Node> getNeighbors(Node node)
    {
        Node[] n = node.nodes;
        for (int i = 0; i < n.Length; i++)
            if (n[i] != null) yield return n[i];
    }
    public List<Node> findPath( Node end, List<List<Node>> all)
    {
        
        var newAll = new List<List<Node>>();
        foreach (var p in allPathes(end, all))
        {
            if (p.Contains(end)) return p;
            else newAll.Add(p);
        }
        return findPath(end, newAll);
    }
    public IEnumerable<List<Node>> allPathes(Node end, List<List<Node>> all) 
    {
        foreach (var path in all)
        {
            foreach (var node in getNeighbors(path.Last()))
            {
                if (!path.Contains(node) && !node.visited)
                {
                    var copyOfPath = new List<Node>(path);
                    copyOfPath.Add(node);
                    yield return copyOfPath;
                    if (node == end) yield break;
                }

            }
        }
    }
    static public void Reset(Node node) 
    {
        Node[] n = node.nodes;

        for (int i = 0; i < n.Length; i++)
        { 
            if (n[i] != null && n[i].visited) 
            {
                n[i].visited = false;
                Reset(n[i]);
            }
        }
    }
    static public int[] plus(int[] ar1, int[] ar2)
    {
        int[] res = new int[ar1.Length];
        for (int i = 0; i < ar1.Length; i++) res[i] = ar1[i] + ar2[i];
        return res;
    }
    static public int[] delta(int[] start, int[] final) 
    {
        int[] delta = new int[2];
        delta[0] = final[0] - start[0];
        delta[1] = final[1] - start[1];
        return delta;
    }
}
