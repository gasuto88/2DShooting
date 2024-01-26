/*-------------------------------------------------
* EnemyManagerScript.cs
* 
* 作成日　2024/ 1/22
* 更新日　2024/ 1/24
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;

/// <summary>
/// 敵を管理する
/// </summary>
public class EnemyManagerScript : MonoBehaviour
{

    #region フィールド変数

    // 敵のレベル
    private int _enemyLevel = 0;

    // 自分のTransform
    private Transform _myTransform = default;

    // 敵の状態
    private EnemyState _enemyState = EnemyState.START;

    private enum EnemyState
    {
        START,
        INIT,
        EXECUTE,
        CRASH
    }

    // 敵を動かすScript
    private EnemyMoveScript _enemyMoveScript = default;

    // 敵を動かすScripts
    private EnemyMoveScript[] _enemyMoveScripts = default;

    // ゲームの状態を管理するScript
    private GameManagerScript _gameManagerScript = default;

    // 敵のHpを管理するScript
    private EnemyHpManagerScript _enemyHpManagerScript = default;

    #endregion

    #region プロパティ

    public int EnemyLevel { get => _enemyLevel; set => _enemyLevel = value; }

    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start()
    {
        // 自分のTransformを設定
        _myTransform = transform;

        // 敵の挙動を取得
        EasyMoveScript _easyMoveScript = GetComponent<EasyMoveScript>();
        NormalMoveScript _normalMoveScript = GetComponent<NormalMoveScript>();
        HardMoveScript _hardMoveScript = GetComponent<HardMoveScript>();
        ExtraMoveScript _extraMoveScript = GetComponent<ExtraMoveScript>();

        // GameManagerScriptを取得
        _gameManagerScript
            = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();

        _enemyHpManagerScript = GetComponent<EnemyHpManagerScript>();

        // 敵の挙動を設定
        _enemyMoveScripts
            = new EnemyMoveScript[]
            { _easyMoveScript,_normalMoveScript,_hardMoveScript,_extraMoveScript};

        SetEnemyMode();
    }

    /// <summary>
    /// 敵を制御する処理
    /// </summary>
    public void EnemyControll()
    {
        switch (_enemyState)
        {
            // 開始
            case EnemyState.START:

                // 目的地に移動
                _enemyMoveScript.GoToTargetPosition();
                
                // 目的地に着いたら
                if (_enemyMoveScript.CheckArriveTargetPosition(
                    _enemyMoveScript.Destinations[0], _myTransform.position))
                {
                    // ゲーム開始判定
                    _gameManagerScript.IsGameStart = true;

                    // 敵のHpバーを可視化
                    _enemyHpManagerScript.HpBar.gameObject.SetActive(true);

                    _enemyState = EnemyState.INIT;
                }

                break;

            // 初期化
            case EnemyState.INIT:

                _enemyMoveScript.Init();

                _enemyState = EnemyState.EXECUTE;

                break;

            // 実行中
            case EnemyState.EXECUTE:

                _enemyMoveScript.Execute();

                break;

            // 撃破
            case EnemyState.CRASH:

                // ゲームクリア判定
                _gameManagerScript.IsGameClear = true;

                break;
        }
    }

    /// <summary>
    /// 敵のレベルを設定する処理
    /// </summary>
    /// <param name="level">敵のレベル</param>
    public void SetEnemyMode()
    {
        _enemyMoveScript = _enemyMoveScripts[_enemyLevel];
    }

    /// <summary>
    /// 敵の状態を撃破にする処理
    /// </summary>
    public void CrashEnemyState()
    {
        _enemyState = EnemyState.CRASH;
    }
}