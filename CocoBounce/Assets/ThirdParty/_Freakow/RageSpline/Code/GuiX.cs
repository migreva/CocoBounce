using UnityEngine;
using System;

public static class GuiX {

	public static Action<Action> Horizontal = horizontalBlockActions => {
		GUILayout.BeginHorizontal();
		horizontalBlockActions();
		GUILayout.EndHorizontal();
	};

  	public static Action<Action> Vertical = verticalBlockActions => {
  		GUILayout.BeginVertical();
  		verticalBlockActions();
  		GUILayout.EndVertical();
  	};

	public static Action<GUIStyle, Action> VerticalStyled = (blockStyle, verticalBlockActions) => {
		if (blockStyle==null) GUILayout.BeginVertical();
		else 
			GUILayout.BeginVertical(blockStyle);
		verticalBlockActions();
		GUILayout.EndVertical();
	};

}
