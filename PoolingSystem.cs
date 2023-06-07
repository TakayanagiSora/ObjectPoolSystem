using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プールオブジェクトの種類
/// </summary>
public enum PoolObjectType
{
    cube,
    sphere,
    capsule
}

/// <summary>
/// オブジェクトプールを管理するクラス
/// </summary>
public class PoolingSystem : MonoBehaviour
{
    #region 変数
    [Tooltip("取得したPoolObjectParamAssetクラス")]
    public PoolObjectParamAsset _poolObjectParamAsset = default;

    [Tooltip("生成したプレハブを格納するためのキュー配列")]
    private Queue<CashObjectInformation>[] _objectQueues = default;

    [Tooltip("生成/取得したオブジェクトを格納する中間変数")]
    private CashObjectInformation _temporaryObject = default;
    #endregion


    private void Awake()
    {
        // プールオブジェクトの種類分、配列領域を確保する
        _objectQueues = new Queue<CashObjectInformation>[_poolObjectParamAsset._scriptablePoolInformation.Count];

        // プールの生成
        CreateObject();
    }

    /// <summary>
    /// プールを生成する
    /// </summary>
    private void CreateObject()
    {
        // 配列の要素数 = プールオブジェクトの種類分、繰り返す
        for (int i = 0; i < _objectQueues.Length; i++)
        {
            // 配列の中の各キューを生成
            _objectQueues[i] = new Queue<CashObjectInformation>();

            // 各プールオブジェクトに設定されたキューの最大容量まで、オブジェクト生成を繰り返す
            for (int k = 0; k < _poolObjectParamAsset._scriptablePoolInformation[i]._maxQueue; k++)
            {
                // オブジェクトを生成する
                _temporaryObject = Instantiate(_poolObjectParamAsset._scriptablePoolInformation[i]._prefab);

                // 生成したオブジェクトを非表示にする
                _temporaryObject.gameObject.SetActive(false);

                // 生成したオブジェクトに、どのプールオブジェクトかの情報を持たせる
                _temporaryObject.GetComponent<CashObjectInformation>()._myObjectType = (PoolObjectType)i;

                // 生成したオブジェクトを各キューに追加する
                _objectQueues[i].Enqueue(_temporaryObject);
            }
        }
    }

    /// <summary>
    /// プールからオブジェクトを取り出す
    /// </summary>
    /// <param name="poolObjectType">取り出すオブジェクトの種類</param>
    public GameObject GetObject(PoolObjectType poolObjectType)
    {
        try
        {
            // enumで指定されたキューからオブジェクトを取り出す
            _temporaryObject = _objectQueues[(int)poolObjectType].Dequeue();
        }
        // 容量不足でキューからの取り出しに失敗したとき
        catch (InvalidOperationException)
        {
            Debug.LogWarning(poolObjectType + "キューの容量不足");

            // オブジェクトを生成する
            _temporaryObject = Instantiate(_poolObjectParamAsset._scriptablePoolInformation[(int)poolObjectType]._prefab);

            // 生成したオブジェクトをキューに追加する
            _objectQueues[(int)poolObjectType].Enqueue(_temporaryObject);
        }

        // 取り出したオブジェクトを表示する
        _temporaryObject.gameObject.SetActive(true);

        // 取り出したオブジェクトをreturnする
        return _temporaryObject.gameObject;
    }

    /// <summary>
    /// プールにオブジェクトを返す
    /// </summary>
    /// <param name="returnObject">返すオブジェクト</param>
    public void ReturnObject(CashObjectInformation returnObject)
    {
        // 渡されたオブジェクトを非表示にする
        returnObject.gameObject.SetActive(false);

        // 渡されたオブジェクトを指定されたプールに返す
        _objectQueues[(int)returnObject._myObjectType].Enqueue(returnObject);
    }
}
