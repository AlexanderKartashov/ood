package app;

import document.IMutableDocument;
import environment.ITextEncoder;
import environment.fileSystem.IFileSystem;
import fileSystem.FileSystemCreator;
import hsitory.IHistory;
import menu.*;
import menu.action.MenuCommandsFactory;
import menu.parser.ActionParserCreator;
import modules.DocumentModulesFactory;
import modules.IDocumentModulesFactory;
import storage.IFileStorage;

import java.io.BufferedReader;
import java.io.InputStreamReader;

public class App {
    public void Run(){

        final EnvironmentModulesFactory environmentModulesFactory = new EnvironmentModulesFactory(new FileSystemCreator());
        final IErrorHandler errorHandler = environmentModulesFactory.CreateErrorHandler();
        final IFileSystem fs = environmentModulesFactory.CreateFileSystem();

        final IDocumentModulesFactory documentModules = new DocumentModulesFactory();

        try (final IFileStorage storage = documentModules.CreateFileStorage(fs))
        {
            final IMutableDocument document = documentModules.CreateDocument();
            final IHistory history = documentModules.CreateHistory();

            final MenuCommandsFactoryProxy proxy = new MenuCommandsFactoryProxy();
            final IActionsParserFactory parserCreator = new ActionParserCreator(proxy);
            final IActionsParser parser = parserCreator.CreateActionsParser();
            proxy.SetFactory(new MenuCommandsFactory(
                    environmentModulesFactory.CreateEncoder(),
                    fs,
                    documentModules.CreateDocumentItemFactory(storage),
                    environmentModulesFactory.CreateLogger(),
                    documentModules.CreateCommandsFactory(),
                    document,
                    history,
                    parser)
            );

            final IActionSource actionSource = new DocumentMenuActionSource(parser, new BufferedReader(new InputStreamReader(System.in)));
            final Menu menu = new Menu(errorHandler);
            menu.Run(actionSource);
        }
        catch(Throwable ex)
        {
            errorHandler.OnError(ex);
        }
    }
}
