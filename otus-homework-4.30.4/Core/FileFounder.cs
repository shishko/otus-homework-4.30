using otus_homework_4._30._4.EventHandlers;

namespace otus_homework_4._30._4.Core
{
    internal class FileFounder
    {
        public static int _foundFiles = 0;
        public static readonly int _limit = 3;

        public event EventHandler<FileFoundEventArgs>? FileFound;

        /// <summary>
        /// Запускает рекурсивный обход каталога.
        /// </summary>
        internal void ScanDirectory(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Путь не может быть пустым.", nameof(path));

            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException($"Директория не найдена: {path}");

            _foundFiles = 0;

            var directoryInfo = new DirectoryInfo(path);
            Scan(directoryInfo);
        }

        /// <summary>
        /// Внутренний метод обхода
        /// </summary>
        private bool Scan(DirectoryInfo directory)
        {
            try
            {
                foreach (FileInfo file in directory.GetFiles())
                {
                    _foundFiles++;

                    var args = new FileFoundEventArgs(file.FullName);
                    OnFileFound(args);

                    /// <summary>
                    /// Проверяем лимиты или исключение
                    /// </summary>
                    if (args.Cancel || (_limit > 0 && _foundFiles >= _limit))
                    {
                        return true;
                    }
                }

                /// <summary>
                /// Переходим в поддиректории
                /// </summary>
                foreach (DirectoryInfo subDir in directory.GetDirectories())
                {
                    if (Scan(subDir))
                    {
                        return true;
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                /// игнорируем папки, если нет прав доступа
            }
            catch (DirectoryNotFoundException)
            {
                /// игнорируем папки, если были удалены во время сканирования
            }

            return false;
        }

        protected virtual void OnFileFound(FileFoundEventArgs e) => FileFound?.Invoke(this, e);
    }

}
