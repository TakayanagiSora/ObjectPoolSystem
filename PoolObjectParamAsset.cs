using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v�[���I�u�W�F�N�g�̃p�����[�^��ݒ肵�ĕۑ�����N���X
/// </summary>
// Assets > Create > Scriptable > CreatePoolObjectAsset �������ł��̃A�Z�b�g���쐬
[CreateAssetMenu(fileName = "Data", menuName = "Scriptable/CreatePoolObjectAsset")]
public class PoolObjectParamAsset : ScriptableObject
{
    // List�ŃI�u�W�F�N�g���ƂɊe�v�f��ێ�
    public List<PoolInfomation> _scriptablePoolInformation = new List<PoolInfomation>();
}

// Inspector�ŕύX�������e��ێ�
[System.Serializable]
public class PoolInfomation
{
    [Tooltip("�I�u�W�F�N�g��")]
    public string _name = default;

    [Tooltip("�v�[��������v���n�u")]
    public CashObjectInformation _prefab = default;

    [Tooltip("�L���[�̍ő�e��")]
    public int _maxQueue = default;
}