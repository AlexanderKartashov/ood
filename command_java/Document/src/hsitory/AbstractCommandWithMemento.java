package hsitory;

public abstract class AbstractCommandWithMemento extends AbstractCommand {

    public AbstractCommandWithMemento(IMementoOriginator mementoOriginator) {
        _mementoOriginator = mementoOriginator;
    }

    @Override
    protected void UnExecuteImpl() {
        _memento.Restore();
    }

    @Override
    protected boolean BeforeExecute() {
        final boolean continueExecution = super.BeforeExecute();

        if (continueExecution) {
            if (_memento == null) {
                _memento = _mementoOriginator.GetState();
            }
        }
        return continueExecution;
    }

    private final IMementoOriginator _mementoOriginator;
    private IMemento _memento;
}
