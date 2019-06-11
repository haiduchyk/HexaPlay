using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Map : MonoBehaviour
{
    public GameObject HexPrefab;
    public GameObject StarPrefab;
    public GameObject EnemyPrefab;
    public StateOfGame stateOfGame;
    public Tree tree;
    public GameManager gameManager;
    
    void Start()
    {   
        ScriptParse.map = this;
        int level = PlayerPrefs.GetInt("Level");
        tree = ScriptParse.CreateLevel(level);
        GenerateMap(tree.root);
        Tree.Reset(tree.root);
        stateOfGame = new StateOfGame();
    }
    
    public void GenerateHex(Node node) {
        GameObject hex = instantiate(node, HexPrefab);
        hex.name += " hex";
        hex.GetComponent<Hex>().node = node;
        
    }
    public void GenerateEnemy(Node node) {
        GameObject enemy = instantiate(node, EnemyPrefab);
        enemy.name += " enemy";
        enemy.GetComponent<Enemy>().node = node;
        gameManager.enemyObjs.Add(enemy);
        gameManager.enemyNodes.Add(node);
    }
    public void GenerateStar(Node node) {
        GameObject star = instantiate(node, StarPrefab);
        star.name += " star";
        star.GetComponent<Star>().node = node;
        gameManager.starObjs.Add(star);
        gameManager.starNodes.Add(node);
    }
    public GameObject instantiate(Node node, GameObject prefab) 
    {
        int x = node.coor[0];
        int y = node.coor[1];
        GameObject obj = (GameObject)Instantiate(
            prefab,
            DrawHex.Position(x, y),
            Quaternion.identity,
            this.transform 
        );
        obj.name = string.Format("{0}, {1}", x, y);
        return obj;
    }
    public void GenerateMap(Node node) 
    {
        GenerateHex(node);
        node.visited = true;
        Node[] n = node.nodes;

        for (int i = 0; i < n.Length; i++)
            if (n[i] != null && !n[i].visited) 
                GenerateMap(n[i]);
    }
}
