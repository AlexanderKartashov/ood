package app;

import menu.ILogger;

public class Logger implements ILogger {
    @Override
    public void Log(String str) {
        System.out.println(str);
    }
}
