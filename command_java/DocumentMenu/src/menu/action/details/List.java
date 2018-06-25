package menu.action.details;

import document.IDocument;
import menu.IAction;
import menu.ILogger;
import sun.reflect.generics.reflectiveObjects.NotImplementedException;

public class List implements IAction {
    public List(IDocument document, ILogger logger) {
        _document = document;
        _logger = logger;
    }

    @Override
    public void Perform() throws Exception {
        new BuilderDirector().Build(_document, new ListBuilder(_logger));
    }

    private final IDocument _document;
    private final ILogger _logger;
}
