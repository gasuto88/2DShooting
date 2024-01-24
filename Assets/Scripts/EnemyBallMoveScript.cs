/*-------------------------------------------------
* EnemyBallMoveScript.cs
* 
* 作成日　2023/12/29
* 更新日　2024/1/12
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;

/// <summary>
/// 敵の弾を動かす
/// </summary>
public class EnemyBallMoveScript : BallMoveScript
{
	#region フィールド変数

	private float _ballSpeed = default;

	private float _ballRotationSpeed = default;
	#endregion

	#region プロパティ

	public float BallSpeed { get => _ballSpeed; set => _ballSpeed = value; }

	public float BallRotationSpeed { get => _ballRotationSpeed; set => _ballRotationSpeed = value; }

    #endregion

    /// <summary>
    /// 敵の弾を動かす処理
    /// </summary>
    protected override void BallMove()
    {
		// 上方向に移動
		myTransform.Translate(transform.up * _ballSpeed * Time.deltaTime);

		// 弾のZ軸を回転
		myTransform.Rotate(transform.forward * _ballRotationSpeed * Time.deltaTime);

		// 弾がステージ範囲外だったら
		if (_gameManagerScript.CheckOutStage(myTransform.position))
		{
			this.enabled = false;
		}
    }
}