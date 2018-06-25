package storage;

import java.io.IOException;

public interface IResource {
    String Name();

    String Path();

    void Delete() throws IOException;
}
