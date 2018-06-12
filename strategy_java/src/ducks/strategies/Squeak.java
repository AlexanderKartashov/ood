package ducks.strategies;

import ducks.base.IQuackBehaviour;

public class Squeak implements IQuackBehaviour {
    public Squeak(StringBuilder sb) {
        _sb = sb;
    }

    @Override
    public void Quack() {
        _sb.append("squeak");
    }

    private final StringBuilder _sb;
}
