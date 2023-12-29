/*-------------------------------------------------
* CircleColliderScript.cs
* 
* �쐬���@2023/11/20
*
* �쐬�ҁ@�{�ؑ�n
-------------------------------------------------*/
using UnityEngine;
/// <summary>
/// �~�̏Փ˂𔻒肷��
/// </summary>
public class CircleColliderScript : MonoBehaviour
{
    // �����𔼕��ɂ���萔
    private const int HALF = 2;

    // Transform��������
    private Transform _myTransform = default;

    // �����̔��a
    private float _myRadius = default;

    /// <summary>
    /// �X�V�O����
    /// </summary>
    private void Start()
    {
        // Transform��������
        _myTransform = transform;

        // �����̔��a��ݒ�
        _myRadius = _myTransform.localScale.x / HALF;
    }
    /// <summary>
    /// �~�̏Փ˂𔻒肷��
    /// </summary>
    /// <param name="collision">�ՓˑΏ�</param>
    /// <returns>�Փ˔���</returns>
    public bool CheckCircleCollision(Transform collision)
    {
        // �����ƏՓˑΏۂ̋���
        float distance = Vector3.Distance(_myTransform.position,collision.position);
        Debug.Log(distance);
        // �ՓˑΏۂ̔��a
        float collisionRadius = collision.localScale.x / HALF;

        // �����ƏՓˑΏۂ̔��a�̍��v
        float totalRadius = collisionRadius + _myRadius;

        // �����ƏՓˑΏۂ̋��� �� ���a�̍��v�@
        return distance < totalRadius;
    }
    /// <summary>
    /// ��Βl�ɕϊ�����
    /// </summary>
    /// <param name="value">��Βl�ɂ���l</param>
    /// <returns>��Βl�ɂ����l</returns>
    private float ConvertAbsoluteValue(float value)
    {     
        // ��Βl�ɕϊ�
        return (value * value) / HALF;
    }
}
