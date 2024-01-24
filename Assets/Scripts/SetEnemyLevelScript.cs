/*-------------------------------------------------
* SetStageLevelScript.cs
* 
* 作成日　2024/ 1/23
* 更新日　2024/ 1/23
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// 敵のレベルを設定する
/// </summary>
public class SetEnemyLevelScript : MonoBehaviour 
{

	#region 定数

	// 敵のレベル ------------------------------
	private const int LEVEL_ZERO = 0;
	private const int LEVEL_OWE = 1;
	private const int LEVEL_TWO = 2;
	private const int LEVEL_THREE = 3;
	// -----------------------------------------

	// 敵
	private const string ENEMY = "Enemy";

	#endregion

	#region フィールド変数

	[SerializeField]
	private string _sceneName = "";

	// 敵のレベル
	private int _enemyLevel = 0;

	#endregion
	
	/// <summary>
	/// ゲームシーンに遷移する
	/// </summary>
	private void LoadGameScene(int level)
    {
		// イベントを登録
        SceneManager.sceneLoaded += LoadData;

		// 敵のレベルを設定
		_enemyLevel = level;

        // シーン遷移
        SceneManager.LoadScene(_sceneName);
    }

	/// <summary>
	/// 遷移時に送るデータ
	/// </summary>
	/// <param name="next">遷移シーン</param>
	/// <param name="mode"></param>
	private void LoadData(Scene next,LoadSceneMode mode)
    {
		// EnemyManagerScriptを取得
		EnemyManagerScript _enemyManagerScript
			= GameObject.FindGameObjectWithTag(ENEMY).GetComponent<EnemyManagerScript>();

		// 敵のレベルを設定
		_enemyManagerScript.EnemyLevel = _enemyLevel;

		// イベントを削除
		SceneManager.sceneLoaded -= LoadData;
	}

	/// <summary>
	/// イージーモード
	/// </summary>
	public void OnEasyMode()
    {
		LoadGameScene(LEVEL_ZERO);

	}

	/// <summary>
	/// ノーマルモード
	/// </summary>
	public void OnNormalMode()
    {
		LoadGameScene(LEVEL_OWE);

	}

	/// <summary>
	/// ハードモード
	/// </summary>
	public void OnHardMode()
    {
		LoadGameScene(LEVEL_TWO);
	}

	/// <summary>
	/// エクストラモード
	/// </summary>
	public void OnExtraMode()
    {
		LoadGameScene(LEVEL_THREE);
	}
}