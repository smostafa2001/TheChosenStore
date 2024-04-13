namespace Common.Application.Sms;

public interface ISmsService
{
    void Send(string number, string message);
}