using HedgeLinks.Data;
using HedgeLinks.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HedgeLinks.Services
{
    public class Functional : IFunctional
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRoles _roles;
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly SuperAdminDefaultOptions _superAdminDefaultOptions;
        private readonly EmployerDefaultOptions _employerDefaultOptionss;
        private readonly JobseekerDefaultOptions _jobseekerDefaultOptions;
        private readonly OperatorDefaultOptions _operatorDefaultOptions;


        public Functional(UserManager<ApplicationUser> userManager, IHostingEnvironment environment,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,
            SignInManager<ApplicationUser> signInManager,
            IRoles roles,
            IOptions<SuperAdminDefaultOptions> superAdminDefaultOptions,
            IOptions<EmployerDefaultOptions> employerDefaultOptions,
            IOptions<OperatorDefaultOptions> operatorDefaultOptions,
            IOptions<JobseekerDefaultOptions> jobseekerDefaultOptions)
        {
            _userManager = userManager;
            _hostingEnvironment = environment;
            _roleManager = roleManager;
            _context = context;
            _signInManager = signInManager;
            _roles = roles;
            _superAdminDefaultOptions = superAdminDefaultOptions.Value;
            _employerDefaultOptionss = employerDefaultOptions.Value;
            _operatorDefaultOptions = operatorDefaultOptions.Value;
            _jobseekerDefaultOptions = jobseekerDefaultOptions.Value;
        }


        public async Task InitAppData()
        {
            try
            {
                var countryServerPath = _hostingEnvironment.WebRootPath + "\\Countries\\countries.json";
                StreamReader streamReaderCountry = new StreamReader(countryServerPath); //init app with super admin user
                string data = streamReaderCountry.ReadToEnd();

                if (data != null)
                {
                    var json = JsonConvert.DeserializeObject<CountryRoot>(data);
                    foreach (var item in json.countries)
                    {
                        await _context.Country.AddAsync(new Country
                        {
                            name = item.name,
                            sortname = item.sortname,
                            phoneCode = item.phoneCode
                        });
                        await _context.SaveChangesAsync();
                    }
                }

                var stateServerPath = _hostingEnvironment.WebRootPath + "\\Countries\\states.json";
                StreamReader streamReaderState = new StreamReader(stateServerPath); //init app with super admin user
                string state = streamReaderState.ReadToEnd();

                if (state != null)
                {
                    var json2 = JsonConvert.DeserializeObject<StateRoot>(state);

                    try
                    {
                        foreach (var item in json2.states)
                        {
                            await _context.State.AddAsync(new State
                            {
                                name = item.name,
                                country_id = Int32.Parse(item.country_id)
                            });
                            await _context.SaveChangesAsync();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

                var cityServerPath = _hostingEnvironment.WebRootPath + "\\Countries\\cities.json";
                StreamReader streamReaderCity = new StreamReader(cityServerPath); //init app with super admin user
                string city = streamReaderCity.ReadToEnd();

                if (city != null)
                {
                    var json3 = JsonConvert.DeserializeObject<CityRoot>(city);

                    foreach (var item in json3.Cities)
                    {
                        try
                        {
                            await _context.City.AddAsync(new City
                            {
                                name = item.name,

                                state_id = Int32.Parse(item.state_id)
                            });
                            await _context.SaveChangesAsync();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                    }
                }

//                countries.Add(data); 

                await _context.InvoiceType.AddAsync(new InvoiceType {InvoiceTypeName = "Default"});
                await _context.SaveChangesAsync();

                await _context.PaymentType.AddAsync(new PaymentType {PaymentTypeName = "Default"});
                await _context.SaveChangesAsync();

                await _context.PurchaseType.AddAsync(new PurchaseType {PurchaseTypeName = "Default"});
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
            }
        }


        public async Task SendEmailBySendGridAsync(string apiKey,
            string fromEmail,
            string fromFullName,
            string subject,
            string message,
            string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(fromEmail, fromFullName),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email, email));
            await client.SendEmailAsync(msg);
        }

        public async Task SendEmailByGmailAsync(string fromEmail,
            string fromFullName,
            string subject,
            string messageBody,
            string toEmail,
            string toFullName,
            string smtpUser,
            string smtpPassword,
            string smtpHost,
            int smtpPort,
            bool smtpSSL)
        {
            var body = messageBody;
            var message = new MailMessage();
            message.To.Add(new MailAddress(toEmail, toFullName));
            message.From = new MailAddress(fromEmail, fromFullName);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = smtpUser,
                    Password = smtpPassword
                };
                smtp.Credentials = credential;
                smtp.Host = smtpHost;
                smtp.Port = smtpPort;
                smtp.EnableSsl = smtpSSL;
                await smtp.SendMailAsync(message);
            }
        }

        public async Task CreateDefaultRoles()
        {
            try
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("Operator"));
                await _roleManager.CreateAsync(new IdentityRole("JobSeeker"));
                await _roleManager.CreateAsync(new IdentityRole("Employer"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task CreateDefaultSuperAdmin()
        {
            try
            {
                await _roles.GenerateRolesFromPagesAsync();

                ApplicationUser superAdmin = new ApplicationUser();
                superAdmin.Email = _superAdminDefaultOptions.Email;
                superAdmin.UserName = superAdmin.Email;
                superAdmin.EmailConfirmed = true;

                var result = await _userManager.CreateAsync(superAdmin,"123456");

                if (result.Succeeded)
                {
                    //add to user profile
                    UserProfile profile = new UserProfile();
                    profile.FirstName = "Super";
                    profile.LastName = "Admin";
                    profile.Email = "admin@hedgelinks.com";
                    profile.ApplicationUserId = superAdmin.Id;
                    await _context.UserProfile.AddAsync(profile);
                    await _context.SaveChangesAsync();
                    await _userManager.AddToRoleAsync(superAdmin, "Admin");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CreateDefaultOperator()
        {
            try
            {
                await _roles.GenerateRolesFromPagesAsync();

                ApplicationUser operatorUser = new ApplicationUser();
                operatorUser.Email = _superAdminDefaultOptions.Email;
                operatorUser.UserName = operatorUser.Email;
                operatorUser.EmailConfirmed = true;

                var result = await _userManager.CreateAsync(operatorUser,"123456");

                if (result.Succeeded)
                {
                    //add to user profile
                    UserProfile profile = new UserProfile();
                    profile.FirstName = "operator";
                    profile.LastName = "operator";
                    profile.Email = "operator@hedgelinks.com";
                    profile.ApplicationUserId = operatorUser.Id;
                    await _context.UserProfile.AddAsync(profile);
                    await _context.SaveChangesAsync();
                    await _userManager.AddToRoleAsync(operatorUser, "Operator");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CreateDefaultJobseeker()
        {
            try
            {
                await _roles.GenerateRolesFromPagesAsync();

                ApplicationUser jobseekerUser = new ApplicationUser();
                jobseekerUser.Email = _superAdminDefaultOptions.Email;
                jobseekerUser.UserName = jobseekerUser.Email;
                jobseekerUser.EmailConfirmed = true;

                var result = await _userManager.CreateAsync(jobseekerUser, "123456");

                if (result.Succeeded)
                {
                    //add to user profile
                    UserProfile profile = new UserProfile();
                    profile.FirstName = "Jobseeker";
                    profile.LastName = "Jobseeker";
                    profile.Email = "jobseeker@hedgelinks.com";
                    profile.ApplicationUserId = jobseekerUser.Id;
                    await _context.UserProfile.AddAsync(profile);
                    await _context.SaveChangesAsync();
                    await _userManager.AddToRoleAsync(jobseekerUser, "JobSeeker");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CreateDefaultEmployer()
        {
            try
            {
                await _roles.GenerateRolesFromPagesAsync();
                ApplicationUser employerUser = new ApplicationUser();
                employerUser.Email = _superAdminDefaultOptions.Email;
                employerUser.UserName = employerUser.Email;
                employerUser.EmailConfirmed = true;
                var result = await _userManager.CreateAsync(employerUser, "123456");
                if (result.Succeeded)
                {
                    //add to user profile
                    UserProfile profile = new UserProfile();
                    profile.FirstName = "employer";
                    profile.LastName = "employer";
                    profile.Email = "employer@hedgelinks.com";
                    profile.ApplicationUserId = employerUser.Id;
                    await _context.UserProfile.AddAsync(profile);
                    await _context.SaveChangesAsync();
                    await _userManager.AddToRoleAsync(employerUser, "Employer");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<string> UploadFile(List<IFormFile> files, IHostingEnvironment env, string uploadFolder)
        {
            var result = "";

            var webRoot = env.WebRootPath;
            var uploads = System.IO.Path.Combine(webRoot, uploadFolder);
            var extension = "";
            var filePath = "";
            var fileName = "";


            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    extension = System.IO.Path.GetExtension(formFile.FileName);
                    fileName = Guid.NewGuid().ToString() + extension;
                    filePath = System.IO.Path.Combine(uploads, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    result = fileName;
                }
            }

            return result;
        }
    }
}