/*-------------------------------------------------
* TimerScript.cs
* 
* 作成日　2024/ 1/19
* 更新日　2024/ 1/24
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;

/// <summary>
/// タイマーを計る
/// </summary>
public class TimerScript
{
    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="time">設定時間</param>
    public TimerScript(float time)
    {
        // タイマーを設定
        this._time = time;
        this._baseTime = time;
    }


    #region フィールド変数

    // 経過時間
    private float _time = 0f;

    // 初期時間
	private float _baseTime = 0f;

    // タイマーの状態
	private TimerState _timerState = TimerState.Execute;
    
    // Execute 実行中
    // End     終了
	public enum TimerState
    {
		Execute,
		End
	}

    #endregion

    /// <summary>
    /// タイマー処理
    /// </summary>
    /// <returns>終了判定</returns>
    public TimerState Execute()
    {
        // タイマーが終了したら
        if (_time <= 0f)
        {
            _timerState = TimerState.End;
        }

        _time -= Time.deltaTime;

        // 終了判定
        return _timerState;
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Reset()
    {
        // 初期時間を設定
        _time = _baseTime;

        // 実行中に設定
        _timerState = TimerState.Execute;
    }
}