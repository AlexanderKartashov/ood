package document.commands;

import document.documentItem.ItemsCollection.IDocumentItem;
import document.documentItem.ItemsCollection.IMutableDocumentItemCollection;
import document.documentItem.image.IMutableImage;
import document.documentItem.image.Size;
import document.documentItem.paragraph.IMutableParagraph;
import document.documentItem.title.IMutableDocumentTitle;
import hsitory.ICommand;

import java.util.Optional;

public class CommandsFactory implements ICommandsFactory {
    public ICommand EditText(IMutableParagraph paragraph, String newText) {
        return new EditText(paragraph, newText);
    }

    public ICommand DeleteItem(IMutableDocumentItemCollection items, int pos) {
        return new DeleteItem(items, pos);
    }

    public ICommand ResizeImage(IMutableImage image, Size newSize) {
        return new ResizeImage(image, newSize);
    }

    public ICommand SetTitle(IMutableDocumentTitle title, String newTitle) {
        return new SetDocumentTitle(title, newTitle);
    }

    public ICommand InsertItem(IMutableDocumentItemCollection items, IDocumentItem newItem, Optional<Integer> position) {
        return new InsertItem(items, newItem, position);
    }
}
