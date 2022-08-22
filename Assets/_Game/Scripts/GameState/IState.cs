namespace AdrianKovatana.Essentials.FiniteStateMachine
{
    public interface IState
    {
        void StateEnter();
        void StateUpdate();
        void StateFixedUpdate();
        void StateExit();
    }
}
