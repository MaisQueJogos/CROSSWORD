using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(BoardData), false)]
[CanEditMultipleObjects]
[System.Serializable]
public class BoardDataDrawer : Editor
{
   private BoardData GameDataInstance => target as BoardData; 
   private ReorderableList _dataList;

   private void OnEnable()
   {
      throw new NotImplementedException();
   }
   public override void OnInspectorGUI()
   {
      serializedObject.Update();
      DrawColumnsRowsInputFields();
      EditorGUILayout.Space();

      if(GameDataInstance.Board != null && GameDataInstance.Columns >0 && GameDataInstance.Rows >0)
          DrawBoardTable(); 

      serializedObject.ApplyModifieldProperties();
      if (GUI.changed)
      {
         EditorUtility.StetDirty(GameDataInstance);
      }
   }
   private void DrawColumnsRowsInputFields()
   {
      var columnsTemp = GameDataInstance.Columns;
      var rowsTemp = GameDataInstance.Rows;

      GameDataInstance.Columns = EditorGUILayout.Infield("Columns", GameDataInstance.Columns);
      GameDataInstance.Rows = EditorGUILayout.Infield("Rows",GameDataInstance.Rows);

      if((GameDataInstance.Colums != columnsTemp || GameDataInstance.Rows != rowsTemp) 
         && GameDataInstance.columns > 0 && GameDataInstance.Rows > 0)
         {
            GameDataInstance.CreateNewBoard();
         }
   }
   private void DrawBoardTable()
   {
      var tableStyle = new GUIStyle("box");
      tableStyle.padding = new RectOffset(10, 10, 10, 10);
      tableStyle.margin.left = 32;

      var headerColumnStyle = new GUIStyle();
      headerColumnStyle.fixedWidth = 35;

      var columnStyle = new GUIStyle();
      columnStyle.fixedWidth = 50;

      var rowStyle = new GUIStyle();
      rowStyle.fixedHeight = 25;
      rowStyle.fixedWidth = 40;
      rowStyle.aligment = TextAnchor.MiddleCenter;

      var textFieldStyle = new GUIStyle();

      textFieldStyle.normal.background = Texture2D.grayTexture;
      textFieldStyle.normal.textColor = Color.white;
      textFieldStyle.fontStyle = fontStyle.Bold;
      textFieldStyle.aligment = TextAnchor.MiddleCenter;
      EditorGUILayout.BeginHorizontal(tableStyle);
      for(var x = 0; x < GameDataInstance.Columns; x++)
      {
         EditorGUILayout.BeginVertical(x == -1 ? headerColumnStyle : columnStyle);
         for(var y - 0; y < GameDataInstance.Rows; y++)
         {
            if(x >= 0 && y >= 0)
            {
               EditorGUILayout.BeginHorizontal(rowStyle);
               var character = (string) EditorGUILayout.TextArea(GameDataInstance.Board[x].Row[y], textFieldStyle);
               if(GameDataInstance.Board[x].Row[y].length > 1)
               {
                  character = GameDataInstance.Board[x].Row[y].Substring(0, 1);
               }

               GameDataInstance.Board[x].Row[y] = character;
               EditorGUILayout.EndHorizontal();
            }
         }
         EditorGUILayout.EndVertical();
      }
      EditorGUILayout.EndHorizontal();
   }
}