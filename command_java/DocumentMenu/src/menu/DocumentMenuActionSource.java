package menu;

import java.io.BufferedReader;
import java.io.IOException;

public class DocumentMenuActionSource extends ActionSource {

    public DocumentMenuActionSource(IActionsParser actionsParser, BufferedReader reader) {
        super(actionsParser);
        _reader = reader;
    }

    @Override
    protected String GetActionDescription() throws IOException {
        return _reader.readLine();
    }

    @Override
    public boolean HasMoreActions() throws IOException {
        return _reader.ready();
    }

    private final BufferedReader _reader;
}
