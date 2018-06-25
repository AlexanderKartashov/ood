package testUtils;

import org.junit.jupiter.api.function.Executable;
import static org.junit.jupiter.api.Assertions.assertFalse;
import static org.junit.jupiter.api.Assertions.fail;

public class Utils {
    public static void assertNoThrows(Executable action) {
        try
        {
            action.execute();
        }
        catch (Throwable ex)
        {
            fail(ex.getMessage());
        }
    }
}
