using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace JSONExtension
{
    public static class JSONExtension
    {
        public static string ToJson(this object obj)
        {
            string result = JsonConvert.SerializeObject(obj);
            return result;
        }
        public static object FromJson(this object obj, string json)
        {
            return JsonConvert.DeserializeObject(json);
        }
    }
}
