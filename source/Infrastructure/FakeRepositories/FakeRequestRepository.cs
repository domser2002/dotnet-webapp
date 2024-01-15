using Domain.Abstractions;
using Domain.Model;

namespace Infrastructure.FakeRepositories;

public class FakeRequestRepository : IRequestRepository
{
    private IDictionary<int, Request> requests;
    public FakeRequestRepository()
    {
        List<User> users = new FakeUserRepository().GetAll();
        Address address1 = new() { Street = "Prosta", City = "Warsaw", FlatNumber = "1", PostalCode = "13-337", StreetNumber = "2" };
        Address address2 = new() { Street = "Kręta", City = "Cracow", FlatNumber = "2", PostalCode = "31-733", StreetNumber = "1" };
        DateTime date0 = new(2024, 1, 1, 7, 0, 0);
        DateTime date1 = new(2025, 3, 1, 7, 0, 0);
        DateTime date2 = new(2026, 3, 1, 7, 0, 0);
        Package package = new() { Weight = 1, Height = 1, Length = 1, Width = 1 };
        List<Request> req = new()
        {
            new() { Id = 1, CancelDate = date0, PickupDate = date1, DeliveryDate = date2, CompanyName="Najlepsza firma", SourceAddress = new(address1), DestinationAddress = new(address2),
            Package = new(package), Price=100, Status=RequestStatus.Idle, Owner = new(users[0]) },
            new() { Id = 2, CancelDate = date0, PickupDate = date1, DeliveryDate = date2, CompanyName="Najgorsza firma", SourceAddress = new(address1), DestinationAddress = new(address2),
            Package = new(package), Price=200, Status=RequestStatus.Cancelled, Owner = new(users[1]) },
            new() { Id = 3, CancelDate = date0, PickupDate = date1, DeliveryDate = date2, CompanyName="Taka sobie firma", SourceAddress = new(address1), DestinationAddress = new(address2),
            Package = new(package), Price=300, Status=RequestStatus.Received, Owner = new(users[2]) }
        };
        requests = req.ToDictionary(p => p.Id);
    }

    public int Add(Request request)
    {
        int index = requests.Count + 1;
        requests.Add(index, request);
        return index;
    }

    public void ChangeStatus(int id, RequestStatus status)
    {
        requests[id].Status = status;
    }

    public void Delete(int id)
    {
        requests.Remove(id);
    }

    public List<Request> GetAll()
    {
        return requests.Values.ToList();
    }

    public List<Request> GetByCompany(string company)
    {
        return requests.Values.Where(request => request.CompanyName == company).ToList();
    }

    public Request GetById(int id)
    {
        List<Request> req = requests.Values.Where(request => request.Id == id).ToList();
        if (req.Count == 1) return req[0];
        return null!;
    }

    public List<Request> GetByOwner(string ownerId)
    {
        FakeUserRepository userRepo = new();
        List<User> users = userRepo.GetAll().Where(user => user.Auth0Id == ownerId).ToList();
        if (users.Count == 1) return users[0].Requests;
        return new();
    }

    public void Update(Request request)
    {
        throw new NotImplementedException();
    }
}
