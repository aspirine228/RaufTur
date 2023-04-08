using System.Threading.Tasks;

namespace IMarketingGameResult
{
    public interface IMarketingGameResult
    {

        Task Log(string text);

        Task Prize(string text);

        Task SpinResult(string result);

    }
}