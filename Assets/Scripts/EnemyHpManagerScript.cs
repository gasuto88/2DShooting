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
/// 敵のHPを管理するクラス
/// </summary>
public class EnemyHpManagerScript : MonoBehaviour 
{

	#region フィールド変数

	[SerializeField,Header("敵のHP"),Range(0,1000)]
	private float _enemyHp = 0f;

    #endregion

    #region プロパティ

	public float EnemyHp { get => _enemyHp; set => _enemyHp = value; }

    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start () 
	{
	
	}
	
	/// <summary>
    /// 更新処理
    /// </summary>
	private void Update () 
	{
	
	}

	/// <summary>
	/// 敵のHPを減らす処理
	/// </summary>
	/// <param name="damage"></param>
	public void DownEnemyHp(float damage)
    {
		_enemyHp -= damage;

		if(_enemyHp <= 0)
        {
			Debug.Log("死んだ");
        }
    }
}