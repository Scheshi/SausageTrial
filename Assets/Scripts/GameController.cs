using System.Collections.Generic;
using Assets.Scripts.Controllers;
using Assets.Scripts.Fabrics;
using Assets.Scripts.Intefaces;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        #region  Config
        
        [Header("Player settings")]
        [SerializeField] private GameObject _slickPrefab;
        [SerializeField] private Vector3 _startPosition;

        [Space(10.0f)]
        [Header("Target settings")] 
        [SerializeField] private Rigidbody _targetPrefab;
        [SerializeField] private float _coolDown;

        [Space(10.0f)] [Header("Level settings")] 
        [SerializeField] private GameObject _levelObjectPrefab;
        [SerializeField] private float _piecesMinHeight;
        [SerializeField] private float _piecesMaxHeight;
        [SerializeField] private int _piecesCount;
        [SerializeField] private int _voidChanceInPersent;

        [Space(10.0f)] [Header("Menu settings")]
        [SerializeField] private Text _menuTextPrefab;
        [SerializeField] private Button _menuButtonPrefab;
        
        
        #endregion
        
        
        #region Fields
        private CameraController _cameraController;
        private MenuController _menu;
        private InputController _inputController = new InputController();
        private readonly SlickFabric _slickFabric = new SlickFabric();
        private LevelFabric _levelFabric = new LevelFabric();
        private IController _slickController;
        private readonly List<IExecutable> _executables = new List<IExecutable>();

        #endregion


        #region  UnityMethods
        
        private void Start()
        {
            _menu = new MenuController(_menuTextPrefab, _menuButtonPrefab);
            _menu.Button.onClick.AddListener(StartGame);
            StartGame();
        }

        private void Update()
        {
            for (int i = 0; i < _executables.Count; i++)
            {
                _executables[i].Execute();
            }
        }
        
        #endregion


        #region  Methods
        
        private void StartGame()
        {
            _menu.Close();
            _levelFabric.Contruct
            (_levelObjectPrefab, _piecesCount, new Vector3(0.0f, -0.5f, 0.0f),
                _piecesMinHeight, _piecesMaxHeight, _voidChanceInPersent);
            var contructPlayer = _slickFabric.Contruct(_startPosition, _slickPrefab, Camera.main);
            _cameraController = new CameraController(contructPlayer.Item1.Transform, Camera.main, new Vector3(5.0f, 5.0f, 30.0f));
            _inputController.AddSlickController(contructPlayer.Item2);
            _inputController.AddTargetController(
                new TargetController(_targetPrefab, contructPlayer.Item1.Transform, _coolDown));
            _slickController = contructPlayer.Item2;
            _executables.Add(_slickController);
            _executables.Add(_inputController);
            _executables.Add(_cameraController);
            _slickController.onDeath += GameOver;
        }
        
        private void GameOver()
        {
            _executables.Clear();
            _levelFabric.Dispose();
            _cameraController.Dispose();
            _cameraController = null;
            _inputController.Dispose();
            _slickController.Dispose();
            _slickController = null;
            _menu.View("Вы проиграли!");
        }
        
        #endregion
    }
}