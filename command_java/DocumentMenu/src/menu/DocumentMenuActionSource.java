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
        return _command;
    }

    @Override
    public boolean HasMoreActions() throws IOException {
        _command = _reader.readLine();
        return !_command.isEmpty();
    }

    private String _command;
    private final BufferedReader _reader;
}
