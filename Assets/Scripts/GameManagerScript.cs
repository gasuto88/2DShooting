/*-------------------------------------------------
* GameManagerScript.cs
* 
* 作成日　2023/12/25
* 更新日　2024/ 2/ 1
*
* 作成者　本木大地
-------------------------------------------------*/
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲームを管理するクラス
/// </summary>
public class GameManagerScript : MonoBehaviour 
{
	#region 定数

	// ステージの幅 -------------------------------
	private const float STAGE_HEIGHT_MAX = 19f;
	private const float STAGE_HEIGHT_MIN = -19f;
	private const float STAGE_WIDTH_MAX = 19f;
	private const float STAGE_WIDTH_MIN = -19f;
	// --------------------------------------------

	// タイトルシーン
	private const string TITLE_SCENE = "TitleScene";

	// 遅延時間
	private const float DELAY_TIME = 1f;

	#endregion

	#region フィールド変数

	[SerializeField,Header("ゲームオーバーの降ってくる速度"),Range(0,100)]
	private float _gameOverSpeed = 0f;

	// プレイヤーの弾のダメージ
	private float _playerBallDamage = 0f;

	// ゲーム開始判定
	private bool isGameStart = false;

	// ゲーム終了判定
	private bool isGameOver = false;

	// ゲームクリア判定
	private bool isGameClear = false;

	// プレイヤーを動かすクラス
	private PlayerMoveScript _playerMoveScript = default;

	// プレイヤーのHpを管理するクラス
	private PlayerHpManagerScript _playerHpManagerScript = default;

	// プレイヤーの衝突を判定するクラス
	private CheckPlayerCollisionScript _checkPlayerCollisionScript = default;

	// 敵の衝突を判定するクラス
	private CheckEnemyCollisionScript _checkEnemyCollisionScript = default;

	// 敵のHPを管理するクラス
	private EnemyHpManagerScript _enemyHpManagerScript = default;

	// 敵を管理するクラス
	private EnemyManagerScript _enemyManagerScript = default;

	// 敵を点滅させるクラス
	private FlashingEnemyScript _flashingEnemyScript = default;

	// フェードアウトするクラス
	private PanelFadeOutScript _fadeOutScript = default;

	// メニュー
	private Canvas _menuCanvas = default;

	// リザルト文字
	private Transform _gameOverText = default;
	private Transform _gameClearText = default;

	// 黒色
	private Color _black = Color.black;
	
	// 白色
	private Color _white = Color.white;

	#endregion

	#region プロパティ

	public float PlayerBallDamage 
	{ get => _playerBallDamage; set => _playerBallDamage = value; }

	public bool IsGameStart { get => isGameStart; set => isGameStart = value; }

	public bool IsGameOver { get => isGameOver; set => isGameOver = value; }

	public bool IsGameClear { get => isGameClear; set => isGameClear = value; }

    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start () 
	{
		// Playerを取得
		GameObject player = GameObject.FindGameObjectWithTag("Player");

		// PlayerMoveScriptを取得
		_playerMoveScript = player.GetComponent<PlayerMoveScript>();

		// CheckPlayerCollisionScriptを取得
		_checkPlayerCollisionScript = player.GetComponent<CheckPlayerCollisionScript>();

		// PlayerHpManagerScript
		_playerHpManagerScript = player.GetComponent<PlayerHpManagerScript>();

		// Enemyを取得
		GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

		// CheckEnemyCollisionScriptを取得
		_checkEnemyCollisionScript = enemy.GetComponent<CheckEnemyCollisionScript>();

		// EnemyHpManagerScriptを取得
		_enemyHpManagerScript = enemy.GetComponent<EnemyHpManagerScript>();

		// EnemyManagerScriptを取得
		_enemyManagerScript = enemy.GetComponent<EnemyManagerScript>();

		// FlashingEnemyScriptを取得
		_flashingEnemyScript = enemy.GetComponent<FlashingEnemyScript>();

		// メニューを取得
		_menuCanvas = GameObject.FindGameObjectWithTag("Menu").GetComponent<Canvas>();

		// メニューを不可視化
		_menuCanvas.enabled = false;

		// リザルト文字を取得
		_gameOverText = GameObject.FindGameObjectWithTag("GameOver").transform;
		_gameClearText = GameObject.FindGameObjectWithTag("GameClear").transform;

		// PanelFadeOutScriptを取得
		_fadeOutScript = GetComponent<PanelFadeOutScript>();
	}
	
	/// <summary>
    /// 更新処理
    /// </summary>
	private void Update () 
	{
		if (!isGameOver && !isGameClear)
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				// メニューを表示
				DisplayMenu();
			}

			// 点滅終了判定
            // プレイヤーが衝突したら
            if (!_playerMoveScript.IsFlashingEnd
				&& !_playerHpManagerScript.IsFlashingEnd
				&& !_playerHpManagerScript.IsDamageEfectEnd
				&& _checkPlayerCollisionScript.CheckPlayerCollision())
            {
                // 点滅終了判定
                _playerMoveScript.IsFlashingEnd = true;
				_playerHpManagerScript.IsFlashingEnd = true;
				_playerHpManagerScript.IsDamageEfectEnd = true;
			}

