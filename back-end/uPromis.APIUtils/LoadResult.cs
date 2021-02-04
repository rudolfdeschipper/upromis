namespace uPromis.APIUtils.APIMessaging
{
    public class LoadResult<T>
    {
        public T[] Data { get; set; }
        public double Pages { get; set; }
        public string Message { get; set; }
    }
}
