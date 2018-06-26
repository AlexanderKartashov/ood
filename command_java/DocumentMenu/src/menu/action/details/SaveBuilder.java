package menu.action.details;

import document.documentItem.IDocumentItemVisitor;
import document.documentItem.ItemsCollection.IDocumentItem;
import document.documentItem.image.IImage;
import document.documentItem.paragraph.IParagraph;
import document.documentItem.title.IDocumentTitle;
import environment.ITextEncoder;
import environment.fileSystem.IFile;
import environment.fileSystem.IFileSystem;
import sun.security.pkcs11.wrapper.CK_DATE;

import java.io.IOException;

class SaveBuilder implements IDocumentStateBuilder {
    public SaveBuilder(IFileSystem fs, ITextEncoder encoder, String path) throws IOException {
        _fs = fs;
        _encoder = encoder;
        _path = path;

        if (!_fs.PathOperations().Checks().PathExists(_path)) {
            _fs.DirectoryOperations().Create(_path);
        }

        _file = _fs.FileOperations().CreateFile(_fs.PathOperations().Generators().RelativeToAbsolute(_path, "index.html"));
        _visitor = new SaveBuilderVisitor();
        _dir = _fs.PathOperations().Generators().RelativeToAbsolute(_path, "data");
    }

    @Override
    public void BuildTitle(IDocumentTitle title) throws IOException {
        title.Accept(_visitor);
    }

    @Override
    public void BuildDocumentItem(IDocumentItem item) throws IOException {
        item.Accept(_visitor);
    }

    @Override
    public void BeforeBuild() throws IOException {
        _file.addString("<!DOCTYPE html><html><head>");
    }

    @Override
    public void AfterBuild() throws Exception {
        _file.addString("</body></html>");
        _file.close();
    }

    private final String _dir;
    private final IDocumentItemVisitor _visitor;
    private final IFile _file;
    private final ITextEncoder _encoder;
    private final IFileSystem _fs;
    private final String _path;


    private class SaveBuilderVisitor implements IDocumentItemVisitor {

        @Override
        public void Visit(IDocumentTitle title) throws IOException {
            _file.addString(new StringBuilder("<title>").append(_encoder.Encode(title.GetTitle())).append("</title></head><body>").toString());
        }

        @Override
        public void Visit(IParagraph paragraph) throws IOException {
            _file.addString(new StringBuilder("<p>").append(_encoder.Encode(paragraph.GetText())).append("</p></head><body>").toString());
        }

        @Override
        public void Visit(IImage image) throws IOException {
            if (!_fs.PathOperations().Checks().PathExists(_dir)) {
                _fs.DirectoryOperations().Create(_dir);
            }

            _fs.FileOperations().Copy(
                    image.GetResource().Path(),
                    _fs.PathOperations().Generators().RelativeToAbsolute(_dir, image.GetResource().Name())
            );

            _file.addString(
                    new StringBuilder("<image src=\"")
                            .append(image.GetResource().Name())
                            .append("\" width=\"")
                            .append(image.GetSize().Width())
                            .append("\" height=\"")
                            .append(image.GetSize().Height())
                            .append("\"></image>").toString());
        }
    }
}
