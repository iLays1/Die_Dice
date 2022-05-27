using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


public class NameGenerator
{
    static List<string> Names;
    public NameGenerator(string textFileName)
    {
        string readFromFilePath = Application.streamingAssetsPath + "/Names/" + textFileName + ".txt";
        Names = File.ReadAllLines(readFromFilePath).ToList();

        ResetList();
    }

    List<string> NamesToUse = new List<string>();

    public string Get()
    {
        if (NamesToUse.Count < 1) { ResetList(); }

        int i = Random.Range(0, NamesToUse.Count);
        var name = NamesToUse[i];
        NamesToUse.Remove(name);

        return name;
    }
    
    void ResetList()
    {
        NamesToUse.Clear();
        
        foreach (var n in Names) NamesToUse.Add(n);
    }
}
