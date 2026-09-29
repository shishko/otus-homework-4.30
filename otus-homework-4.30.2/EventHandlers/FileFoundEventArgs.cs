namespace otus_homework_4._30._2.EventHandlers
{
    internal class FileFoundEventArgs(FileInfo file) : EventArgs
    {
        public FileInfo File { get; } = file ?? throw new ArgumentNullException(nameof(file));
    }
}
