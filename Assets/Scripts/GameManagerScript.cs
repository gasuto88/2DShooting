/*-------------------------------------------------
* GameManagerScript.cs
* 
* 作成日　2023/12/25
* 更新日　2023/12/25
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

	private const float STAGE_HEIGHT_MAX = 22.5f;
	private const float STAGE_HEIGHT_MIN = -22.5f;
	private const float STAGE_WIDTH_MAX = 12.5f;
	private const float STAGE_WIDTH_MIN = -12.5f;

    #endregion

    #region フィールド変数

    // プレイヤーを動かすScript
    private PlayerMoveScript _playerMoveScript = default;

	// プレイヤーの入力を管理するScript
	private PlayerInputScript _playerInputScript = default;

	#endregion

	/// <summary>
    /// 更新前処理
    /// </summary>
	private void Start () 
	{
		// PlayerをTagから取得
		GameObject player = GameObject.FindGameObjectWithTag("Player");

		// PlayerMoveScriptを取得
		_playerMoveScript = player.GetComponent<PlayerMoveScript>();

		// PlayerInputScriptを取得
		_playerInputScript = player.GetComponent<PlayerInputScript>();
	}
	
	/// <summary>
    /// 更新処理
    /// </summary>
	private void Update () 
	{
		_playerMoveScript.PlayerMove();
		_playerInputScript.ShotCoolTime();
	}

	/// <summary>
	/// ステージ外かを判定する処理
	/// </summary>
	/// <param name="position">判定する座標</param>
	/// <returns>ステージ外判定</returns>
	public bool CheckOutStage(Vector3 position)
    {
		// ステージ範囲外だったら
		if(STAGE_HEIGHT_MAX < position.x || position.x < STAGE_HEIGHT_MIN 
			|| STAGE_WIDTH_MAX < position.y || position.y < STAGE_WIDTH_MIN)
        {
			return true;
        }

		return false;
    }
}