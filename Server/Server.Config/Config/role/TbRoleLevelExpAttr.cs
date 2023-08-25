//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Text.Json;



namespace cfg.role
{


public sealed partial class TbRoleLevelExpAttr
{
    private readonly Dictionary<int, role.LevelExpAttr> _dataMap;
    private readonly List<role.LevelExpAttr> _dataList;
    
    public TbRoleLevelExpAttr(JsonElement _json)
    {
        _dataMap = new Dictionary<int, role.LevelExpAttr>();
        _dataList = new List<role.LevelExpAttr>();
        
        foreach(JsonElement _row in _json.EnumerateArray())
        {
            var _v = role.LevelExpAttr.DeserializeLevelExpAttr(_row);
            _dataList.Add(_v);
            _dataMap.Add(_v.Level, _v);
        }
        PostInit();
    }

    public Dictionary<int, role.LevelExpAttr> DataMap => _dataMap;
    public List<role.LevelExpAttr> DataList => _dataList;

    public role.LevelExpAttr GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public role.LevelExpAttr Get(int key) => _dataMap[key];
    public role.LevelExpAttr this[int key] => _dataMap[key];

    public void Resolve(Dictionary<string, object> _tables)
    {
        foreach(var v in _dataList)
        {
            v.Resolve(_tables);
        }
        PostResolve();
    }

    public void TranslateText(System.Func<string, string, string> translator)
    {
        foreach(var v in _dataList)
        {
            v.TranslateText(translator);
        }
    }
    

    partial void PostInit();
    partial void PostResolve();
}

}