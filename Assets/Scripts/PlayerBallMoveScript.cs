/*-------------------------------------------------
* PlayerBallMoveScript.cs
* 
* 作成日　2023/12/29
* 作成日　2023/12/29
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;

/// <summary>
/// プレイヤーの弾を動かすクラス
/// </summary>
public class PlayerBallMoveScript : BallMoveScript
{
	#region フィールド変数

	[SerializeField, Header("プレイヤーの弾の速度"), Range(0, 100)]
	private float _playerBallSpeed = 0f;

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
	protected  void Update () 
	{
		BallMove();
	}

	/// <summary>
	/// プレイヤーの弾を動かす処理
	/// </summary>
	protected override void BallMove()
    {		
		// 上方向に移動
		myTransform.Translate(Vector2.up * _playerBallSpeed * Time.deltaTime);

		// 弾がステージ範囲外だったら
		if (_gameManagerScript.CheckOutStage(myTransform.position))
		{
			this.enabled = false;
		}

		base.BallMove();
    }
}