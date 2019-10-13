using UnityEditor;
using UnityEngine;
//расширения для Player
namespace Geekbrains.Editor.Test
{
    //класс у которого должны расширятся возможности редактора должен наследоваться от Editor
    [CustomEditor(typeof(Player))]//указываем, какой компонент хотим расширить typeof(Player)
    public class PlayerEditor : UnityEditor.Editor
    {
        //private bool _isPressButtonOk;
        //переопределяем метод который будет расширять инспектор
        public override void OnInspectorGUI()
        {
            //оьъект на который мы добавили этот скрипт это target,приводим к типу, который хотим расширить (Player), и через myTarget обращаемся к нашему объекту
            Player myTarget = (Player)target;
            //создаю объект - тип Player, для этого преобразую переменную target в тип Player
            //Player player = this.target as Player;
            //название окна редактора (можно без , EditorStyles.boldLabel) - это выделение жирным)
            GUILayout.Label("Настройки игрока", EditorStyles.boldLabel);
            //текстовое окно с описанием
            //player.description = EditorGUILayout.TextArea(player.description, GUILayout.Height(50));
            myTarget.description = EditorGUILayout.TextArea(myTarget.description, GUILayout.Height(50));
        }
    }
}