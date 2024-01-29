/*-------------------------------------------------
* PlayerMoveScript.cs
* 
* 作成日　2023/12/25
* 更新日　2023/12/29
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;
using System.Collections;

/// <summary>
/// プレイヤーを動かす
/// </summary>
public class PlayerMoveScript : MonoBehaviour
{
	#region 定数

	// プレイヤーの弾のTag
	private const string PLAYER_BALL = "PlayerBall";

	// 弾の数
	private const int THREE_BALL = 3;
	
    // 残機の透明度
    private const float MAX_ALPHA = 1;
    private const float MIN_ALPHA = 0;


	#endregion

	#region フィールド変数

	[SerializeField, Header("プレイヤーの移動速度"), Range(0, 100)]
	private float _moveSpeed = default;

	[SerializeField, Header("プレイヤーの加速速度"), Range(0, 1000)]
	private float _accelSpeed = default;

	[SerializeField, Header("プレイヤーの弾のダメージ"), Range(0, 1000)]
	private float _playerBallDamage = 0f;

	[SerializeField, Header("プレイヤーの弾の速度"), Range(0, 100)]
	private float _playerBallSpeed = 0f;

	[SerializeField, Header("プレイヤーの弾の大きさ")]
	private Vector3 _playerBallScale = default;

	[SerializeField, Header("射撃のクールタイム"), Range(0, 10)]
	private float _shotCoolTime = 0f;

	[Space(10)]
	[Header("射撃位置")]
	[Space(10)]

	[SerializeField]
	private Transform _centerShotPos = default;
	[SerializeField]
	private Transform _leftShotPos = default;
	[SerializeField]
	private Transform _rightShotPos = default;

	[SerializeField, Header("被弾時のの点滅速度"), Range(0, 10)]
	private float _flashSpeed = 0f;

	[SerializeField, Header("被弾時の点滅回数"), Range(0, 10)]
	private float _flashMaxCount = 0f;

	// プレイヤーの移動速度 ----------------
	private float _moveUpSpeed = 0f;
	private float _moveDownSpeed = 0f;
	private float _moveLeftSpeed = 0f;
	private float _moveRightSpeed = 0f;
	// -------------------------------------

	// 射撃時の経過時間
	private float _shotTime = 0f;

	// プレイヤーの座標
	private Vector2 _playerPosition = default;

	// 射撃位置
	private Transform[] _shotPositions = default;

	// 自分のTransform
	private Transform _myTransform = default;

	// プレイヤーの射撃判定
	private bool isPlayerShot = false;

	// 衝突判定
	private bool isCollision = false;

	// プレイヤーの入力を管理するScript
	private PlayerInputScript _playerInputScript = default;

	// 弾の個数を管理するSccript
	private BallManagerScript _ballManagerScript = default;

	// ゲームを管理するScript
	private GameManagerScript _gameManagerScript = default;

    #endregion

    #region プロパティ

	public Vector3 PlayerPosition { get => _playerPosition; set => _playerPosition = value; }

	public bool IsCollision { get => isCollision; set => isCollision = value; }

    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start()
	{
		//自分のTransformを設定
		_myTransform = transform;

		//自分の座標を設定
		_playerPosition = _myTransform.position;

		// 射撃位置を設定
		_shotPositions 
			= new Transform[] 
			{_centerShotPos,_leftShotPos,_rightShotPos};

		// 射撃のクールタイムを設定
		_shotTime = _shotCoolTime;

		// PlayerInputScriptを取得
		_playerInputScript = GetComponent<PlayerInputScript>();

		// GameManagerを取得
		GameObject gameMane = GameObject.FindGameObjectWithTag("GameManager");

		// GameManagerScriptを取得
		_gameManagerScript = gameMane.GetComponent<GameManagerScript>();

		// BallManagerScriptを取得
		_ballManagerScript = gameMane.GetComponent<BallManagerScript>();

		// 弾の速度を設定
		_ballManagerScript.PLayerBallSpeed = _playerBallSpeed;

		// 弾の大きさを設定
		_ballManagerScript.PlayerBallScale = _playerBallScale;

		// 弾の威力
		_gameManagerScript.PlayerBallDamage = _playerBallDamage;
	}

    /// <summary>
    /// プレイヤーを動かす処理
    /// </summary>
    public void PlayerMove()
	{
		// 上入力判定
		if (_playerInputScript.UpInput())
		{
			// 速度を滑らかに増やす
			_moveUpSpeed = SmoothValueUp(_moveUpSpeed, _accelSpeed, _moveSpeed);

			// 上移動
			_playerPosition += Vector2.up * _moveUpSpeed * Time.deltaTime;
		}
        else
        {
			// 初期化
			_moveUpSpeed = 0f;
        }
		// 下入力判定
		if (_playerInputScript.DownInput())
		{
			// 速度を滑らかに増やす
			_moveDownSpeed = SmoothValueUp(_moveDownSpeed, _accelSpeed, _moveSpeed);

			// 下移動
			_playerPosition += Vector2.down * _moveDownSpeed * Time.deltaTime;
		}
		else
		{
			// 初期化
			_moveDownSpeed = 0f;
		}
		// 左入力判定
		if (_playerInputScript.LeftInput())
		{
			// 速度を滑らかに増やす
			_moveLeftSpeed = SmoothValueUp(_moveLeftSpeed, _accelSpeed, _moveSpeed);

			// 左移動
			_playerPosition += Vector2.left * _moveLeftSpeed * Time.deltaTime;
		}
		else
		{
			// 初期化
			_moveLeftSpeed = 0f;
		}
		// 右入力判定
		if (_playerInputScript.RightInput())
		{
			// 速度を滑らかに増やす
			_moveRightSpeed = SmoothValueUp(_moveRightSpeed, _accelSpeed, _moveSpeed);

			// 右移動
			_playerPosition += Vector2.right * _moveRightSpeed * Time.deltaTime;
		}
		else
		{
			// 初期化
			_moveRightSpeed = 0f;
		}

		// 射撃していなかったら
		// 射撃入力判定
		if (!isPlayerShot)
		{
			// 射撃している
			isPlayerShot = true;

			// 弾を３つ取り出す
			for (int i = 0; i < THREE_BALL; i++)
			{
				// ステージ範囲内だったら
				if (!_gameManagerScript.CheckOutStage(_shotPositions[i].position))
				{
					// 弾を取り出す
					_ballManagerScript.BallOutput
						(_shotPositions[i].position, _myTransform.rotation, PLAYER_BALL);
				}
			}
		}

		// 自分の座標をステージ範囲内に制限する
		_playerPosition = _gameManagerScript.ClampStageRange(_playerPosition);

		// 自分の座標を設定
		_myTransform.position = _playerPosition;
	}

	/// <summary>
	/// 値を滑らかに増やす処理
	/// </summary>
	/// <param name="value">増やす値</param>
	/// <param name="speed">加速値</param>
	/// <param name="maxValue">最大値</param>
	/// <returns>増やした値</returns>
	private float SmoothValueUp(float value, float speed, float maxValue)
    {
		// 最大値じゃないとき
		if (value < maxValue)
		{
			// 速度を加算する
			value += speed * Time.deltaTime;
		}
		// 最大値以上だったら
		else if(maxValue <= value)
        {
			// 最大値を設定
			value = maxValue;
        }

		return value;
    }

	/// <summary>
	/// 射撃のクールタイム処理
	/// </summary>
	public void ReloadPlayerShot()
	{
		if (isPlayerShot)
		{
			_shotTime -= Time.deltaTime;

			if (_shotTime <= 0)
			{
				// 射撃していない
				isPlayerShot = false;

				// 射撃のクールタイムを設定
				_shotTime = _shotCoolTime;
			}
		}
	}

	//public IEnumerator FlashCoroutine()
 //   {
	//	Debug.LogWarning("点滅");

	//	int flashCount = 0;

	//	_flashSpeed *= -1;

	//	SpriteRenderer sprite = GetComponent<SpriteRenderer>();

	//	Color black = Color.black;

 //       while (flashCount < _flashMaxCount)
 //       {		
	//		sprite.color += black * _flashSpeed * Time.deltaTime;

	//		if(sprite.color.a <= 0f)
 //           {
	//			sprite.color = black;

	//			_flashSpeed *= -1;
 //           }
	//		else if(1f <= sprite.color.a)
 //           {
	//			_flashSpeed *= -1;
	//		}
 //       }
		
 //   }
}