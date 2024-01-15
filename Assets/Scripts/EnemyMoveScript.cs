/*-------------------------------------------------
* EnemyMoveScript.cs
* 
* 作成日　2023/12/28
* 更新日　2024/ 1/15
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;
using System.Collections;

/// <summary>
/// 敵を動かすクラス
/// </summary>
public class EnemyMoveScript : MonoBehaviour 
{
	#region 定数

	// 敵の弾のTag
	private const string ENEMY_BALL = "EnemyBall";

	// 弾の数
	private const float EIGHT_BALL = 8;

	#endregion

	#region フィールド変数

	private float _enemyRotationSpeed = 0f;

	[SerializeField, Header("射撃のクールタイム"), Range(0,2)]
	private float _shotCoolTime = 0f;

	[SerializeField, Header("射撃ができる時間"), Range(0, 10)]
	private float _defaultCanShotTime = 0f;

	[SerializeField,Header("待機時間"), Range(0, 10)]
	private float _defaultWaitTime = 0f;

	[SerializeField, Header("敵の行動")]
	private EnemyState _enemyState = EnemyState.FIRST;

	[Space(10)]
	[Header("【第一行動】")]
	[Space(10)]

	[SerializeField, Header("敵の回転速度"), Range(-500, 500)]
	private float _firstRotationSpeed = 0f;

	[SerializeField, Header("敵の弾の速度"), Range(0, 100)]
	private float _firstBallSpeed = default;

	[SerializeField, Header("敵の弾の回転速度"), Range(-22, 22)]
	private float _firstBallRotationSpeed = default;

	

	[Space(10)]
	[Header("【第二行動】")]
	[Space(10)]

	[SerializeField, Header("敵の回転速度"), Range(-500, 500)]
	private float _secondRotationSpeed = 0f;

	[SerializeField, Header("敵の弾の速度"), Range(0, 100)]
	private float _secondBallSpeed = default;

	[SerializeField, Header("敵の弾の回転速度"), Range(-22, 22)]
	private float _secondBallRotationSpeed = default;

	[Space(10)]
	[Header("【第三行動】")]
	[Space(10)]

	[SerializeField, Header("敵の回転速度"), Range(-500, 500)]
	private float _trirdRotationSpeed = 0f;

	[SerializeField, Header("敵の弾の速度"), Range(0, 100)]
	private float _trirdBallSpeed = default;

	[SerializeField, Header("敵の弾の回転速度"), Range(-22, 22)]
	private float _trirdBallRotationSpeed = default;

	// 射撃時の経過時間
	private float _shotTime = 0f;

	// 射撃ができる経過時間
	private float _canShotTime = 0f;

	// 待機経過時間
	private float _waitTime = 0f;

	// 値を初期化したか
	private bool isValueDefault = false;

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

		// 射撃ができる時間を設定
		_canShotTime = _defaultCanShotTime;

		// 待機時間を設定
		_waitTime = _defaultWaitTime;
	}

	/// <summary>
	/// 敵を動かす処理
	/// </summary>
	public void EnemyMove()
    {
		// 敵の状態
		switch (_enemyState)
		{
			// 第一行動
			case EnemyState.FIRST:

				// 初期化してなかったら
                if (!isValueDefault)
                {
					// 初期化している
					isValueDefault = true;

					// 敵の回転速度
					_enemyRotationSpeed = _firstRotationSpeed;

					// 弾の速度を設定
					_ballManagerScript.EnemyBallSpeed = _firstBallSpeed;

					// 弾の回転速度を設定
					_ballManagerScript.EnemyBallRotationSpeed = _firstBallRotationSpeed;
                }

				if (0f < _canShotTime)
				{
					// 経過時間を減算
					_canShotTime -= Time.deltaTime;

					FirstAction();
				}
				else if (_canShotTime <= 0f)
				{
					// 時間を減算
					_waitTime -= Time.deltaTime;

					// 時間経過したら
					if (_waitTime <= 0f)
					{
						// 弾の回転を反対にする
						_ballManagerScript.EnemyBallRotationSpeed *= -1;

						// 敵の回転を反対にする
						_enemyRotationSpeed *= -1;

						// 射撃ができる時間を設定
						_canShotTime = _defaultCanShotTime;

						// 待機時間を設定
						_waitTime = _defaultWaitTime;
					}
				}
				
				break;

			// 第二行動
			case EnemyState.SECOND:

                if (!isValueDefault)
                {
					isValueDefault = true;

					// 敵の回転速度
					_enemyRotationSpeed = _secondRotationSpeed;

					// 弾の速度を設定
					_ballManagerScript.EnemyBallSpeed = _secondBallSpeed;

					// 弾の回転速度を設定
					_ballManagerScript.EnemyBallRotationSpeed = _secondBallRotationSpeed;
				}

				SecondAction();

				break;

			case EnemyState.THIRD:
				break;
		}
	}

	/// <summary>
	/// 第一行動
	/// </summary>
	private void FirstAction()
	{
		// 敵のZ軸を回転
		_myTransform.Rotate(Vector3.forward * _enemyRotationSpeed * Time.deltaTime);

		// 時間経過したら
		if (_shotTime <= 0f)
		{
			for (int i = 0; i < EIGHT_BALL; i++)
			{
				// 弾を取り出す
				_ballManagerScript.BallOutput(_myTransform.position,
				_myTransform.rotation, ENEMY_BALL);

				// 敵のZ軸を回転
				_myTransform.Rotate(Vector3.forward * 22.5f);
			}

			// 射撃のクールタイムを設定
			_shotTime = _shotCoolTime;
		}

		// 時間を減算
		_shotTime -= Time.deltaTime;
	}


    private void SecondAction()
    {
		// 時間経過したら
		if (_shotTime <= 0f)
		{
			int randomAngle = 0;

			for (int i = 0; i < 8; i++)
			{
				// 弾を取り出す
				_ballManagerScript.BallOutput(_myTransform.position,
				_myTransform.rotation, ENEMY_BALL);

				randomAngle = Random.Range(0, 180);

				// 敵のZ軸を回転
				_myTransform.Rotate(Vector3.forward * randomAngle);
			}

			// 射撃のクールタイムを設定
			_shotTime = _shotCoolTime;
		}

		// 時間を減算
		_shotTime -= Time.deltaTime;
	}
}