using QFramework;
using UnityEngine;

namespace TruthOrLie_Demo
{
    public class BaseController : MonoBehaviour, IController
    {
        public IArchitecture GetArchitecture()
        {
            return TruthOrLie_Demo_Context.Interface;
        }
    }
}