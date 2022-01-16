using System;

namespace Chanhyeong.Decorate
{
    public interface IScore
    {
        event Action<int> OnScorePoint;
        
        int Score { get; set; }
        void OnScore();
    }
}