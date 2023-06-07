using UnityEngine;

/// <summary>
/// プールオブジェクトにアタッチする、情報キャッシュ用クラス
/// </summary>
public class CashObjectInformation : MonoBehaviour
{
    /// <summary>
    /// 自身のオブジェクトの種類
    /// </summary>
    [HideInInspector]
    public PoolObjectType _myObjectType = default;
}
