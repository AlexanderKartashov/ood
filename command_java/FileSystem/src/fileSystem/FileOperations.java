package fileSystem;

import environment.fileSystem.IFile;
import fileSystem.File;
import environment.fileSystem.IFileOperations;
import java.io.*;

class FileOperations implements IFileOperations {
    @Override
    public void Copy(String from, String to) throws IOException {

        try (InputStream is = new FileInputStream(new java.io.File(from));
             OutputStream os = new FileOutputStream(new java.io.File(to)))
        {
            byte[] buffer = new byte[1024];
            int length;
            while ((length = is.read(buffer)) > 0) {
                os.write(buffer, 0, length);
            }
        }
        catch(Throwable err)
        {
            throw err;
        }
    }

    @Override
    public void Delete(String path) {
        final java.io.File f = new java.io.File(path);
        if (f.exists())
        {
            f.delete();
        }
    }

    @Override
    public IFile CreateFile(String path) throws FileNotFoundException {
        return new fileSystem.File(path);
    }
}
