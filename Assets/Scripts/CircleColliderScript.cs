/*-------------------------------------------------
* CircleColliderScript.cs
* 
* 作成日　2023/11/20
* 更新日　2024/ 2/ 1
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;

/// <summary>
/// 円の衝突を判定するクラス
/// </summary>
public class CircleColliderScript : MonoBehaviour
{
    // 数字を半分にする定数
    private const int HALF = 2;

    // Transformを初期化
    private Transform _myTransform = default;

    // 自分の半径
    private float _myRadius = default;

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start()
    {
        // Transformを初期化
        _myTransform = transform;

        // 自分の半径を設定
        _myRadius = _myTransform.localScale.x / HALF;
    }
    /// <summary>
    /// 円の衝突を判定するクラス
    /// </summary>
    /// <param name="target">衝突対象</param>
    /// <returns>衝突判定</returns>
    public bool CheckCircleCollision(Transform target)
    {
        // 自分と衝突対象の距離
        float distance = Vector3.Distance(_myTransform.position,target.position);
        
        // 衝突対象の半径
        float collisionRadius = target.localScale.x / HALF;

        // 自分と衝突対象の半径の合計
        float totalRadius = collisionRadius + _myRadius;

        // 衝突判定　
        return distance < totalRadius;
    }
}
