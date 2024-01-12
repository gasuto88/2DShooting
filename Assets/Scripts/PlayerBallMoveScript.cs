/*-------------------------------------------------
* PlayerBallMoveScript.cs
* 
* 作成日　2023/12/29
* 作成日　2024/1/12
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

	private float _playerBallSpeed = 0f;

    #endregion

    #region プロパティ
	
	public float PlayerBallSpeed { get => _playerBallSpeed; set => _playerBallSpeed = value; }

    #endregion

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
    }
}