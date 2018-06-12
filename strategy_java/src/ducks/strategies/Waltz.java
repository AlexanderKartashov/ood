package ducks.strategies;

import ducks.base.IDanceBehaviour;

public class Waltz implements IDanceBehaviour {
    public Waltz(StringBuilder sb) {
        _sb = sb;
    }

    @Override
    public void Dance() {
        _sb.append("waltz");
    }

    private final StringBuilder _sb;
}
