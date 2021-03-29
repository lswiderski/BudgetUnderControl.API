using BudgetUnderControl.Common.Contracts.Email;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.CommonInfrastructure.Interfaces.Email
{
    public interface IEmailBuilder : IFrom, ITo, IAfterTo, IContent, IAfterCc
    {
    }

    public interface IFrom
    {
        ITo SentBy(EmailParticipant from);
    }

    public interface ITo
    {
        IAfterTo To(EmailParticipant to);
    }

    public interface ICc
    {
        IAfterCc WithCopySentTo(EmailParticipant cc);
    }

    public interface ITitle
    {
        IContent Entitled(string subject);
    }

    public interface IAfterTo : ITitle, ICc
    {
        IAfterTo And(EmailParticipant to);
    }

    public interface IAfterCc : ITitle
    {
        IAfterCc And(EmailParticipant cc);
    }

    public interface IContent
    {
        EmailMessage WithHtmlContent(string body);
        EmailMessage WithPlainTextContent(string body);
    }
}
