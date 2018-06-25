package fileSystem;

import environment.fileSystem.IFileOperations;
import java.io.*;

class FileOperations implements IFileOperations {
    @Override
    public void Copy(String from, String to) throws IOException {

        try (InputStream is = new FileInputStream(new File(from));
             OutputStream os = new FileOutputStream(new File(to)))
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
        final File f = new File(path);
        if (f.exists())
        {
            f.delete();
        }
    }
}
