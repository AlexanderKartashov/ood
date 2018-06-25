package menu.action;

import document.documentItem.image.Size;
import menu.IAction;

import java.util.Optional;

public interface IMenuCommandsFactory {

    IAction Redo();

    IAction Undo();

    IAction List();

    IAction ChangeText(int pos, String text);

    IAction Delete(int pos);

    IAction InsertImage(Optional<Integer> pos, Size size, String path);

    IAction InsertParagraph(Optional<Integer> pos, String text);

    IAction ResizeImage(int pos, Size size);

    IAction Save(String path);

    IAction SetTitle(String title);

    IAction Help();
}
