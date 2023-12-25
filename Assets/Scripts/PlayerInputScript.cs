/*-------------------------------------------------
* PlayerInputScript.cs
* 
* 作成日　2023/12/22
* 更新日　2023/12/25
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;

/// <summary>
/// プレイヤーの入力を管理するクラス
/// </summary>
public class PlayerInputScript : MonoBehaviour 
{

	#region フィールド変数

	#endregion

	/// <summary>
	/// 上入力の判定処理
	/// </summary>
	/// <returns>上入力判定</returns>
	public bool UpInput()
    {		
		if (Input.GetKey(KeyCode.W))
		{
			return true;
		}

		return false;
	}

	/// <summary>
	/// 下入力の判定処理
	/// </summary>
	/// <returns>下入力判定</returns>
	public bool DownInput()
    {
		if (Input.GetKey(KeyCode.S))
		{
			return true;
		}

		return false;
	}

	/// <summary>
	/// 左入力の判定処理
	/// </summary>
	/// <returns>左入力判定</returns>
	public bool LeftInput()
    {
		if (Input.GetKey(KeyCode.A))
		{
			return true;
		}

		return false;
	}

	/// <summary>
	/// 右入力の判定処理
	/// </summary>
	/// <returns>右入力判定</returns>
	public bool RightInput()
    {
		if (Input.GetKey(KeyCode.D))
		{
			return true;
		}

		return false;
	}

	/// <summary>
	/// 射撃入力の判定処理
	/// </summary>
	/// <returns>射撃入力判定</returns>
	public bool ShotInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
			return true;
        }
		return false;
    }

	public void ShotCoolTime()
    {
		
    } 
}