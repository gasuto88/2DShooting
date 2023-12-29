/*-------------------------------------------------
* EnemyMoveScript.cs
* 
* 作成日　2023/12/28
* 更新日　2023/12/28
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;

/// <summary>
/// 敵を動かすクラス
/// </summary>
public class EnemyMoveScript : MonoBehaviour 
{
	#region 定数

	// 敵のTag
	private const string ENEMY = "Enemy";

    #endregion

    #region フィールド変数

    [SerializeField,Header("敵の回転速度"),Range(0,1000)]
	private float _enemyRotationSpeed = default;

	[SerializeField, Header("射撃のクールタイム"), Range(0,2)]
	private float _shotCoolTime = 0f;

	// 射撃時の経過時間
	private float _shotTime = 0f;

	// 敵の射撃判定
	private bool isEnemyShot = false;

	// 自分のTransform
	private Transform _myTransform = default;

	// ゲームを管理するScript
	private GameManagerScript _gameManagerScript = default;

	// 弾の個数を管理するSccript
	private BallManagerScript _ballManagerScript = default;

	#endregion

	/// <summary>
    /// 更新前処理
    /// </summary>
	private void Start () 
	{
		//自分のTransformを設定
		_myTransform = transform;

		// GameManagerを取得
		GameObject gameMane = GameObject.FindGameObjectWithTag("GameManager");

		// GameManagerScriptを取得
		_gameManagerScript = gameMane.GetComponent<GameManagerScript>();

		// BallManagerScriptを取得
		_ballManagerScript = gameMane.GetComponent<BallManagerScript>();
	}

	/// <summary>
	/// 敵を動かす処理
	/// </summary>
	public void EnemyMove()
    {
		_myTransform.Rotate(Vector3.forward * _enemyRotationSpeed * Time.deltaTime);

		// 射撃していなかったら
		// 射撃入力判定
		if (!isEnemyShot)
		{
			// 射撃している
			isEnemyShot = true;

			// 弾を取り出す
			BallMoveScript tempScript = _ballManagerScript.BallOutput(_myTransform.position,_myTransform.rotation);

			// 弾にEnemyBallMoveScriptを設定
			tempScript.GetComponent<EnemyBallMoveScript>().enabled = true;

			// TagをEnemyに設定
			tempScript.tag = ENEMY;
		}
	}

	/// <summary>
	/// 射撃のクールタイム処理
	/// </summary>
	public void ReloadEnemyShot()
	{
		if (isEnemyShot)
		{
			_shotTime -= Time.deltaTime;

			if (_shotTime <= 0)
			{
				// 射撃していない
				isEnemyShot = false;

				// 射撃のクールタイムを設定
				_shotTime = _shotCoolTime;
			}
		}
	}
}