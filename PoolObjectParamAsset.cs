using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プールオブジェクトのパラメータを設定して保存するクラス
/// </summary>
// Assets > Create > Scriptable > CreatePoolObjectAsset を押下でこのアセットを作成
[CreateAssetMenu(fileName = "Data", menuName = "Scriptable/CreatePoolObjectAsset")]
public class PoolObjectParamAsset : ScriptableObject
{
    // Listでオブジェクトごとに各要素を保持
    public List<PoolInfomation> _scriptablePoolInformation = new List<PoolInfomation>();
}

// Inspectorで変更した内容を保持
[System.Serializable]
public class PoolInfomation
{
    [Tooltip("オブジェクト名")]
    public string _name = default;

    [Tooltip("プール化するプレハブ")]
    public CashObjectInformation _prefab = default;

    [Tooltip("キューの最大容量")]
    public int _maxQueue = default;
}