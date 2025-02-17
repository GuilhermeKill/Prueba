namespace Shared
{
    public class Functions
    {
        public static int SkipRows(int page, int pageSize) => (page - 1) * pageSize;
    }
}
