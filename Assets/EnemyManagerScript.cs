/*-------------------------------------------------
* EnemyManagerScript.cs
* 
* 作成日　2024/ 1/22
* 更新日　2024/ 1/22
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagerScript : MonoBehaviour 
{

	#region フィールド変数

	// 敵の状態
	private EnemyState _enemyState = EnemyState.Init;

	private enum EnemyState
    {
		Init,
		Execute,
		Exit,

    }

	private EnemyMoveScript _enemyMoveScript = default;

	private EasyMoveScript _easyMoveScript = default;

	private NormalMoveScript _normalMoveScript = default;

	private HardModeScript _hardModeScript = default;

	private ExtraMoveScript _extraMoveScript = default;


	#endregion

	/// <summary>
    /// 更新前処理
    /// </summary>
	private void Start () 
	{
		_easyMoveScript = GetComponent<EasyMoveScript>();

		_normalMoveScript = GetComponent<NormalMoveScript>();

		_hardModeScript = GetComponent<HardModeScript>();

		_extraMoveScript = GetComponent<ExtraMoveScript>();

		_enemyMoveScript = _easyMoveScript;
	}
	
	private void EnemyControll()
    {
        switch (_enemyState)
        {
            case EnemyState.Init:

				_enemyMoveScript.Init();

				_enemyState = EnemyState.Execute;

                break;
            case EnemyState.Execute:

				_enemyMoveScript.Execute();

                break;
            case EnemyState.Exit:

				_enemyMoveScript.Exit();

                break;
        }
    }
}