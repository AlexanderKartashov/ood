package ducks.strategies;

import ducks.base.IFlyBehavior;

public class FlyNoWay implements IFlyBehavior {
    public FlyNoWay(StringBuilder sb) {
        _sb = sb;
    }

    @Override
    public void Fly() {
        _sb.append("no way");
    }

    private final StringBuilder _sb;
}
