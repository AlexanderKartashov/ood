package app;

import environment.ITextEncoder;
import static org.apache.commons.lang3.StringEscapeUtils.escapeHtml4;

class HtmlEncoder implements ITextEncoder {

    @Override
    public String Encode(String raw) {
        return escapeHtml4(raw);
    }
}
