using Domain.Model;

namespace Mailer;

public interface IMailer
{
    public void SendRegistrationMail(User to);
    public void SendRequestAcceptedMail(Request request, string agreement, string recepit);
    public void SendPackagePickedUpMail(Request request);
    public void SendPackageDeveliredMail(Request request);
    public void SendDeliveryFailedMail(Request request, User courier, string reason);
}
