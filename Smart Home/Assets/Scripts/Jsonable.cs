using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jsonable
{
    public int id { get; set; }
    public string cmd { get; set; }
    public string name { get; set; }
    public List<double> paras { get; set; }
}
