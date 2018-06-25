package modules;

import document.Document;
import document.IDocument;
import document.IMutableDocument;
import document.commands.CommandsFactory;
import document.commands.ICommandsFactory;
import document.documentItem.ItemsCollection.DocumentItemCollection;
import document.documentItem.ItemsCollection.DocumentItemFactory;
import document.documentItem.ItemsCollection.IDocumentItemFactory;
import document.documentItem.title.DocumentTitle;
import environment.fileSystem.IFileSystem;
import hsitory.History;
import hsitory.IHistory;
import storage.FileStorage;
import storage.IFileStorage;
import storage.IResourcesFactory;
import storage.ResourcesFactory;

import java.io.IOException;

public class DocumentModulesFactory implements IDocumentModulesFactory {
    @Override
    public IHistory CreateHistory() {
        return new History(10);
    }

    @Override
    public IMutableDocument CreateDocument() {
        return new Document(new DocumentItemCollection(), new DocumentTitle("Default title"));
    }

    @Override
    public IFileStorage CreateFileStorage(IFileSystem fs) throws IOException {
        return new FileStorage(fs, CreateResourcesFactory());
    }

    @Override
    public IDocumentItemFactory CreateDocumentItemFactory(IFileStorage storage) {
        return new DocumentItemFactory(storage);
    }

    @Override
    public ICommandsFactory CreateCommandsFactory() {
        return new CommandsFactory();
    }

    private IResourcesFactory CreateResourcesFactory() {
        return new ResourcesFactory();
    }
}
