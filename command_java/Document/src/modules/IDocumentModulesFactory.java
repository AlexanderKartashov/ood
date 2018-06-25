package modules;

import document.IMutableDocument;
import document.commands.CommandsFactory;
import document.commands.ICommandsFactory;
import document.documentItem.ItemsCollection.DocumentItemFactory;
import document.documentItem.ItemsCollection.IDocumentItemFactory;
import environment.fileSystem.IFileSystem;
import hsitory.IHistory;
import storage.FileStorage;
import storage.IFileStorage;
import storage.IResourcesFactory;
import storage.ResourcesFactory;

import java.io.IOException;

public interface IDocumentModulesFactory {
    IHistory CreateHistory();

    IMutableDocument CreateDocument();

    IFileStorage CreateFileStorage(IFileSystem fs) throws IOException;

    IDocumentItemFactory CreateDocumentItemFactory(IFileStorage storage);

    ICommandsFactory CreateCommandsFactory();
}
