package hsitory;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import sun.plugin.dom.exception.InvalidStateException;

import static org.hamcrest.CoreMatchers.is;
import static org.hamcrest.MatcherAssert.assertThat;
import static org.junit.jupiter.api.Assertions.*;
import static testUtils.Utils.assertNoThrows;

class AbstractCommandTest {

    private DerivedCommand _command;

    @BeforeEach
    void setUp() {
        _command = new DerivedCommand();
    }

    @Test
    void execute() {
        assertNoThrows(_command::Execute);
        assertThat(_command.executed, is(true));
    }

    @Test
    void executeDestroyedCommand() {
        assertNoThrows(_command::Destroy);
        assertNoThrows(_command::Execute);
        assertThat(_command.executed, is(false));
    }

    @Test
    void doubleExecute() {
        assertNoThrows(_command::Execute);
        assertThrows(IllegalStateException.class, _command::Execute);
    }

    @Test
    void doubleExecuteDestroyedCommand() {
        assertNoThrows(_command::Execute);
        assertNoThrows(_command::Destroy);
        assertNoThrows(_command::Execute);
    }

    @Test
    void executeUnExecuted() {
        assertNoThrows(_command::Execute);
        assertThat(_command.executed, is(true));

        assertNoThrows(_command::UnExecute);
        assertThat(_command.unexecuted, is(true));

        _command.executed = false;
        assertNoThrows(_command::Execute);
        assertThat(_command.executed, is(true));
    }

    @Test
    void unExecute() {
        assertNoThrows(_command::Execute);
        assertNoThrows(_command::UnExecute);
        assertThat(_command.unexecuted, is(true));
    }

    @Test
    void unExecuteNotExecuted() {
        assertThrows(IllegalStateException.class, _command::UnExecute);
        assertThat(_command.unexecuted, is(false));
    }

    @Test
    void unExecuteDestroyedCommand() {
        assertNoThrows(_command::Destroy);
        assertNoThrows(_command::UnExecute);
        assertThat(_command.unexecuted, is(false));
    }

    @Test
    void destroy() {
        assertNoThrows(_command::Destroy);
        assertThat(_command.destroyed, is(true));
    }

    @Test
    void isExecuted() {
        assertThat(_command.IsExecuted(), is(false));
        assertNoThrows(_command::Execute);
        assertThat(_command.IsExecuted(), is(true));
        assertNoThrows(_command::UnExecute);
        assertThat(_command.IsExecuted(), is(false));
    }

    class DerivedCommand extends AbstractCommand{

        public boolean executed = false;
        public boolean unexecuted = false;
        public boolean destroyed = false;

        @Override
        protected void ExecuteImpl() {
            executed = true;
        }

        @Override
        protected void UnExecuteImpl() {
            unexecuted = true;
        }

        @Override
        protected void DestroyImpl() throws Exception {
            destroyed = true;
        }
    }
}