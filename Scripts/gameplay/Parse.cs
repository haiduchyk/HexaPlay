using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public sealed class ScriptParse 
{
    static int nodeCounter = 0;
    static public List<int[]> directions = new List<int[]> {
    new int[] {1, 0}, 
    new int[] {1, -1}, 
    new int[] {0, -1}, 
    new int[] {-1, 0}, 
    new int[] {-1, 1}, 
    new int[] {0, 1},
    };

    static List<string> script;
    static string[] enemies;
    static string[] stars;
    static public Map map;
    static public GameManager game;
    static public Tree CreateTree() 
    {
        var tree = new Tree();
        var root = tree.insert();
        checkStuff(root, "0");
        NodesCreater(root); 
        return tree;
    }
    static public void saveTree(Node node)
    {
        script = new List<string>() {"", "|", ""};
        visualisation(node);
    }
    static public void visualisation(Node node) 
    {
        node.visited = true;
        Node[] n = node.nodes;
        for (int i = 0; i < n.Length; i++)
        {
            if (n[i] != null && !n[i].visited) 
            {
                existOfStuff(n[i], nodeCounter);
                writeNode(nodeCounter);
                visualisation(n[i]); 
            }
        }
    }
    static void existOfStuff(Node node, int number) 
    {
        if (game.enemyNodes.Contains(node)) writeEnemy(number);
        if (game.starNodes.Contains(node)) writeStar(number);
    }
    static void writeEnemy(int number) 
    {
        writeStuff(number, 0);
    }
    static void writeStar(int number) 
    {
        writeStuff(number, 1);
    }
    static void writeStuff(int number, int index) 
    {
        var stuff = script[script.Count - 2];
        var enemy = stuff.Split('|')[index] + number + " ";
        var res = enemy +  stuff.Split('|')[index];
        script[script.Count - 2] = res;
    }
    static public void NodesCreater(Node node, int line = 0) 
    {
        string[] elem = script[line].Split(' ');
        for (int i = 0; i < elem.Length; i++) {
            int index = getIndex(elem[i]);
            var newNode = addNode(index, node);
            string key = getKey(elem[i]);
            int nextLine = getLine(key);
            checkStuff(newNode, key + "");
            if (nextLine != -1) NodesCreater(newNode, nextLine);
        }
    }
    static public void makeListOfStuff() 
    {
         int secondLast  = getSecondLast(script);
         string[] elem = script[secondLast].Split('|');
         enemies = elem[0].Split(' ');
         stars = elem[1].Split(' ');
    }
    
    static int getIndex(string s) => numb(s.Substring(0, 1));
    static string getKey(string s) => s.Substring(1);
    static int getSecondLast(List<string> l) => l.Count - 2;
    static int getLine(string key) 
    {
        int last = script.Count - 1;
        return Array.IndexOf(script[last].Split(' '), key);
    }
    static public Node addNode(int index, Node node) 
    {
        int [] delta = directions[index]; 
        int[] coor = Tree.plus(delta, node.coor);
        var newNode = new Node(coor);
        node.addNode(index, newNode);
        return newNode;
    }
    static public void checkStuff(Node node, string line)
    {
        bool starFlag = stars.Contains(line);
        bool enemyFlag = enemies.Contains(line);
        if (starFlag) map.GenerateStar(node);
        if (enemyFlag) map.GenerateEnemy(node);
    }
    static public Tree CreateLevel(int level) 
    {
        ReadScript(level);
        makeListOfStuff();
        var tree = CreateTree();
        return tree;
    }
    static public void ReadScript(int numberOfLevel)
    {
        string path = "Assets/Scripts/levels/level" + numberOfLevel + ".ψna";
        StreamReader sr = new StreamReader(path);
        script = new List<string>();
        string line;
        while ((line = sr.ReadLine()) != null)
        script.Add(line);
        sr.Close();
    }
    static int numb(string s) => Convert.ToInt32(s);
}
