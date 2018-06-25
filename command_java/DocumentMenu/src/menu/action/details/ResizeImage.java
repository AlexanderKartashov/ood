package menu.action.details;

import document.commands.ICommandsFactory;
import document.documentItem.ItemsCollection.IDocumentItemCollection;
import document.documentItem.image.IImage;
import document.documentItem.image.IMutableImage;
import document.documentItem.image.Size;
import hsitory.ICommand;
import hsitory.IHistory;

public class ResizeImage extends ActionWithHistory {
    public ResizeImage(IHistory history, ICommandsFactory commands, IDocumentItemCollection items, int position, Size newSize) {
        super(history);
        _commands = commands;
        _newSize = newSize;

        final IImage i = items.GetItem(position).GetImage();
        if (i == null)
        {
            throw new IllegalArgumentException(Integer.toString(position) + " item not an image");
        }
        if (!(i instanceof IMutableImage))
        {
            throw new IllegalArgumentException(Integer.toString(position) + " is readonly an image");
        }
        _image  = (IMutableImage)i;
    }

    @Override
    protected ICommand CreateCommand() {
        return _commands.ResizeImage(_image, _newSize);
    }

    private final ICommandsFactory _commands;
    private final IMutableImage _image;
    private final Size _newSize;
}
