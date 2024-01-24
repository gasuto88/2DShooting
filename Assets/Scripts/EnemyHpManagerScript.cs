/*-------------------------------------------------
* EnemyHpScript.cs
* 
* 作成日　2023/12/29
* 更新日　2023/12/29
* 
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;

/// <summary>
/// 敵のHPを管理する
/// </summary>
public class EnemyHpManagerScript : MonoBehaviour 
{

	#region フィールド変数

	// 敵のHp
	private float _enemyHp = 0f;

	// ゲームを管理するScript
	private EnemyManagerScript _enemyManagerScript = default;

    #endregion

    #region プロパティ

	public float EnemyHp { get => _enemyHp; set => _enemyHp = value; }

    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start () 
	{
		// EnemyManagerScriptを取得
		_enemyManagerScript
			= GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyManagerScript>();
	}

	/// <summary>
	/// 敵のHPを減らす処理
	/// </summary>
	/// <param name="damage">くらったダメージ</param>
	public void DownEnemyHp(float damage)
    {
		// Hpを減算
		_enemyHp -= damage;

		// 死んだら
		if(_enemyHp <= 0)
        {
			// 敵の状態を撃破に設定
			_enemyManagerScript.CrashEnemyState();
        }
    }
}