/*-------------------------------------------------
* CheckEnemyCollisionScript.cs
* 
* 作成日　2023/12/29
* 更新日　2023/12/29
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵の衝突を判定するクラス
/// </summary>
public class CheckEnemyCollisionScript : MonoBehaviour 
{
	#region 定数

	// プレイヤーの弾のTag
	private const string PLAYER_BALL = "PlayerBall";

	#endregion

	#region フィールド変数

	// 自分のTransform
	private Transform _myTransform = default;

	// 弾の個数を管理するScript
	private BallManagerScript _ballManagerScript = default;

	#endregion

	/// <summary>
	/// 更新前処理
	/// </summary>
	private void Start () 
	{
		// 自分のTransformを設定
		_myTransform = transform;

		// BallManagerScriptを取得
		_ballManagerScript 
			= GameObject.FindGameObjectWithTag("GameManager").GetComponent<BallManagerScript>();
	}

	/// <summary>
	/// 敵の衝突を判定する処理
	/// </summary>
	/// <returns>衝突判定</returns>
	public bool CheckEnemyCollision()
	{
		foreach (CircleColliderScript collisionScript
			in GameObject.FindObjectsOfType<CircleColliderScript>())
		{
			if (collisionScript.tag == PLAYER_BALL 
				&& collisionScript.CheckCircleCollision(_myTransform))
			{
				// PlayerBallMoveScriptを無効にする
				collisionScript.GetComponent<PlayerBallMoveScript>().enabled = false;

				// BallMoveScriptを取得
				BallMoveScript ballMoveScript = collisionScript.GetComponent<BallMoveScript>();

				// 弾をしまう
				_ballManagerScript.BallInput(ballMoveScript);

				// 衝突した
				return true;
			}
		}

		// 衝突してない
		return false;
	}
}