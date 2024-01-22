/*-------------------------------------------------
* EasyMoveScript.cs
* 
* 作成日　2024/ 1/
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyMoveScript : EnemyMoveScript
{

    #region フィールド変数

    [Space(10)]
    [Header("【Easy】")]
    [Space(10)]

    [SerializeField, Header("敵のHP"), Range(0, 1000)]
    private float _easyEnemyHp = 0f;

    [SerializeField, Header("敵の移動速度"), Range(0, 500)]
    private float _easyMoveSpeed = 0f;

    [SerializeField, Header("敵の回転速度"), Range(-500, 500)]
    private float _easyRotationSpeed = 0f;

    [SerializeField, Header("敵の弾の速度"), Range(0, 100)]
    private float _easyBallSpeed = default;

    [SerializeField, Header("敵の弾の回転速度"), Range(-22, 22)]
    private float _easyBallRotationSpeed = default;

    [SerializeField, Header("敵の弾の大きさ")]
    private Vector3 _easyBallScale = default;

    [SerializeField, Header("射撃のクールタイム"), Range(0, 2)]
    private float _easyShotCoolTime = default;

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
}