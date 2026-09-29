namespace otus_homework_4._30.Extensions
{
    public static class EnumerableExtensions
    {
        public static T? GetMax<T>(this IEnumerable<T> collection, Func<T, float> convertToNumber) where T : class
        {
            ArgumentNullException.ThrowIfNull(collection);

            ArgumentNullException.ThrowIfNull(convertToNumber);

            T? maxElement = null;
            var hasElements = false;
            var maxValue = float.MinValue;

            foreach (var item in collection)
            {
                if (item == null) continue;

                var currentValue = convertToNumber(item);
                
                if (!hasElements || currentValue > maxValue)
                {
                    maxValue = currentValue;
                    maxElement = item;
                    hasElements = true;
                }
            }

            return maxElement;
        }
    }
}
