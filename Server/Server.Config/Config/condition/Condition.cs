//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Text.Json;
using Server.Config.Core;


namespace cfg.condition
{
public abstract partial class Condition :  BeanBase 
{
    public Condition(JsonElement _json) 
    {
        PostInit();
    }

    public Condition() 
    {
        PostInit();
    }

    public static Condition DeserializeCondition(JsonElement _json)
    {
        switch (_json.GetProperty("$type").GetString())
        {
            case "TimeRange": return new condition.TimeRange(_json);
            case "MultiRoleCondition": return new condition.MultiRoleCondition(_json);
            case "GenderLimit": return new condition.GenderLimit(_json);
            case "MinLevel": return new condition.MinLevel(_json);
            case "MaxLevel": return new condition.MaxLevel(_json);
            case "MinMaxLevel": return new condition.MinMaxLevel(_json);
            case "ClothesPropertyScoreGreaterThan": return new condition.ClothesPropertyScoreGreaterThan(_json);
            case "ContainsItem": return new condition.ContainsItem(_json);
            default: throw new SerializationException();
        }
    }



    public virtual void Resolve(Dictionary<string, object> _tables)
    {
        PostResolve();
    }

    public virtual void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "}";
    }

    partial void PostInit();
    partial void PostResolve();
}
}