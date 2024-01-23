/*-------------------------------------------------
* GameManagerScript.cs
* 
* 作成日　2023/12/25
* 更新日　2023/12/27
*
* 作成者　本木大地
* 
* 
* メモ
* アップデートはおのおので書く
-------------------------------------------------*/
using UnityEngine;

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
	#endregion

	#region フィールド変数

	private float _playerBallDamage = 0f;

	// プレイヤーを動かすScript
	private PlayerMoveScript _playerMoveScript = default;

	// プレイヤーの衝突を判定するScript
	private CheckPlayerCollisionScript _checkPlayerCollisionScript = default;

	// 敵を動かすScript
	private EnemyMoveScript _enemyMoveScript = default;

	// 敵の衝突を判定するScript
	private CheckEnemyCollisionScript _checkEnemyCollisionScript = default;

	// 敵のHPを管理するScript
	private EnemyHpManagerScript _enemyHpManagerScript = default;

	private EnemyManagerScript _enemyManagerScript = default;

    #endregion

    #region プロパティ

	public float PlayerBallDamage 
	{ get => _playerBallDamage; set => _playerBallDamage = value; }

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

		// Enemyを取得
		GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

		// EnemyMoveScriptを取得
		_enemyMoveScript = enemy.GetComponent<EnemyMoveScript>();

		// CheckEnemyCollisionScriptを取得
		_checkEnemyCollisionScript = enemy.GetComponent<CheckEnemyCollisionScript>();

		// EnemyHpManagerScriptを取得
		_enemyHpManagerScript = enemy.GetComponent<EnemyHpManagerScript>();

		// EnemyManagerScriptを取得
		_enemyManagerScript = enemy.GetComponent<EnemyManagerScript>();
	}
	
	/// <summary>
    /// 更新処理
    /// </summary>
	private void Update () 
	{
		// プレイヤーが衝突したら
        if (_checkPlayerCollisionScript.CheckPlayerCollision())
        {
			Debug.LogError("ゲームオーバー");
        }
		// 敵のHpが０より上だったら
		// 敵が衝突したら
        if (0 < _enemyHpManagerScript.EnemyHp 
			&& _checkEnemyCollisionScript.CheckEnemyCollision())
        {
			// 敵のHPを減らす
			_enemyHpManagerScript.DownEnemyHp(_playerBallDamage);
        }

		_playerMoveScript.PlayerMove();
		_playerMoveScript.ReloadPlayerShot();
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
}