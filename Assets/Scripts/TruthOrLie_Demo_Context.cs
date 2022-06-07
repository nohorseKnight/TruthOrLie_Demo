using QFramework;

namespace TruthOrLie_Demo
{
    public class TruthOrLie_Demo_Context : Architecture<TruthOrLie_Demo_Context>
    {
        protected override void Init()
        {
            RegisterSystem(new UISystem());
            RegisterModel(new GameRuntimeModel());
        }
    }
}