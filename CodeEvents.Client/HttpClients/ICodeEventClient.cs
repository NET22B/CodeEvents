using CodeEvents.Common.Dto;

namespace CodeEvents.Client.HttpClients
{
    public interface ICodeEventClient
    {
        Task<CodeEventDto?> GetCodeEvent(string name, CancellationToken token);
        Task<IEnumerable<CodeEventDto>?> GetCodeEvents(CancellationToken token);
        Task<LectureDto?> GetLecture(string name, int id, CancellationToken token);
    }
}