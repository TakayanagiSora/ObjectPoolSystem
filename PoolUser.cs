using UnityEngine;

/// <summary>
/// プールからオブジェクトを取得するクラス
/// <para>（デバッグ用）</para>
/// </summary>
public class PoolUser : MonoBehaviour
{
    [Tooltip("取得したPoolingSystemクラス")]
    public PoolingSystem _poolingSystem = default;

    // 本番環境はこのように定数定義して保守性を高める
    [Tooltip("利用するプールオブジェクトの種類")]
    private const PoolObjectType POOL_OBJECT_TYPE = PoolObjectType.cube;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Cubeを取得
            // 本番環境はこのように使用する
            _poolingSystem.GetObject(POOL_OBJECT_TYPE);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Sphereを取得
            _poolingSystem.GetObject(PoolObjectType.sphere);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // Capsuleを取得
            _poolingSystem.GetObject(PoolObjectType.capsule);
        }
    }
}
