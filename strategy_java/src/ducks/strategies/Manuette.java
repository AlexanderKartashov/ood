package ducks.strategies;

import ducks.base.IDanceBehaviour;

public class Manuette implements IDanceBehaviour {
    public Manuette(StringBuilder sb) {
        _sb = sb;
    }

    @Override
    public void Dance() {
        _sb.append("manuette");
    }

    private final StringBuilder _sb;
}
