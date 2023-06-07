using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v�[���I�u�W�F�N�g�̎��
/// </summary>
public enum PoolObjectType
{
    cube,
    sphere,
    capsule
}

/// <summary>
/// �I�u�W�F�N�g�v�[�����Ǘ�����N���X
/// </summary>
public class PoolingSystem : MonoBehaviour
{
    #region �ϐ�
    [Tooltip("�擾����PoolObjectParamAsset�N���X")]
    public PoolObjectParamAsset _poolObjectParamAsset = default;

    [Tooltip("���������v���n�u���i�[���邽�߂̃L���[�z��")]
    private Queue<CashObjectInformation>[] _objectQueues = default;

    [Tooltip("����/�擾�����I�u�W�F�N�g���i�[���钆�ԕϐ�")]
    private CashObjectInformation _temporaryObject = default;
    #endregion


    private void Awake()
    {
        // �v�[���I�u�W�F�N�g�̎�ޕ��A�z��̈���m�ۂ���
        _objectQueues = new Queue<CashObjectInformation>[_poolObjectParamAsset._scriptablePoolInformation.Count];

        // �v�[���̐���
        CreateObject();
    }

    /// <summary>
    /// �v�[���𐶐�����
    /// </summary>
    private void CreateObject()
    {
        // �z��̗v�f�� = �v�[���I�u�W�F�N�g�̎�ޕ��A�J��Ԃ�
        for (int i = 0; i < _objectQueues.Length; i++)
        {
            // �z��̒��̊e�L���[�𐶐�
            _objectQueues[i] = new Queue<CashObjectInformation>();

            // �e�v�[���I�u�W�F�N�g�ɐݒ肳�ꂽ�L���[�̍ő�e�ʂ܂ŁA�I�u�W�F�N�g�������J��Ԃ�
            for (int k = 0; k < _poolObjectParamAsset._scriptablePoolInformation[i]._maxQueue; k++)
            {
                // �I�u�W�F�N�g�𐶐�����
                _temporaryObject = Instantiate(_poolObjectParamAsset._scriptablePoolInformation[i]._prefab);

                // ���������I�u�W�F�N�g���\���ɂ���
                _temporaryObject.gameObject.SetActive(false);

                // ���������I�u�W�F�N�g�ɁA�ǂ̃v�[���I�u�W�F�N�g���̏�����������
                _temporaryObject.GetComponent<CashObjectInformation>()._myObjectType = (PoolObjectType)i;

                // ���������I�u�W�F�N�g���e�L���[�ɒǉ�����
                _objectQueues[i].Enqueue(_temporaryObject);
            }
        }
    }

    /// <summary>
    /// �v�[������I�u�W�F�N�g�����o��
    /// </summary>
    /// <param name="poolObjectType">���o���I�u�W�F�N�g�̎��</param>
    public GameObject GetObject(PoolObjectType poolObjectType)
    {
        try
        {
            // enum�Ŏw�肳�ꂽ�L���[����I�u�W�F�N�g�����o��
            _temporaryObject = _objectQueues[(int)poolObjectType].Dequeue();
        }
        // �e�ʕs���ŃL���[����̎��o���Ɏ��s�����Ƃ�
        catch (InvalidOperationException)
        {
            Debug.LogWarning(poolObjectType + "�L���[�̗e�ʕs��");

            // �I�u�W�F�N�g�𐶐�����
            _temporaryObject = Instantiate(_poolObjectParamAsset._scriptablePoolInformation[(int)poolObjectType]._prefab);

            // ���������I�u�W�F�N�g���L���[�ɒǉ�����
            _objectQueues[(int)poolObjectType].Enqueue(_temporaryObject);
        }

        // ���o�����I�u�W�F�N�g��\������
        _temporaryObject.gameObject.SetActive(true);

        // ���o�����I�u�W�F�N�g��return����
        return _temporaryObject.gameObject;
    }

    /// <summary>
    /// �v�[���ɃI�u�W�F�N�g��Ԃ�
    /// </summary>
    /// <param name="returnObject">�Ԃ��I�u�W�F�N�g</param>
    public void ReturnObject(CashObjectInformation returnObject)
    {
        // �n���ꂽ�I�u�W�F�N�g���\���ɂ���
        returnObject.gameObject.SetActive(false);

        // �n���ꂽ�I�u�W�F�N�g���w�肳�ꂽ�v�[���ɕԂ�
        _objectQueues[(int)returnObject._myObjectType].Enqueue(returnObject);
    }
}
