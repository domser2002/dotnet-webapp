using SendGrid.Helpers.Mail;
using SendGrid;
using Domain.Model;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Mailer;

public class Mailer : IMailer
{
    public void SendDeliveryFailedMail(Request request, User courier, string reason)
    {
        var dynamicTemplateData = new
        {
            FullName = request.Owner.PersonalData,
            RequestID = request.Id,
            CourierFullname = courier.FullName,
            Company = courier.Company,
            Date = request.DeliveryDate,
            Reason = reason
        };
        Send(request.Owner, "d-cc35ccc475964d2e88aef70b482694b4", dynamicTemplateData, null).Wait();
    }

    public void SendPackageDeveliredMail(Request request)
    {
        var dynamicTemplateData = new
        {
            FullName = request.Owner.PersonalData,
            RequestID = request.Id
        };
        Send(request.Owner, "d-8f3249680fb647b889ccb89a0dc47a96", dynamicTemplateData, null).Wait();
    }

    public void SendPackagePickedUpMail(Request request)
    {
        var dynamicTemplateData = new
        {
            FullName = request.Owner.PersonalData,
            RequestID = request.Id
        };
        Send(request.Owner, "d-38c86f8883b24899a02111eb7de14515", dynamicTemplateData, null).Wait();
    }

    public void SendRegistrationMail(User to)
    {
        var dynamicTemplateData = new { FullName = to.FullName };
        Send(new(to), "d-2167d74376524c39bfd641be531abd92", dynamicTemplateData, null).Wait();
    }

    public void SendRequestAcceptedMail(Request request, string agreement, string recepit)
    {
        var dynamicTemplateData = new
        {
            FullName = request.Owner.PersonalData,
            RequestID = request.Id
        };
        (string, string)[] attachments = new (string, string)[] { ("agreement", agreement), ("receipt", recepit) };
        Send(request.Owner, "d-0be105ff09724c28abb983706629c751", dynamicTemplateData, attachments).Wait();
    }

    private async Task Send(ContactInformation to, string template, object templateData, (string, string)[]? attachments)
    {
        var client = new SendGridClient("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");
        var msg = new SendGridMessage();
        msg.SetFrom(new EmailAddress("dotnetwebapp@gmail.com", "Courier Hub"));
        msg.AddTo(new EmailAddress(to.Email, to.PersonalData));
        msg.SetTemplateId(template);
        msg.SetTemplateData(templateData);
        if (attachments is not null)
        {
            foreach ((string, string) attachment in attachments)
            {
                BlobServiceClient blobServiceClient = new("DefaultEndpointsProtocol=https;AccountName=dotnetwebapp;" +
                    "AccountKey=l8YwfKHD9jI0GRpxzKfJrhbJHpiavg5hQvN0MXhRmAB0BLOoqZY6+dG+xMApvt0w2YNXvTtxdzo++ASt0zLpNg==;EndpointSuffix=core.windows.net");
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("container");
                BlobClient blobClient = containerClient.GetBlobClient(attachment.Item2);
                if (blobClient.ExistsAsync().Result)
                {
                    using var ms = new MemoryStream();
                    blobClient.DownloadTo(ms);
                    var file = Convert.ToBase64String(ms.ToArray());
                    msg.AddAttachment($"{attachment.Item1}.pdf", file);
                }
            }
        }
        await client.SendEmailAsync(msg);
    }
}
