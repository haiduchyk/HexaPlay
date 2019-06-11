using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    static public int[] degrees = new int[] { 
         0, 60, 120, 180, 240, 300
        };
    private float speed = 6f;
    public Vector3 target;
    public Node node;
    public float rotation = 0;
    public void SetRotation(Node start, Node end) 
    {
        
        int[] delta = Tree.delta(start.coor, end.coor);
        var d = ScriptParse.directions;
        int index = 0;
        for (int i = 0; i < d.Count; i++)
            if (d[i].SequenceEqual(delta)) 
                index = i;
        
        rotation = degrees[index];
        Quaternion target = Quaternion.Euler(0, 0, rotation);
        gameObject.transform.rotation = target;
    }

    void Start() => target = this.transform.position;
    void Update() => transform.position = Vector3.MoveTowards(
        transform.position, 
        target, 
        Time.deltaTime * speed
        );
}
