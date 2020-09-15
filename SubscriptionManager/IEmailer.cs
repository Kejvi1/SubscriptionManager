using System;

namespace Detyra_2
{
    public interface IEmailer
    {
        bool sendEmail(string adresa, string mesazhi);
        bool shtonjoftimin(DateTime njoftimi, string adresa);
    }
}