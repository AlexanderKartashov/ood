package ducks.strategies;

import ducks.base.IFlyBehavior;

public class FlyWithCounter implements IFlyBehavior {
    public FlyWithCounter(IFlyBehavior decorated, StringBuilder sb) {
        _decorated = decorated;
        _sb = sb;
        _count = 0;
    }

    @Override
    public void Fly() {
        _decorated.Fly();
        _sb.append(", flies count = ").append(++_count);
    }

    private final IFlyBehavior _decorated;
    private final StringBuilder _sb;
    private int _count;
}
