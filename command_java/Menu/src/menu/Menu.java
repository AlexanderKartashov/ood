package menu;

import java.io.IOException;

public class Menu {

    public Menu(IErrorHandler errorHandler) {
        _errorHandler = errorHandler;
    }

    public void Run(IActionSource actions) throws IOException {
        while(actions.HasMoreActions()) {
            try
            {
                final IAction action = actions.GetNextAction();
                action.Perform();
            }
            catch(Throwable ex)
            {
                _errorHandler.OnError(ex);
            }
        }
    }

    private final IErrorHandler _errorHandler;
}
