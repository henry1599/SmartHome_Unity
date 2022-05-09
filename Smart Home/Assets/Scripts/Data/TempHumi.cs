using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSONExtension;

public class TempHumi : Jsonable
{
    public TempHumi(int _id, string _cmd, string _name, List<double> _paras)
    {
        id = _id;
        cmd = _cmd;
        name = _name;
        paras = _paras;
    }
    public string FromObjectToJson()
    {
        return JsonUtility.ToJson(this);
    }
    public static string FromJsonToString(string json)
    {
        string rawJsonString = json.FromJson(json).ToString();
        return rawJsonString;
    }
    public static TempHumi FromJsonToObject(string json)
    {
        return JsonUtility.FromJson<TempHumi>(json);
    }
}