			// プレイヤーの体力が０以上
			if (0 < _playerHpManagerScript.PlayerLife)
            {
                if (_playerMoveScript.IsFlashingEnd)
                {
					// プレイヤーの点滅処理
					_playerMoveScript.FlashingPlayer();
				}
                if (_playerHpManagerScript.IsFlashingEnd)
                {
					// プレイヤー残機の点滅処理
					_playerHpManagerScript.FlashingLifeUI();
				}
				if (_playerHpManagerScript.IsDamageEfectEnd)
				{
					// ダメージエフェクトの点滅処理
					_playerHpManagerScript.FlashingDamageEfect();
				}

			}

            // 敵のHpが０より上だったら
            // 敵が衝突したら
            if (0 < _enemyHpManagerScript.EnemyHp
				&& _checkEnemyCollisionScript.CheckEnemyCollision())
			{
                if (!_flashingEnemyScript.IsFlashing)
                {
					// 敵の点滅終了判定
					_flashingEnemyScript.IsFlashing = true;
                }

				// 敵のHPを減らす
				_enemyHpManagerScript.DownEnemyHp(_playerBallDamage);
			}

			if (_flashingEnemyScript.IsFlashing)
			{
				// 敵の点滅処理
				_flashingEnemyScript.FlashingEnemy();
			}


			// ゲームが開始したら
			if (isGameStart)
			{
				_playerMoveScript.PlayerMove();
			}
		}

        // ゲームが終了したら
        if (isGameOver && !isGameClear)
        {
			Debug.LogError("しんだ");
			// ゲームオーバーを表示
			DisplayGameOver();

			StartCoroutine(ResultCoroutine(_black));
        }

		// ゲームをクリアしたら
        if (isGameClear && !isGameOver)
        {
			DisplayGameClear();

			StartCoroutine(ResultCoroutine(_white));
		}

		// 敵を制御する
		_enemyManagerScript.EnemyControll();
	}

	/// <summary>
	/// ステージ範囲外かを判定する処理
	/// </summary>
	/// <param name="position">判定する座標</param>
	/// <returns>ステージ外判定</returns>
	public bool CheckOutStage(Vector3 position)
    {
		// ステージ範囲外だったら
		if(position.x < STAGE_WIDTH_MIN || STAGE_WIDTH_MAX < position.x 
			|| position.y < STAGE_HEIGHT_MIN || STAGE_HEIGHT_MAX < position.y)
        {
			return true;
        }

		return false;
    }

	/// <summary>
	/// ステージ範囲を制限する処理
	/// </summary>
	/// <param name="position">制限する座標</param>
	/// <returns>制限した座標</returns>
	public Vector3 ClampStageRange(Vector3 position)
    {
		// 縦幅の範囲を超えていたら、ステージ内に戻す
		if (position.x < STAGE_WIDTH_MIN)
		{
			position.x = STAGE_WIDTH_MIN;
		}
		else if (STAGE_WIDTH_MAX < position.x)
		{
			position.x = STAGE_WIDTH_MAX;
		}

		// 横幅の範囲を超えていたら、ステージ内に戻す
		if (position.y < STAGE_HEIGHT_MIN)
		{
			position.y = STAGE_HEIGHT_MIN;
		}
		else if (STAGE_HEIGHT_MAX < position.y)
		{
			position.y = STAGE_HEIGHT_MAX;
		}

		return position;
	}

	/// <summary>
	/// メニューを表示する処理
	/// </summary>
	private void DisplayMenu()
    {
		if (!_menuCanvas.enabled)
		{
			// 可視化
			_menuCanvas.enabled = true;

			// 時間停止
			Time.timeScale = 0;
		}
		else if (_menuCanvas.enabled)
        {
			// ゲームを続ける
			OnResume();
		}
    }

	/// <summary>
	/// ゲームオーバーを表示する処理
	/// </summary>
	public void DisplayGameOver()
    {
		if (0 <= _gameOverText.position.y)
		{
			_gameOverText.Translate(Vector2.down * _gameOverSpeed * Time.deltaTime);
		}
    }

	/// <summary>
	/// ゲームクリアを表示する処理
	/// </summary>
	public void DisplayGameClear()
    {
		if (0 <= _gameClearText.position.y)
		{
			_gameClearText.Translate(Vector2.down * _gameOverSpeed * Time.deltaTime);
		}
	}

	/// <summary>
	/// ゲームを続ける
	/// </summary>
	public void OnResume()
    {
		// 不可視化
		_menuCanvas.enabled = false;

		// 時間開始
		Time.timeScale = 1;
	}

	/// <summary>
	/// ゲームを終わる
	/// </summary>
	public void OnExit()
    {
		// 時間開始
		Time.timeScale = 1;

		SceneManager.LoadScene(TITLE_SCENE);
    }

	/// <summary>
	/// リザルト時の遅延処理
	/// </summary>
	private IEnumerator ResultCoroutine(Color color)
    {
		// 遅延
		yield return new WaitForSeconds(DELAY_TIME);

		// フェードアウト
		_fadeOutScript.PanelFadeOut(color);
	}
}