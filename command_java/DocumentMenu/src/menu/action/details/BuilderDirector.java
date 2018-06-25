package menu.action.details;

import document.IDocument;

class BuilderDirector {

    public void Build(IDocument document, IDocumentStateBuilder builder){
        builder.BeforeBuild();
        builder.BuildTitle(document.Title());
        document.Items().GetIterator().forEachRemaining(builder::BuildDocumentItem);
        builder.AfterBuild();
    }
}
