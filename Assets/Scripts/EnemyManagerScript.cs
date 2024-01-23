/*-------------------------------------------------
* EnemyManagerScript.cs
* 
* 作成日　2024/ 1/22
* 更新日　2024/ 1/22
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;

public class EnemyManagerScript : MonoBehaviour
{

    #region フィールド変数

    // 敵の状態
    private EnemyState _enemyState = EnemyState.INIT;

    private enum EnemyState
    {
        INIT,
        EXECUTE,
        CRASH
    }

    private int _index = 0;

    private EnemyMoveScript _enemyMoveScript = default;

    private EnemyMoveScript[] _enemyMoveScripts = default;

    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start()
    {
        EasyMoveScript _easyMoveScript = GetComponent<EasyMoveScript>();
        NormalMoveScript _normalMoveScript = GetComponent<NormalMoveScript>();
        HardMoveScript _hardMoveScript = GetComponent<HardMoveScript>();
        ExtraMoveScript _extraMoveScript = GetComponent<ExtraMoveScript>();

        _enemyMoveScripts
            = new EnemyMoveScript[]
            { _easyMoveScript,_normalMoveScript,_hardMoveScript,_extraMoveScript};

        _enemyMoveScript = _easyMoveScript;
    }

    public void EnemyControll()
    {
        switch (_enemyState)
        {
            case EnemyState.INIT:

                _enemyMoveScript.Init();

                _enemyState = EnemyState.EXECUTE;

                break;
            case EnemyState.EXECUTE:

                _enemyMoveScript.Execute();

                break;
            case EnemyState.CRASH:

                Debug.LogError("ゲームクリア");

                break;
        }
    }

    /// <summary>
    /// 敵の状態を切り替える
    /// </summary>
    public void ChengeEnemyState()
    {
        Debug.Log("うんち");
        _index++;

        if (4 <= _index)
        {
            _enemyState = EnemyState.CRASH;
            return;
        }

        _enemyMoveScript = _enemyMoveScripts[_index];

        _enemyState = EnemyState.INIT;
    }
}