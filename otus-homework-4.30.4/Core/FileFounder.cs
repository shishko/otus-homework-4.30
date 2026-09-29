using otus_homework_4._30._4.EventHandlers;

namespace otus_homework_4._30._4.Core
{
    internal class FileFounder
    {
        public event EventHandler<FileFoundEventArgs>? FileFound;

        /// <summary>
        /// Запускает рекурсивный обход каталога.
        /// </summary>
        /// <param name="path">Путь к начальной директории</param>
        public void ScanDirectory(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Путь не может быть пустым.", nameof(path));

            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException($"Директория не найдена: {path}");

            var directoryInfo = new DirectoryInfo(path);
            Scan(directoryInfo);
        }

        /// Внутренний рекурсивный метод обхода
        private bool Scan(DirectoryInfo directory)
        {
            try
            {
                /// файлы в текущей директории и вызываем событие
                foreach (FileInfo file in directory.GetFiles())
                {
                    var args = new FileFoundEventArgs(file.FullName);
                    OnFileFound(args);

                    if (args.Cancel)
                    {
                        return true;
                    }
                }

                ///переходим в поддиректории
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

        /// безопасный метод вызова события
        protected virtual void OnFileFound(FileFoundEventArgs e) => FileFound?.Invoke(this, e);
    }

}
