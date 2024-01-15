using Domain.Model;

namespace Domain.Abstractions;

public interface IRequestRepository
{
    List<Request> GetAll();
    int Add(Request request);
    List<Request> GetByOwner(string ownerId);
    List<Request> GetByCompany(string company);
    void Delete(int id);
    void ChangeStatus(int id, RequestStatus status);
    Request GetById(int id);
    public void Update(Request request);
}
