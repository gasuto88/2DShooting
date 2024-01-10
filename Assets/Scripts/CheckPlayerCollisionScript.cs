/*-------------------------------------------------
* CheckPlayerCollisionScript.cs
* 
* 作成日　2023/12/29
* 更新日　2023/12/29
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;

/// <summary>
/// プレイヤーの衝突を判定するクラス
/// </summary>
public class CheckPlayerCollisionScript : MonoBehaviour 
{
	#region 定数

	// 敵の弾のTag
	private const string ENEMY_BALL = "EnemyBall";
	
	// 敵のTag
	private const string ENEMY = "Enemy";

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
	/// プレイヤーの衝突を判定する処理
	/// </summary>
	/// <returns>衝突判定</returns>
	public bool CheckPlayerCollision()
    {
		foreach(CircleColliderScript collisionScript 
			in GameObject.FindObjectsOfType<CircleColliderScript>())
        {
			// 衝突したら
            if (collisionScript.CheckCircleCollision(_myTransform))
            {
				// 敵の弾だったら
				if (collisionScript.tag == ENEMY_BALL)
				{
					// EnemyBallMoveScriptを無効にする
					collisionScript.GetComponent<EnemyBallMoveScript>().enabled = false;

					// BallMoveScriptを取得
					BallMoveScript ballMoveScript = collisionScript.GetComponent<BallMoveScript>();

					// 弾をしまう
					_ballManagerScript.BallInput(ballMoveScript);

					// 衝突した
					return true;
				}
				// 敵だったら
				else if(collisionScript.tag == ENEMY)
                {
					// 衝突した
					return true;
				}
            }
        }

		// 衝突してない
		return false;
    }
}