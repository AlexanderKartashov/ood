package hsitory;

public interface ICommand {

    void Execute();

    void UnExecute();

    void Destroy() throws Exception;

    boolean IsExecuted();
}
