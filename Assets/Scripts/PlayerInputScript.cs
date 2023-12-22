/*-------------------------------------------------
* PlayerInputScript.cs
* 
* 作成日　2023/12/22
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;

public class PlayerInputScript : MonoBehaviour 
{

	#region フィールド変数

	//private float _horizontalInput = default;

	private float _verticalInput = default;

	public enum HorizontalState
    {
		NORMAL,
		LEFT,
		RIGHT
    }

	public enum VerticalState
    {
		NORMAL,
		UP,
		DOWN
    }

	private HorizontalState _horizontalState = HorizontalState.NORMAL;
	private VerticalState _verticalState = VerticalState.NORMAL;

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
		HorizontalInput();
	}

	public void HorizontalInput()
    {
		float _horizontalInput = Input.GetAxis("Horizontal");
		
		if (_horizontalInput < 0)
		{
			_horizontalState = HorizontalState.LEFT;
		}
		else if (0 < _horizontalInput)
		{
			_horizontalState = HorizontalState.RIGHT;
		}
		
	}

	public void VerticalInput()
    {
		float _vertical = Input.GetAxis("Vertical");

		if(_verticalInput < 0)
        {
			_verticalState = VerticalState.DOWN;
        }
		else if(0 < _verticalInput)
        {
			_verticalState = VerticalState.UP;
        }
    }
}