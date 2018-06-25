package document.documentItem.paragraph;

import document.documentItem.IDocumentItemVisitor;
import document.documentItem.image.IImage;
import hsitory.IMemento;
import hsitory.IMementoOriginator;

public class Paragraph implements IMutableParagraph {

    public Paragraph(String text) {
        _text = text;
    }

    @Override
    public void close() throws Exception {}

    @Override
    public void SetText(String text) {
        _text = text;
    }

    @Override
    public String GetText() {
        return _text;
    }

    @Override
    public void Accept(IDocumentItemVisitor visitor) {
        visitor.Visit(this);
    }

    @Override
    public IParagraph GetParagraph() {
        return this;
    }

    @Override
    public IMemento GetState() {
        return new ParagraphMemento(_text, this);
    }

    private String _text;
}
