package app;

import document.documentItem.image.Size;
import menu.IAction;
import menu.action.IMenuCommandsFactory;

import java.util.Optional;

class MenuCommandsFactoryProxy implements IMenuCommandsFactory {

    public void SetFactory(IMenuCommandsFactory factory) {
        _factory = factory;
    }

    @Override
    public IAction Redo() {
        ValidateFactory();
        return _factory.Redo();
    }

    @Override
    public IAction Undo() {
        ValidateFactory();
        return _factory.Undo();
    }

    @Override
    public IAction List() {
        ValidateFactory();
        return _factory.List();
    }

    @Override
    public IAction ChangeText(int pos, String text) {
        ValidateFactory();
        return _factory.ChangeText(pos, text);
    }

    @Override
    public IAction Delete(int pos) {
        ValidateFactory();
        return _factory.Delete(pos);
    }

    @Override
    public IAction InsertImage(Optional<Integer> pos, Size size, String path) {
        ValidateFactory();
        return _factory.InsertImage(pos, size, path);
    }

    @Override
    public IAction InsertParagraph(Optional<Integer> pos, String text) {
        ValidateFactory();
        return _factory.InsertParagraph(pos, text);
    }

    @Override
    public IAction ResizeImage(int pos, Size size) {
        ValidateFactory();
        return _factory.ResizeImage(pos, size);
    }

    @Override
    public IAction Save(String path) {
        ValidateFactory();
        return _factory.Save(path);
    }

    @Override
    public IAction SetTitle(String title) {
        ValidateFactory();
        return _factory.SetTitle(title);
    }

    @Override
    public IAction Help() {
        ValidateFactory();
        return _factory.Help();
    }

    private void ValidateFactory() {
        if (_factory == null)
        {
            throw new NullPointerException("factory not initialized");
        }
    }

    private IMenuCommandsFactory _factory;
}
