﻿using UnityEngine;

    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper _bootstrapperPrefab;
        
        private void Awake()
        {
            var bootstrapper = FindObjectOfType<GameBootstrapper>();
            if (bootstrapper == null)
            {
                Instantiate(_bootstrapperPrefab);
            }
        }
    }