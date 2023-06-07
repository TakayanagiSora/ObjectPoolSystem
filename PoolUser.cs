using UnityEngine;

/// <summary>
/// �v�[������I�u�W�F�N�g���擾����N���X
/// <para>�i�f�o�b�O�p�j</para>
/// </summary>
public class PoolUser : MonoBehaviour
{
    [Tooltip("�擾����PoolingSystem�N���X")]
    public PoolingSystem _poolingSystem = default;

    // �{�Ԋ��͂��̂悤�ɒ萔��`���ĕێ琫�����߂�
    [Tooltip("���p����v�[���I�u�W�F�N�g�̎��")]
    private const PoolObjectType POOL_OBJECT_TYPE = PoolObjectType.cube;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Cube���擾
            // �{�Ԋ��͂��̂悤�Ɏg�p����
            _poolingSystem.GetObject(POOL_OBJECT_TYPE);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Sphere���擾
            _poolingSystem.GetObject(PoolObjectType.sphere);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // Capsule���擾
            _poolingSystem.GetObject(PoolObjectType.capsule);
        }
    }
}
