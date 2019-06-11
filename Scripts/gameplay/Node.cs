using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    // Start is called before the first frame update

    public bool visited = false;
    public int[] coor = new int[2];
    public Node[] nodes = new Node[6];
    
    public Node(int[] coor) 
    {
        this.coor = coor;
    }
    
    public Node addNode(int index, Node newNode) 
    {
       // p(newNode.ToString() + "index" + index);
        connect(this, newNode, index);

        return newNode;
    }
    public void connect(Node node, Node newNode, int index) 
    {
        node.nodes[index] = newNode;
        newNode.nodes[mirror(index)] = node;
        var nextNode = node.nodes[next(index)];
        int nextIndex = next(mirror(next(index)));
        var prevNode = node.nodes[previous(index)];
        int prevIndex = previous(mirror(previous(index)));
        if (nextNode != null && nextNode.nodes[nextIndex] == null) connect(nextNode, newNode, nextIndex);//  
        if (prevNode != null && prevNode.nodes[prevIndex] == null ) connect(prevNode, newNode, prevIndex); //
    }

    public int next(int i) => i == this.nodes.Length - 1 ? 0 : i + 1;
    public int previous(int i) => i == 0 ? this.nodes.Length - 1 : i - 1;
    public int mirror(int i) => i > 2 ? i - 3 : i + 3; 
    public string toString() => "( " + this.coor[0] + ", " + this.coor[1] + " ), ";
}
