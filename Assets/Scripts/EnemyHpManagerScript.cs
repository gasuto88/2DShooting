/*-------------------------------------------------
* EnemyHpScript.cs
* 
* 作成日　2023/12/29
* 更新日　2024/ 1/25
* 
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 敵のHPを管理する
/// </summary>
public class EnemyHpManagerScript : MonoBehaviour 
{

	#region フィールド変数

	// 敵のHp
	private float _enemyHp = 0f;

	// 敵の最大Hp
	private float _enemyMaxHp = 0f;

	// ゲームを管理するScript
	private EnemyManagerScript _enemyManagerScript = default;

	// Hpバー
	private Slider _hpBar = default;

    #endregion

    #region プロパティ

	public float EnemyMaxHp { get => _enemyMaxHp; set => _enemyMaxHp = value; }

	public float EnemyHp { get => _enemyHp; set => _enemyHp = value; }

	public Slider HpBar { get => _hpBar; set => _hpBar = value; }

    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start () 
	{
		// EnemyManagerScriptを取得
		_enemyManagerScript
			= GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyManagerScript>();

		// Hpバーを取得
		_hpBar = GameObject.FindGameObjectWithTag("HpBar").GetComponent<Slider>();

		// Hpバーを不可視化
		_hpBar.gameObject.SetActive(false);
	}

    /// <summary>
    /// 敵のHPを減らす処理
    /// </summary>
    /// <param name="damage">くらったダメージ</param>
    public void DownEnemyHp(float damage)
    {
		// Hpを減算
		_enemyHp -= damage;

		DisplayEnemyHp();

		// 死んだら
		if(_enemyHp <= 0)
        {
			// 敵の状態を撃破に設定
			_enemyManagerScript.CrashEnemyState();
        }
    }

	/// <summary>
	/// 敵のHpを表示する処理
	/// </summary>
	private void DisplayEnemyHp()
    {
		_hpBar.value = _enemyHp / _enemyMaxHp;
    }
}