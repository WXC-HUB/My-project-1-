﻿using Assets.Scripts.BaseUtils;
using Assets.Scripts.BuildingSystem;
using Assets.Scripts.Inputs;
using UnityEngine;

namespace Assets.Scripts.Core
{
    //和整个游戏相关的东西从这里走，只允许GameManager处理Level逻辑
    public class GameManager : MonoSingleton<GameManager>
    {
        [field: SerializeField]
        public BuildItem Item { get; private set; }

        [field: SerializeField]
        public BuildingLayer BuildingLayer { get; private set; }

        [SerializeField]
        private InputActionsHandler _inputActionsHandler;



        private void Awake()
        {
            base.Awake();
            UI.UIManager.Instance.InitUIManager();

            //调用一个空函数，确保表格管理器加载出来
            GameTableConfig.Instance.CallBlank();
        }

        // Use this for initialization
        void Start()
        {
            LevelManager.Instance.LoadLevelConfigFromTable(1, 1);
        }

        // Update is called once per frame
        void Update()
        {
            BuildingLayer.ShowPreview(_inputActionsHandler.MouseWorldPosition, Item);
            if (_inputActionsHandler.IsMouseLeftButtonPressed
                && BuildingLayer != null 
                && Item != null
                && BuildingLayer.IsEmpty(_inputActionsHandler.MouseWorldPosition))
            {
                BuildingLayer.Build(_inputActionsHandler.MouseWorldPosition, Item);
            }
        }
    }
}