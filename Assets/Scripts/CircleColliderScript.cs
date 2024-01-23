/*-------------------------------------------------
* CircleColliderScript.cs
* 
* ì¬ú@2023/11/20
*
* ì¬Ò@{Øån
-------------------------------------------------*/
using UnityEngine;

/// <summary>
/// ~ÌÕËð»è·é
/// </summary>
public class CircleColliderScript : MonoBehaviour
{
    // ð¼ªÉ·éè
    private const int HALF = 2;

    // Transformðú»
    private Transform _myTransform = default;

    // ©ªÌ¼a
    private float _myRadius = default;

    /// <summary>
    /// XVO
    /// </summary>
    private void Start()
    {
        // Transformðú»
        _myTransform = transform;

        // ©ªÌ¼aðÝè
        _myRadius = _myTransform.localScale.x / HALF;
    }
    /// <summary>
    /// ~ÌÕËð»è·é
    /// </summary>
    /// <param name="target">ÕËÎÛ</param>
    /// <returns>ÕË»è</returns>
    public bool CheckCircleCollision(Transform target)
    {
        // ©ªÆÕËÎÛÌ£
        float distance = Vector3.Distance(_myTransform.position,target.position);
        
        // ÕËÎÛÌ¼a
        float collisionRadius = target.localScale.x / HALF;

        // ©ªÆÕËÎÛÌ¼aÌv
        float totalRadius = collisionRadius + _myRadius;

        // ©ªÆÕËÎÛÌ£ ª ¼aÌv@
        return distance < totalRadius;
    }
}
