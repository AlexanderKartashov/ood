package document.documentItem.title;

import document.documentItem.IDocumentItemVisitor;
import hsitory.IMemento;
import hsitory.IMementoOriginator;

import java.io.IOException;

public class DocumentTitle  implements IMutableDocumentTitle {

    public DocumentTitle(String title){
        _title = title;
    }

    @Override
    public void SetTitle(String title) {
        _title = title;
    }

    @Override
    public String GetTitle() {
        return _title;
    }

    @Override
    public void Accept(IDocumentItemVisitor visitor) throws IOException {
        visitor.Visit(this);
    }

    @Override
    public IMemento GetState() {
        return new DocumentTitleMemento(_title, this);
    }

    private String _title;
}
