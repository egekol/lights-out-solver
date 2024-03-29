﻿// 16042023

using System;
using System.Linq;
using AssetKits.ParticleImage;
using Managers;
using UnityEngine;

namespace UI
{
    public class IntroUI : MonoBehaviour
    {
        [SerializeField] private ParticleImage particleImage;
        private GameManager _gameManager;

        private void OnEnable()
        {
            particleImage.Play();
            particleImage.onLastParticleFinish.AddListener(LoadMenu);
        }

        private void OnDisable()
        {
            // particleImage.onParticleFinish.RemoveListener(LoadMenu);
        }

        private void Start()
        {
            _gameManager = DependencyInjector.Instance.Resolve<GameManager>();

        }

        private void LoadMenu()
        {
            particleImage.Stop();
            particleImage.Clear();
            // _gameManager. LoadSceneReference(_gameManager.gameSceneList.First(i=>i.Name=="G_LevelScene03"));
            _gameManager.LoadMainMenu();
        }
    }
}