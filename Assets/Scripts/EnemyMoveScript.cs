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

	// 敵の弾のTag
	private const string ENEMY_BALL = "EnemyBall";

	#endregion

	#region フィールド変数

	[SerializeField,Header("敵の回転速度"),Range(-500,500)]
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

	private enum EnemyState
    {
		FIRST,
		SECOND,
		THIRD,
		STOP
    }

	private EnemyState _enemyState = EnemyState.FIRST;

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
		switch (_enemyState)
		{
			case EnemyState.FIRST:

				FirstMove();

				break;

			case EnemyState.SECOND:

				SecondMove();

				break;

			case EnemyState.THIRD:
				break;
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

	private void FirstMove()
    {
		_myTransform.Rotate(Vector3.forward * _enemyRotationSpeed * Time.deltaTime);

		// 射撃していなかったら
		// 射撃入力判定
		if (!isEnemyShot)
		{
			// 射撃している
			isEnemyShot = true;

			for (int i = 0; i < 8; i++)
			{
				// 弾を取り出す
					_ballManagerScript.BallOutput(_myTransform.position,
					_myTransform.rotation,ENEMY_BALL);				

				_myTransform.Rotate(Vector3.forward * 22.5f);
			}
		}
	}

	private void SecondMove()
	{
		_myTransform.Rotate(Vector3.forward * _enemyRotationSpeed * Time.deltaTime);

		// 射撃していなかったら
		// 射撃入力判定
		if (!isEnemyShot)
		{
			// 射撃している
			isEnemyShot = true;

			for (int i = 0; i < 8; i++)
			{
				// 弾を取り出す
				_ballManagerScript.BallOutput(_myTransform.position,
				_myTransform.rotation, ENEMY_BALL);

				_myTransform.Rotate(Vector3.forward * 22.5f);
			}
		}
	}
}