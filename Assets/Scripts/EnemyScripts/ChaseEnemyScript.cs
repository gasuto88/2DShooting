/*-------------------------------------------------
* ChaseEnemyScript.cs
* 
* 作成日　2024/ 1/24
* 更新日　2024/ 1/24
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;

/// <summary>
/// 敵に追従するクラス
/// </summary>
public class ChaseEnemyScript : MonoBehaviour 
{

	#region フィールド変数

	// 自分のTransform
	private Transform _myTransform = default;

	// 敵の座標
	private Transform _enemy = default;

	#endregion

	/// <summary>
    /// 更新前処理
    /// </summary>
	private void Start () 
	{
		// 自分のTransformを設定
		_myTransform = transform;

		// 敵の座標取得
		_enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
	}
	
	/// <summary>
    /// 更新処理
    /// </summary>
	private void Update () 
	{
		// 敵の座標設定
		_myTransform.position = _enemy.position;
	}
}