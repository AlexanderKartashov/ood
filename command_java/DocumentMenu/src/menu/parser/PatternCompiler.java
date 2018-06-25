package menu.parser;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

class PatternCompiler {

    public PatternCompiler(String name) {
        Start();
        Command(name);
    }

    public PatternCompiler Digit() {
        Whitespaces();
        _sb.append("(\\d+?)");
        return this;
    }

    public PatternCompiler Position(){
        Whitespaces();
        _sb.append("(\\d+?|end)");
        return this;
    }

    public PatternCompiler Everything() {
        Whitespaces();
        _sb.append("(.+)");
        return this;
    }

    public Matcher CreateMatcher(String match) {
        End();
        return Pattern.compile(_sb.toString(), Pattern.CASE_INSENSITIVE).matcher(match);
    }

    private void Whitespaces() {
        _sb.append("\\s+?");
    }

    private void Command(String name) {
        _sb.append(name);
    }

    private void Start() {
        _sb.append("^");
    }

    private void End() {
        _sb.append("$");
    }

    private StringBuilder _sb = new StringBuilder();
}
