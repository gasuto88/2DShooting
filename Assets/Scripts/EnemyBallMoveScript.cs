/*-------------------------------------------------
* EnemyBallMoveScript.cs
* 
* 作成日　2023/12/29
* 更新日　2023/12/29
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;

/// <summary>
/// 敵の弾を動かすクラス
/// </summary>
public class EnemyBallMoveScript : BallMoveScript 
{
	#region 定数

	// 弾のTag
	private const string BALL = "Ball";

	#endregion

	#region フィールド変数

	[SerializeField, Header("敵の弾の速度"), Range(0, 100)]
	private float _enemyBallSpeed = default;

	#endregion

	/// <summary>
	/// 更新前処理
	/// </summary>
	protected override void Start () 
	{
		base.Start();
	}
	
	/// <summary>
    /// 更新処理
    /// </summary>
	protected void Update () 
	{
		BallMove();
	}

	/// <summary>
	/// 敵の弾を動かす処理
	/// </summary>
    protected override void BallMove()
    {
		// 上方向に移動
		myTransform.Translate(Vector3.up * _enemyBallSpeed * Time.deltaTime);

		// 弾がステージ範囲外だったら
		if (_gameManagerScript.CheckOutStage(myTransform.position))
		{
			this.enabled = false;

			// TagをBallに設定
			this.tag = BALL;
		}

		base.BallMove();
    }
}