﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HedgeLinks.Services
{
    public interface IFunctional
    {
        Task InitAppData();
        Task CreateDefaultRoles();

        Task CreateDefaultSuperAdmin();
        Task CreateDefaultOperator();
        Task CreateDefaultJobseeker();
        Task CreateDefaultEmployer();
        

        Task SendEmailBySendGridAsync(string apiKey, 
            string fromEmail, 
            string fromFullName, 
            string subject, 
            string message, 
            string email);

        Task SendEmailByGmailAsync(string fromEmail,
            string fromFullName,
            string subject,
            string messageBody,
            string toEmail,
            string toFullName,
            string smtpUser,
            string smtpPassword,
            string smtpHost,
            int smtpPort,
            bool smtpSSL);

        Task<string> UploadFile(List<IFormFile> files, IHostingEnvironment env, string uploadFolder);

    }
}
