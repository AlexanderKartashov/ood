package menu.action.details;

import document.documentItem.ItemsCollection.IDocumentItem;
import document.documentItem.title.IDocumentTitle;
import environment.ITextEncoder;
import environment.fileSystem.IFileSystem;

class SaveBuilder implements IDocumentStateBuilder {
    public SaveBuilder(IFileSystem fs, ITextEncoder encoder, String path) {
        _fs = fs;
        _encoder = encoder;
        _path = path;
    }

    @Override
    public void BuildTitle(IDocumentTitle title) {

    }

    @Override
    public void BuildDocumentItem(IDocumentItem item) {

    }

    @Override
    public void BeforeBuild() {

    }

    @Override
    public void AfterBuild() {

    }

    private final ITextEncoder _encoder;
    private final IFileSystem _fs;
    private final String _path;
}
