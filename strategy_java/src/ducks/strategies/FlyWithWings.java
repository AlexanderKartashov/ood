package ducks.strategies;

import ducks.base.IFlyBehavior;

public class FlyWithWings implements IFlyBehavior {
    public FlyWithWings(StringBuilder sb) {
        _sb = sb;
    }

    @Override
    public void Fly() {
        _sb.append("with wings");
    }

    private final StringBuilder _sb;
}
