package ducks;

import ducks.base.Duck;
import ducks.strategies.*;

import java.io.Console;
import java.util.ArrayList;

public class SimUDuck {
    public static void main(String args[])
    {
        Test t = new Test();
        t.Run();
        t.RunFunctional();
    }

    private static class Test
    {
        public void Run() {

            StringBuilder sb = new StringBuilder();

            final Duck mallard = new Duck(
                    new FlyWithWings(sb),
                    new Quack(sb),
                    new Waltz(sb),
                    "Mallard",
                    sb);
            final Duck decoy = new Duck(
                    new FlyNoWay(sb),
                    new Squeak(sb),
                    new DanceNoWay(sb),
                    "Decoy",
                    sb
            );
            final Duck redHead = new Duck(
                    new FlyWithCounter(new FlyWithWings(sb), sb),
                    new Quack(sb),
                    new Manuette(sb),
                    "Redhead",
                    sb
            );

            Duck ducks[] = { mallard, decoy, redHead };
            TestDucks(ducks, sb);

            System.out.println(sb.toString());
        }

        public void RunFunctional()
        {
            StringBuilder sb = new StringBuilder();

            final Duck mallard = new Duck(
                    () -> sb.append("with wings"),
                    () -> sb.append("quack"),
                    () -> sb.append("waltz"),
                    "Mallard",
                    sb);
            final Duck decoy = new Duck(
                    () -> sb.append("with wings"),
                    () -> sb.append("squeak"),
                    () -> sb.append("no dancing"),
                    "Decoy",
                    sb
            );
            final Duck redHead = new Duck(
                    () -> sb.append("with wings"),
                    () -> sb.append("quack"),
                    () -> sb.append("manuette"),
                    "Redhead",
                    sb
            );
            Duck ducks[] = { mallard, decoy, redHead };
            TestDucks(ducks, sb);

            System.out.println(sb.toString());
        }

        private void TestDucks(Duck ducks[], StringBuilder sb)
        {
            for(Duck d : ducks)
            {
                TestDuck(d, sb);
            }
        }

        private void TestDuck(Duck duck, StringBuilder sb)
        {
            sb.append("Duck ").append(duck.GetName()).append("\n");
            duck.Fly();
            duck.Dance();
            duck.Quack();
            sb.append("\n");
        }
    }
}
