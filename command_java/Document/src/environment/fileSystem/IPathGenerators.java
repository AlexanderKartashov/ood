package environment.fileSystem;

import java.io.IOException;

public interface IPathGenerators {
    String GetUniqueFileNameInDirectory(String folder) throws IOException;

    String GetUniqueTempFolderPath() throws IOException;

    String RelativeToAbsolute(String root, String name) throws IOException;

    String AbsoluteToRelative(String root, String path);

    String CurrentModulePath();

    String GetFileName(String path);

    String ReplaceExtension(String fileName, String newExtension);
}
