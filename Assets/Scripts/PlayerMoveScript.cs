/*-------------------------------------------------
* PlayerMoveScript.cs
* 
* 作成日　2023/12/25
* 更新日　2023/12/29
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;

/// <summary>
/// プレイヤーを動かすクラス
/// </summary>
public class PlayerMoveScript : MonoBehaviour
{
	#region 定数

	// プレイヤーの弾のTag
	private const string PLAYER_BALL = "PlayerBall";

	#endregion

	#region フィールド変数

	[SerializeField, Header("プレイヤーの移動速度"), Range(0, 100)]
	private float _moveSpeed = default;

	[SerializeField, Header("射撃のクールタイム"), Range(0, 10)]
	private float _shotCoolTime = 0f;

	// 射撃時の経過時間
	private float _shotTime = 0f;

	// プレイヤーの座標
	private Vector2 _playerPosition = default;

	// 自分のTransform
	private Transform _myTransform = default;

	// プレイヤーの射撃判定
	private bool isPlayerShot = false;

	// プレイヤーの入力を管理するScript
	private PlayerInputScript _playerInputScript = default;

	// 弾の個数を管理するSccript
	private BallManagerScript _ballManagerScript = default;

	// ゲームを管理するScript
	private GameManagerScript _gameManagerScript = default;

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
	}

	/// <summary>
	/// プレイヤーを動かす処理
	/// </summary>
	public void PlayerMove()
	{
		// 上入力判定
		if (_playerInputScript.UpInput())
		{
			_playerPosition += Vector2.up * _moveSpeed * Time.deltaTime;
		}
		// 下入力判定
		if (_playerInputScript.DownInput())
		{
			_playerPosition += Vector2.down * _moveSpeed * Time.deltaTime;
		}
		// 左入力判定
		if (_playerInputScript.LeftInput())
		{
			_playerPosition += Vector2.left * _moveSpeed * Time.deltaTime;
		}
		// 右入力判定
		if (_playerInputScript.RightInput())
		{
			_playerPosition += Vector2.right * _moveSpeed * Time.deltaTime;
		}

		// 射撃していなかったら
		// 射撃入力判定
		if (!isPlayerShot
			&& _playerInputScript.ShotInput())
		{
			// 射撃している
			isPlayerShot = true;

			// 弾を取り出す
			_ballManagerScript.BallOutput(_myTransform.position,_myTransform.rotation,PLAYER_BALL);
		}

		// 自分の座標をステージ範囲内に制限する
		_playerPosition = _gameManagerScript.ClampStageRange(_playerPosition);

		// 自分の座標を設定
		_myTransform.position = _playerPosition;
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
}