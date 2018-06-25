package hsitory;

public abstract class AbstractCommand implements ICommand {

    @Override
    public final void Execute() {
        if (BeforeExecute())
        {
            ExecuteImpl();
            AfterExecute();
        }
    }

    @Override
    public final void UnExecute() {
        if (BeforeUnExecute())
        {
            UnExecuteImpl();
            AfterUnExecute();
        }
    }

    @Override
    public final void Destroy() throws Exception {

        if (BeforeDestroy())
        {
            DestroyImpl();
            AfterDestroy();
        }
    }

    @Override
    public final boolean IsExecuted() {
        return _ieExecuted;
    }

    protected boolean BeforeExecute(){
        if (_ieExecuted && _isAlive)
        {
            throw new IllegalStateException("attempt to execute already executed command");
        }
        return _isAlive;
    }
    protected void AfterExecute(){ _ieExecuted = true; }
    protected abstract void ExecuteImpl();

    protected boolean BeforeUnExecute(){
        if (!_ieExecuted && _isAlive)
        {
            throw new IllegalStateException("attempt to unexecute already unexecuted command");
        }
        return _isAlive;
    }
    protected void AfterUnExecute(){ _ieExecuted = false; }
    protected abstract void UnExecuteImpl();

    protected boolean BeforeDestroy() { return _isAlive; }
    protected void AfterDestroy(){ _isAlive = false; }
    protected abstract void DestroyImpl() throws Exception;

    private boolean _isAlive = true;
    private boolean _ieExecuted = false;
}
