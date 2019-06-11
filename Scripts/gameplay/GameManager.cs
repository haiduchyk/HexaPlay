using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> enemyObjs = new List<GameObject>();
    public List<GameObject> starObjs = new List<GameObject>();
    public List<Node> enemyNodes = new List<Node>();
    public List<Node> starNodes = new List<Node>();
    public StateOfGame state = new StateOfGame();
    public Map map;
    public AudioManager music;

    public int MAX_LEVEL = 2;
    float RESTART_DELAY = 1f;
    Color green = new Color(31f/255f, 104/255f, 85/255f);
 
    void Start()
    {
        state.AddObserver(music);
        if (!PlayerPrefs.HasKey("Level")) PlayerPrefs.SetInt("Level", 1);
        ScriptParse.game = this;
    }
    void setLevel() 
    {
        int current = PlayerPrefs.GetInt("Level");
        if (current >= MAX_LEVEL)  PlayerPrefs.SetInt("Level", 1);
        else PlayerPrefs.SetInt("Level", current + 1);
    }
    void Restart()
    {
        var tempScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(tempScene);
    }
    void endGame() {
        if (state.lose) 
        {
            music.Play();
            FindObjectOfType<Camera>().backgroundColor = Color.red;
            Invoke("Restart", RESTART_DELAY);
        }
    }
    void winGame() {
        if (state.win) {
            music.Play();
            FindObjectOfType<Camera>().backgroundColor = green;
            setLevel();
            Invoke("Restart", RESTART_DELAY);
        }
    }
    public bool check(Node node) 
    {
        bool active = GetActive(node);
        if (!active) 
        {
            node.visited = true;
            move();
        }
        return active;
    }
    bool GetActive(Node node) => enemyNodes.Contains(node) || starNodes.Contains(node);

    List<List<Node>> allPath(Node enemy) 
    {
        var allPath = new List<List<Node>>();
            foreach (var star in starNodes)
            {
                var all = new List<List<Node>>();
                all.Add(new List<Node>(){ enemy });
                if (connect(star, enemy)) continue;
                var path = map.tree.findPath(star, all);
                allPath.Add(path);
            }
        return allPath;
    }
    List<Node> minPath(List<List<Node>> allPath) 
    {
            var minPath = new List<Node>();
            int minLength = int.MaxValue;
            foreach (var path in allPath )
            {
                if (path.Count < minLength && path.Count != 0)
                {
                    minLength = path.Count;
                    minPath = path;
                }
            }
            return minPath;
    } 
    bool connect(Node start, Node end) 
    {
        var allConects = new List<Node>();
        Tree.checkConnection(start, allConects);
        return !allConects.Contains(end);
    }
    Node newEnemyNode(List<Node> path)
    {
       Node newEnemy = new Node(new int[2]);
       if (path.Count == 0) return new Node(new int[2]);
       return path.Count > 1 ? path[1] : path[0]; 
    }
    void move() 
    {
        music.Play();
        var newEnemyNodes = new List<Node>(enemyNodes);
        foreach (var enemy in enemyNodes)
        {
            var all = allPath(enemy);
            if (all.Count == 0) state.setWin(true);
            var min = minPath(all);
            if (min.Count == 2) state.setLose(true);
            winGame();
            var newEnemy = newEnemyNode(min);
            int oldIndex = enemyNodes.IndexOf(enemy);
            
            GameObject enemyObj = enemyObjs[oldIndex];
            newEnemyNodes[oldIndex] = newEnemy;
            var component = enemyObjs[oldIndex].GetComponent<Enemy>();

            changePositionAndRotate(enemy, newEnemy, component);
            endGame();
        }
        enemyNodes = newEnemyNodes;

    }
    void changePositionAndRotate(Node oldEnemy, Node newEnemy, Enemy component) 
    {
            component.SetRotation(oldEnemy, newEnemy);
            var x = newEnemy.coor[0];
            var y = newEnemy.coor[1];
            component.target = DrawHex.Position(x, y);
            component.node = newEnemy;
    } 
    void logl(List<Node> l)
    {
        string s = "";
        foreach (var item in l)
        {
            s += item.coor[0] + ", " + item.coor[1] + " => ";
        }
        Debug.Log(s);
    }
}
