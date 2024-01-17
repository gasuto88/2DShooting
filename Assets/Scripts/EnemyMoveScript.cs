/*-------------------------------------------------
* EnemyMoveScript.cs
* 
* 作成日　2023/12/28
* 更新日　2024/ 1/17
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

	// 弾の数
	private const float EIGHT_BALL = 8;

	#endregion

	#region フィールド変数

	private float _enemyRotationSpeed = 0f;

	private float _shotCoolTime = 0f;

	[SerializeField, Header("射撃ができる時間"), Range(0, 10)]
	private float _defaultCanShotTime = 0f;

	[SerializeField,Header("待機時間"), Range(0, 10)]
	private float _defaultWaitTime = 0f;

	[SerializeField, Header("敵の行動")]
	private EnemyState _enemyState = EnemyState.FIRST;

	[SerializeField,Header("敵の移動速度"),Range(0,500)]
	private float _enemyMoveSpeed = 0f;

	[Space(10)]
	[Header(" 敵の移動経路")]
	[Space(10)]

	[SerializeField]
	private Transform _upTransform = default;
	[SerializeField]
	private Transform _downTransform = default;
	[SerializeField]
	private Transform _leftTransform = default;
	[SerializeField]
	private Transform _rightTransform = default;

	[Space(10)]
	[Header("【第一行動】")]
	[Space(10)]

	[SerializeField, Header("敵のHP"), Range(0, 1000)]
	private float _firstEnemyHp = 0f;

	[SerializeField, Header("敵の回転速度"), Range(-500, 500)]
	private float _firstRotationSpeed = 0f;

	[SerializeField, Header("敵の弾の速度"), Range(0, 100)]
	private float _firstBallSpeed = default;

	[SerializeField, Header("敵の弾の回転速度"), Range(-22, 22)]
	private float _firstBallRotationSpeed = default;

	[SerializeField, Header("敵の弾の大きさ")]
	private Vector3 _firstBallScale = default;

	[SerializeField, Header("射撃のクールタイム"), Range(0, 2)]
	private float _firstShotCoolTime = default;

	[Space(10)]
	[Header("【第二行動】")]
	[Space(10)]

	[SerializeField, Header("敵のHP"), Range(0, 1000)]
	private float _secondEnemyHp = 0f;

	[SerializeField, Header("敵の回転速度"), Range(-500, 500)]
	private float _secondRotationSpeed = 0f;

	[SerializeField, Header("敵の弾の速度"), Range(0, 100)]
	private float _secondBallSpeed = default;

	[SerializeField, Header("敵の弾の回転速度"), Range(-22, 22)]
	private float _secondBallRotationSpeed = default;

	[SerializeField, Header("敵の弾の大きさ")]
	private Vector3 _secondBallScale = default;

	[SerializeField, Header("射撃のクールタイム"), Range(0, 2)]
	private float _secondShotCoolTime = default;

	[Space(10)]
	[Header("【第三行動】")]
	[Space(10)]

	[SerializeField, Header("敵のHP"), Range(0, 1000)]
	private float _thirdEnemyHp = 0f;

	[SerializeField, Header("敵の回転速度"), Range(-500, 500)]
	private float _thirdRotationSpeed = 0f;

	[SerializeField, Header("敵の弾の速度"), Range(0, 100)]
	private float _thirdBallSpeed = default;

	[SerializeField, Header("敵の弾の回転速度"), Range(-22, 22)]
	private float _thirdBallRotationSpeed = default;

	[SerializeField, Header("敵の弾の大きさ")]
	private Vector3 _thirdBallScale = default;

	[SerializeField, Header("射撃のクールタイム"), Range(0, 2)]
	private float _thirdShotCoolTime = default;

	// 射撃時の経過時間
	private float _shotTime = 0f;

	// 射撃ができる経過時間
	private float _canShotTime = 0f;

	// 待機経過時間
	private float _waitTime = 0f;

	private int _index = 1;

	// 値の初期化判定
	private bool isDefaultValue = false;

	// 自分のTransform
	private Transform _myTransform = default;

	private Vector3 _targetPosition = default;

	// ゲームを管理するScript
	private GameManagerScript _gameManagerScript = default;

	// 弾の個数を管理するSccript
	private BallManagerScript _ballManagerScript = default;

	// 敵のHpを管理するScript
	private EnemyHpManagerScript _enemyHpManagerScript = default;

	// プレイヤーを動かすScript
	private PlayerMoveScript _playerMoveScript = default;

	private Vector3[] _moveRootPositions = default;

	public enum EnemyState
    {
		FIRST,
		SECOND,
		THIRD,
		CRUSH,
		STOP
    }

    #endregion

    #region プロパティ

	public EnemyState EnemyType { get => _enemyState; set => _enemyState = value; }

	public bool IsDefaultValue { get => isDefaultValue; set => isDefaultValue = value; }

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

		// EnemyHpManagerScriptを取得
		_enemyHpManagerScript = GetComponent<EnemyHpManagerScript>();

		// PlayerMoveScriptを取得
		_playerMoveScript
			= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoveScript>();

		// 射撃ができる時間を設定
		_canShotTime = _defaultCanShotTime;

		// 待機時間を設定
		_waitTime = _defaultWaitTime;

		_moveRootPositions 
			= new Vector3[] { _upTransform.position,_leftTransform.position,
				_downTransform.position, _rightTransform.position};
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
                if (!isDefaultValue)
                {
					// 初期化した
					isDefaultValue = true;

					// 敵のHpを設定
					_enemyHpManagerScript.EnemyHp = _firstEnemyHp;

					// 敵の回転速度
					_enemyRotationSpeed = _firstRotationSpeed;

					// 弾の速度を設定
					_ballManagerScript.EnemyBallSpeed = _firstBallSpeed;

					// 弾の回転速度を設定
					_ballManagerScript.EnemyBallRotationSpeed = _firstBallRotationSpeed;

					// 弾の大きさを設定
					_ballManagerScript.BallScale = _firstBallScale;

					// 射撃のクールタイムを設定
					_shotCoolTime = _firstShotCoolTime;

					_targetPosition = _moveRootPositions[_index];
                }

				if (0f < _canShotTime)
				{
					// 経過時間を減算
					_canShotTime -= Time.deltaTime;

					FirstAction();
				}
				else if (_canShotTime <= 0f)
				{
					
					Vector2 target = _targetPosition - _myTransform.position;

					_myTransform.Translate(
						target * _enemyMoveSpeed * Time.deltaTime, Space.World);

					// 時間を減算
					_waitTime -= Time.deltaTime;

					// 時間経過したら
					// 指定場所に着いたら
					if (_waitTime <= 0f
						&& CheckArrivePosition(_targetPosition, _myTransform.position))
					{
						// 弾の回転を反対にする
						_ballManagerScript.EnemyBallRotationSpeed *= -1;

						// 敵の回転を反対にする
						_enemyRotationSpeed *= -1;

						_index++;

						if (_index == 0)
						{
							_targetPosition = _moveRootPositions[_index];
						}
						else if (_index == 1)
						{
							_targetPosition = _moveRootPositions[_index];
						}
						else if (_index == 2)
						{
							_targetPosition = _moveRootPositions[_index];
						}
						if (_index == 3)
						{
							_targetPosition = _moveRootPositions[_index];

							_index = -1;
						}

						// 射撃ができる時間を設定
						_canShotTime = _defaultCanShotTime;

						// 待機時間を設定
						_waitTime = _defaultWaitTime;				
					}
				}
				
				break;

			// 第二行動
			case EnemyState.SECOND:

				// 初期化してなかったら
				if (!isDefaultValue)
                {
					// 初期化した
					isDefaultValue = true;

					// 敵のHpを設定
					_enemyHpManagerScript.EnemyHp = _secondEnemyHp;

					// 敵の回転速度
					_enemyRotationSpeed = _secondRotationSpeed;

					// 弾の速度を設定
					_ballManagerScript.EnemyBallSpeed = _secondBallSpeed;

					// 弾の回転速度を設定
					_ballManagerScript.EnemyBallRotationSpeed = _secondBallRotationSpeed;

					// 弾の大きさを設定
					_ballManagerScript.BallScale = _secondBallScale;

					// 射撃のクールタイムを設定
					_shotCoolTime = _secondShotCoolTime;
				}

				SecondAction();

				break;
			// 第三行動
			case EnemyState.THIRD:

				// 初期化してなかったら
				if (!isDefaultValue)
				{
					// 初期化した
					isDefaultValue = true;

					// 敵のHpを設定
					_enemyHpManagerScript.EnemyHp = _thirdEnemyHp;

					// 敵の回転速度
					_enemyRotationSpeed = _thirdRotationSpeed;

					// 弾の速度を設定
					_ballManagerScript.EnemyBallSpeed = _thirdBallSpeed;

					// 弾の回転速度を設定
					_ballManagerScript.EnemyBallRotationSpeed = _thirdBallRotationSpeed;

					// 弾の大きさを設定
					_ballManagerScript.BallScale = _thirdBallScale;

					// 射撃のクールタイムを設定
					_shotCoolTime = _thirdShotCoolTime;
				}

				//if (0f < _canShotTime)
				//{
				//	// 経過時間を減算
				//	_canShotTime -= Time.deltaTime;

					ThirdAction();
				//}
				//else if (_canShotTime <= 0f)
				//{
				//	// 時間を減算
				//	_waitTime -= Time.deltaTime;

				//	// 時間経過したら
				//	if (_waitTime <= 0f)
				//	{
				//		// 弾の回転を反対にする
				//		_ballManagerScript.EnemyBallRotationSpeed *= -1;

				//		// 敵の回転を反対にする
				//		_enemyRotationSpeed *= -1;

				//		// 射撃ができる時間を設定
				//		_canShotTime = _defaultCanShotTime;

				//		// 待機時間を設定
				//		_waitTime = _defaultWaitTime;
				//	}
				//}
				break;

			// 撃破
			case EnemyState.CRUSH:

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

	/// <summary>
	/// 第二行動
	/// </summary>
    private void SecondAction()
    {
		// 時間経過したら
		if (_shotTime <= 0f)
		{
			int randomAngle = 0;

			// 弾を取り出す
			_ballManagerScript.BallOutput(_myTransform.position,
			_myTransform.rotation, ENEMY_BALL);

			randomAngle = Random.Range(0, 180);

			// 敵のZ軸を回転
			_myTransform.Rotate(Vector3.forward * randomAngle);

			// 射撃のクールタイムを設定
			_shotTime = _shotCoolTime;
		}

		// 時間を減算
		_shotTime -= Time.deltaTime;
	}

	/// <summary>
	/// 第三行動
	/// </summary>
	private void ThirdAction()
	{
		//// 敵のZ軸を回転
		//_myTransform.Rotate(Vector3.forward * _enemyRotationSpeed * Time.deltaTime);

		// 時間経過したら
		if (_shotTime <= 0f)
		{
            //float angleSpace = 1f;

            for (int i = 0; i < 3; i++)
            {

                // 弾を取り出す
                _ballManagerScript.BallOutput(_myTransform.position,
				_myTransform.rotation, ENEMY_BALL);

				// 敵のZ軸を回転
				_myTransform.Rotate(Vector3.forward * 5f);
			}

			// 射撃のクールタイムを設定
			_shotTime = _shotCoolTime;
		}

		// 時間を減算
		_shotTime -= Time.deltaTime;
	}

	private bool CheckArrivePosition(Vector3 goalPos,Vector3 movePos)
    {
		if ((-1f < goalPos.x - movePos.x && goalPos.x - movePos.x < 1f)
			&& (-1f < goalPos.y - movePos.y && goalPos.y - movePos.y < 1f))
        {
			return true;
        }

		return false;
    }
}