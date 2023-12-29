/*-------------------------------------------------
* GameManagerScript.cs
* 
* 作成日　2023/12/25
* 更新日　2023/12/27
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;

/// <summary>
/// ゲームを管理するクラス
/// </summary>
public class GameManagerScript : MonoBehaviour 
{
	#region 定数

	// ステージの幅 -------------------------------
	private const float STAGE_HEIGHT_MAX = 12.5f;
	private const float STAGE_HEIGHT_MIN = -12.5f;
	private const float STAGE_WIDTH_MAX = 12.5f;
	private const float STAGE_WIDTH_MIN = -12.5f;
	// --------------------------------------------
    #endregion

    #region フィールド変数

    // プレイヤーを動かすScript
    private PlayerMoveScript _playerMoveScript = default;

	// プレイヤーの入力を管理するScript
	private PlayerInputScript _playerInputScript = default;

	// プレイヤーの衝突を判定するScript
	private CheckPlayerCollisionScript _checkPlayerCollisionScript = default;

	// 敵を動かすScript
	private EnemyMoveScript _enemyMoveScript = default;

	// 敵の衝突を判定するScript
	private CheckEnemyCollisionScript _checkEnemyCollisionScript = default;

	// 敵のHPを管理するScript
	private EnemyHpManagerScript _enemyHpManagerScript = default;


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

		// PlayerInputScriptを取得
		_playerInputScript = player.GetComponent<PlayerInputScript>();

		// CheckPlayerCollisionScriptを取得
		_checkPlayerCollisionScript = player.GetComponent<CheckPlayerCollisionScript>();

		GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

		// EnemyMoveScriptを取得
		_enemyMoveScript = enemy.GetComponent<EnemyMoveScript>();

		// CheckEnemyCollisionScriptを取得
		_checkEnemyCollisionScript = enemy.GetComponent<CheckEnemyCollisionScript>();

		// EnemyHpManagerScriptを取得
		_enemyHpManagerScript = enemy.GetComponent<EnemyHpManagerScript>();
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

		// 敵が衝突したら
        if (_checkEnemyCollisionScript.CheckEnemyCollision())
        {
			Debug.LogError("当たった");
			//_enemyHpManagerScript.DownEnemyHp();
        }

		_playerMoveScript.PlayerMove();
		_playerMoveScript.ReloadPlayerShot();
		_enemyMoveScript.EnemyMove();
		_enemyMoveScript.ReloadEnemyShot();
	}

	/// <summary>
	/// ステージ範囲外かを判定する処理
	/// </summary>
	/// <param name="position">判定する座標</param>
	/// <returns>ステージ外判定</returns>
	public bool CheckOutStage(Vector3 position)
    {
		// ステージ範囲外だったら
		if(position.x < STAGE_HEIGHT_MIN || STAGE_HEIGHT_MAX < position.x 
			|| position.y < STAGE_WIDTH_MIN || STAGE_WIDTH_MAX < position.y)
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