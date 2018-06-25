package hsitory;

public interface IHistory {
    void AddCommand(ICommand command) throws Exception;

    void Undo();

    void Redo();
}
