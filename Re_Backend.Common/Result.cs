using Re_Backend.Common.enumscommon;

namespace Re_Backend.Common
{
    public class Result<T>
    {
        public ResultEnum Code { get; set; }
        public T? Data { get; set; }
    }
}
