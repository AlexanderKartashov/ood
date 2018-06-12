package ducks.strategies;

import ducks.base.IDanceBehaviour;

public class DanceNoWay implements IDanceBehaviour {

    public DanceNoWay(StringBuilder sb) {
        _sb = sb;
    }

    @Override
    public void Dance() {
        _sb.append("no dance");
    }

    private final StringBuilder _sb;
}
