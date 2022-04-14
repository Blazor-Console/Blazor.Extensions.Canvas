using System.Threading.Tasks;

namespace Blazor.Extensions
{
    public interface IAsyncProperty<T>
    {
        ValueTask<T> GetAsync();
        ValueTask SetAsync(T value);
    }
}
