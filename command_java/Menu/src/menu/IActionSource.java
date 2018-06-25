package menu;

import java.io.IOException;

public interface IActionSource {

    IAction GetNextAction() throws IOException;

    boolean HasMoreActions() throws IOException;
}
