using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jsonable
{
    public int id;
    public string cmd;
    public string name;
    public string paras;
    public List<string> ExtractParas()
    {
        List<string> result = new List<string>();
        if (paras == "none")
        {
            return result;
        }
        paras = paras.Replace('}',' ');
        paras = paras.Replace('{',' ');
        paras = paras.Trim();
        string[] parasElements = paras.Split(',');
        foreach (string parasElement in parasElements)
        {
            result.Add(parasElement);
        }
        return result;
    }
}
