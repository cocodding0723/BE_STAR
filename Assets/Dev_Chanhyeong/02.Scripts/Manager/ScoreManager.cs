using System;
using System.Collections.Generic;
using Chanhyeong.Decorate;
using UnityEngine;
using UnityToolBox.Pattern;

namespace Chanhyeong
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        public Action<int> onGetScore;
        public int TotalScore { get; private set; }

        private void Awake()
        {
            
        }

        public static void StaticSetScore(int score) => Instance.SetScore(score);

        public void SetScore(int score)
        {
            TotalScore += score;
            onGetScore?.Invoke(TotalScore);
        }
    }
}