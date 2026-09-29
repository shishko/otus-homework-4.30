namespace otus_homework_4._30._4.EventHandlers
{
    internal class FileFoundEventArgs(string file) : EventArgs
    {
        public string FileName { get; } = file ?? throw new ArgumentNullException(nameof(file));

        public bool Cancel { get; set; } = false;
    }
}
