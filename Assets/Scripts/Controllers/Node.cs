using System.Collections.Generic;
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
    public enum TipsInfo
    {
        NONE,
        FIRE, WATER, PLANT, ROCK,
        HP_INCREASE, TRAP, DESTINATION,
        enemy,
        ELEMENT_FIRE, ELEMENT_WATER, ELEMENT_PLANT, ELEMENT_ROCK,
        ZERO, ONE, TWO,
        UP, DOWN, LEFT, RIGHT,
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
        public List<string[]> TipsList;

        public string Info()
        {
            return $"Env:{Env}, EnemyCount:{EnemyCount}, EnemyElement{EnemyElement}, IsVisited{IsVisited}";
        }

    }
}