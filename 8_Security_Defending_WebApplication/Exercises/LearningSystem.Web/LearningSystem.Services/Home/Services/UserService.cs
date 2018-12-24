
namespace LearningSystem.Services.Home.Services
{
    using AutoMapper.QueryableExtensions;
    using iTextSharp.text;
    using iTextSharp.text.html.simpleparser;
    using iTextSharp.text.pdf;
    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Home.Interfaces;
    using LearningSystem.Services.Home.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly LearningSystemDbContext db;

        public UserService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<UserProfileServiceModel> ProfileAsync(string id)
        {
            return await db.Users
                .Where(u => u.Id == id)
                .ProjectTo<UserProfileServiceModel>(new { userId = id})
                .FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<UserListingServiceModel>> FindUsersAsync(string searchText)
        {
            searchText = searchText ?? string.Empty;

            return await this.db.Users
                .OrderBy(u => u.Name)
                .Where(u => u.Name.ToLower().Contains(searchText.ToLower()))
                .ProjectTo<UserListingServiceModel>()
                .ToListAsync();
        }
        //---------------------------------html -> pdf-------------------------------------------
        public async Task<byte[]> GetPdfCertificate(int courseId, string studentId)
        {
            var studentInCourse = await this.db.FindAsync<StudentCourse>(courseId, studentId);

            if (studentInCourse == null)
            {
                return null;
            }

            var sertificateInfo = await this.db.Courses
                .Where(c => c.Id == courseId)
                .Select(c => new
                {
                    CourseName = c.Name,
                    CourseStartDate = c.StartDate.ToShortDateString(),
                    CourseEndDate = c.EndDate.ToShortDateString(),
                    StudentName = c.Students.Where(s => s.StudentId == studentId).Select(s => s.Student.Name).FirstOrDefault(),
                    StudentGrade = c.Students.Where(s => s.StudentId == studentId && s.CourseId == courseId)
                        .Select(s => s.Grade).FirstOrDefault(),
                    TrainerName = c.Trainer.Name,
                })
                .FirstOrDefaultAsync();
            //-------------------
            var pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            var htmlparser = new HtmlWorker(pdfDoc);

            using (var memoryStream = new MemoryStream())
            {
                var writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();

                using (var stringReader = new StringReader(string.Format(ServiceConstants.PdfCertificate, sertificateInfo.CourseName,
                   sertificateInfo.CourseStartDate,
                   sertificateInfo.CourseEndDate,
                   sertificateInfo.StudentName,
                   sertificateInfo.StudentGrade,
                   sertificateInfo.TrainerName,
                   DateTime.Now.ToShortDateString())))
                {
                    htmlparser.Parse(stringReader);
                }

                pdfDoc.Close();

                byte[] bytes = memoryStream.ToArray();

                return bytes;
            }

            //-------------------------------
        }
    }
}
