package document.documentItem.title;

import hsitory.IMementoOriginator;

public interface IMutableDocumentTitle extends IDocumentTitle, IMementoOriginator {
    void SetTitle(String title);
}
