using UnityEditor;
using UnityEngine;

namespace LumosLib
{
    public class SampleTestEditor : BaseTestEditorWindow
    {
        private int _intField;
        private float _floatField;
        private string _stringField;
        private Vector2 _vector2Field;
        private Vector3 _vector3Field;
        private bool _booleanField;
        
        private TestEditorGroup _testGroup;
        private bool _isToggledTestGroup;
        
        private TestEditorGroup _test2Group;
        private bool _isToggledTest2Group;


        private GameObject _testObject;
        
        
        
        [MenuItem("[ ✨Lumos Lib ]/Test Editor/SAMPLE")]
        public static void Open()
        {
            OnOpen<SampleTestEditor>("SAMPLE");
        }

        
        //MEMO : if you want ( create group or change properties )
        private void OnEnable()
        {
            TitleFontSize = 20;
            
            _testGroup = CreateGroup("Test");
            _test2Group = CreateGroup("Test2");
        }

        protected override void OnGUI()
        {
            base.OnGUI();
            
            DrawGroup(_testGroup, () =>
            {
                _testGroup.DrawField("Int" ,ref _intField);
                _testGroup.DrawSpaceLine();
                _testGroup.DrawField("Float",ref _floatField);
                _testGroup.DrawField("String", ref _stringField);
                _testGroup.DrawField("Vector2", ref _vector2Field);
                _testGroup.DrawField("Vector3", ref _vector3Field);
                _testGroup.DrawField("Vector3", ref _vector3Field);
                _testGroup.DrawField("Vector3", ref _vector3Field);
                // MEMO : onClick call back => checked toggle
                _testGroup.DrawField("Boolean", ref _booleanField, null);
                _testGroup.DrawButton("Test Click", () =>
                {
                    Debug.Log("Test click");
                });
            });
            
            DrawToggleGroup(_testGroup, ref _isToggledTestGroup, () =>
            {
                _testGroup.DrawField("Int" ,ref _intField);
                _testGroup.DrawSpaceLine();
                _testGroup.DrawField("Float",ref _floatField);
                _testGroup.DrawField("String", ref _stringField);
                _testGroup.DrawField("Vector2", ref _vector2Field);
                _testGroup.DrawField("Vector3", ref _vector3Field);
                _testGroup.DrawField("Boolean", ref _booleanField, null);
                _testGroup.DrawButton("Test Click", () =>
                {
                    Debug.Log("Test click");
                });
            });
            
            DrawToggleGroup(_test2Group, ref _isToggledTest2Group, () =>
            {
                _test2Group.DrawField("Int" ,ref _intField);
                _test2Group.DrawSpaceLine();
                _test2Group.DrawField("Float",ref _floatField);
                _test2Group.DrawField("String", ref _stringField);
                _test2Group.DrawField("Vector2", ref _vector2Field);
                _test2Group.DrawField("Vector3", ref _vector3Field);
                _test2Group.DrawField("Boolean", ref _booleanField, null);
                _test2Group.DrawButton("Test Click", () =>
                {
                    Debug.Log("Test click");
                });
            });

            FinishDraw();
        }
    }
}