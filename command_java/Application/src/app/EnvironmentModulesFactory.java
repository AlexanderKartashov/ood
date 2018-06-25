package app;

import environment.ITextEncoder;
import environment.fileSystem.IFileSystem;
import environment.fileSystem.IFileSystemCreator;
import menu.IErrorHandler;
import menu.ILogger;

public class EnvironmentModulesFactory {
    public EnvironmentModulesFactory(IFileSystemCreator fsCreator){
        _fsCreator = fsCreator;
    }

    public ILogger CreateLogger(){
        return new Logger();
    }

    public IErrorHandler CreateErrorHandler() {
        return new ErrorHandler();
    }

    public ITextEncoder CreateEncoder() { return new HtmlEncoder(); }

    public IFileSystem CreateFileSystem() {
        return _fsCreator.CreateFileSystem();
    }

    private final IFileSystemCreator _fsCreator;
}
