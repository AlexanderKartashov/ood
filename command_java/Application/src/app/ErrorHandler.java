package app;

import menu.IErrorHandler;

public class ErrorHandler implements IErrorHandler {
    @Override
    public void OnError(Throwable err) {
        System.err.println(err.getMessage());
    }
}
