package menu;

public interface IActionParser extends IActionInfo {
    IAction Parse(String action) throws IllegalArgumentException;
}
