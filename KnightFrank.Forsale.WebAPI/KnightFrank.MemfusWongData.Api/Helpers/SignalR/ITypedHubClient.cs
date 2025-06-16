using System.Threading.Tasks;

namespace KnightFrank.MemfusWongData.Api.Helpers.SignalR
{
    public interface ITypedHubClient
    {
        Task BroadcastMessage(string type, string payload);
    }
}
