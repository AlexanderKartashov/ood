package menu;

@FunctionalInterface
public interface IErrorHandler {
    void OnError(Throwable err);
}
