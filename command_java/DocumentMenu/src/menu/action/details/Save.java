package menu.action.details;

import document.IDocument;
import environment.ITextEncoder;
import environment.fileSystem.IFileSystem;
import menu.IAction;

public class Save implements IAction {
    public Save(String path, IDocument doc, IFileSystem fs, ITextEncoder encoder) {
        _path = path;
        _document = doc;
        _fs = fs;
        _encoder = encoder;
    }

    @Override
    public void Perform() throws Exception {
        new BuilderDirector().Build(_document, new SaveBuilder(_fs, _encoder, _path));
    }

    private final IFileSystem _fs;
    private final String _path;
    private final IDocument _document;
    private final ITextEncoder _encoder;
}
