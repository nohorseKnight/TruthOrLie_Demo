using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace TruthOrLie_Demo
{
    public enum Environment
    {
        NONE,
        FIRE,
        WATER,
        PLANT,
        ROCK,
        HP_INCREASE,
        TRAP,
        DESTINATION,
    }
    public class Node : BaseController
    {
        [SerializeField] Environment _env;
        public Environment Env
        {
            get { return _env; }
            set { _env = value; }
        }
        [SerializeField] int _enemyCount;
        public int EnemyCount
        {
            get { return _enemyCount; }
            set { _enemyCount = value; }
        }
        [SerializeField] Element _enemyElement;
        public Element EnemyElement
        {
            get { return _enemyElement; }
            set { _enemyElement = value; }
        }
        [SerializeField] bool _isVisited;
        public bool IsVisited
        {
            get { return _isVisited; }
            set { _isVisited = value; }
        }

        public string Info()
        {
            return $"Env:{Env}, EnemyCount:{EnemyCount}, EnemyElement{EnemyElement}, IsVisited{IsVisited}";
        }

    }
}