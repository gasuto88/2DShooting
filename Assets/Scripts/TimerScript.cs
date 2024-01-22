/*-------------------------------------------------
* TimerScript.cs
* 
* 作成日　2024/ 1/19
* 更新日　2024/ 1/19
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript
{
	public TimerScript(float time)
    {
		this._time = time;
		this._baseTime = time;
    }


	#region フィールド変数

	private float _time = 0f;

	private float _baseTime = 0f;

	private TimerState _timerState = TimerState.Execute;

	public enum TimerState
    {
		Execute,
		End
	}

    #endregion


    public TimerState Execute()
    {
        if (_time <= 0f)
        {
            _timerState = TimerState.End;
        }

        _time -= Time.deltaTime;

        return _timerState;
    }

    public void Reset()
    {
		_time = _baseTime;
        _timerState = TimerState.Execute;
    }
}