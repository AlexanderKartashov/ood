package document.commands;

import document.documentItem.image.IMutableImage;
import document.documentItem.image.Size;
import hsitory.AbstractCommandWithMemento;

class ResizeImage extends AbstractCommandWithMemento {

    public ResizeImage(IMutableImage image, Size newSize) {
        super(image);

        _image = image;
        _newSize = newSize;
    }

    @Override
    protected void DestroyImpl() {}

    @Override
    protected void ExecuteImpl() {
        _image.SetSize(_newSize);
    }

    private final IMutableImage _image;
    private final Size _newSize;
}
