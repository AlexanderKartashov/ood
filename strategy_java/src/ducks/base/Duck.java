package ducks.base;

public class Duck {
    public Duck(
            IFlyBehavior flyBehaviour,
            IQuackBehaviour quackBehaviour,
            IDanceBehaviour danceBehaviour,
            String name,
            StringBuilder sb)
    {
        _flyBehaviour = flyBehaviour;
        _danceBehaviour = danceBehaviour;
        _quackBehaviour = quackBehaviour;
        _name = name;
        _sb = sb;
    }

    public String GetName()
    {
        return _name;
    }

    public void Swim()
    {
        _sb.append("Swimming").append("\n");
    }

    public void Dance()
    {
        _sb.append("Dancing: ");
        _danceBehaviour.Dance();
        _sb.append("\n");
    }

    public void Quack()
    {
        _sb.append("Quacking: ");
        _quackBehaviour.Quack();
        _sb.append("\n");
    }

    public void Fly()
    {
        _sb.append("Flying: ");
        _flyBehaviour.Fly();
        _sb.append("\n");
    }

    private final String _name;
    private final StringBuilder _sb;
    private final IQuackBehaviour _quackBehaviour;
    private final IFlyBehavior _flyBehaviour;
    private final IDanceBehaviour _danceBehaviour;
}
