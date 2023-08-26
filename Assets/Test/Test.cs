using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class Test : MonoBehaviour
{
    private void Start()
    {
        var stopwatch =  System.Diagnostics.Stopwatch.StartNew();
        var path =  Application.streamingAssetsPath + "/EnemyDataEntities.xlsx";//python生成的表格
        var path2 = Application.streamingAssetsPath + "/EnemyData.xlsx";
        var data = ExcelRuntimeTools.ExcelLoader.LoadExcel<EnemyData>(path);
        Debug.Log($"加载耗时:{stopwatch.ElapsedMilliseconds}ms");
        

        stopwatch = System.Diagnostics.Stopwatch.StartNew();
        data = ExcelRuntimeTools.ExcelLoader.LoadExcel<EnemyData>(path);
        Debug.Log($"二次加载耗时:{stopwatch.ElapsedMilliseconds}ms");

        stopwatch = System.Diagnostics.Stopwatch.StartNew();
        //测试发现只能减少5ms左右
        using (var fs = File.OpenRead(path))
        {
            var data2 = ExcelRuntimeTools.ExcelLoader.LoadExcel<EnemyData, EnemyDataEntity>(fs);
            Debug.Log($"减少反射加载时间:{stopwatch.ElapsedMilliseconds}ms");
        }

        //打印数据
        //foreach (var item in data.EnemyDataEntities)
        //{
        //    Debug.Log($"Id:{item.Id} Name:{item.Name} Health:{item.Health} Exp:{item.Exp} Attack:{item.Attack} 中文测试:{item.中文测试} ");
        //}


        var dic = new Dictionary<int, EnemyDataEntity>();
        ExcelRuntimeTools.ExcelLoader.LoadToDictionary(dic, path2);
        Debug.Log($"字典数量:{dic.Count}");

        foreach (var item in dic)
        {
            Debug.Log($"Key:{item.Key} Id:{item.Value.Id} Name:{item.Value.Name} Health:{item.Value.Health} Exp:{item.Value.Exp} Attack:{item.Value.Attack} 中文测试:{item.Value.中文测试} ");
        }

        var list = new List<EnemyDataEntity>();
        ExcelRuntimeTools.ExcelLoader.LoadToList(list, path2);
        Debug.Log($"列表数量:{list.Count}");

        foreach (var item in list)
        {
            Debug.Log($"Id:{item.Id} Name:{item.Name} Health:{item.Health} Exp:{item.Exp} Attack:{item.Attack} 中文测试:{item.中文测试} ");
        }
    }
}
