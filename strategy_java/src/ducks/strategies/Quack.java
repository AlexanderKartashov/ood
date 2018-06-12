package ducks.strategies;

import ducks.base.IQuackBehaviour;

public class Quack implements IQuackBehaviour {
    public Quack(StringBuilder sb) {
        _sb = sb;
    }

    @Override
    public void Quack() {
        _sb.append("quack");
    }

    private final StringBuilder _sb;
}
