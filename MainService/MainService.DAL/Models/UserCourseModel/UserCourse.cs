using System.ComponentModel.DataAnnotations.Schema;
using MainService.DAL.Abstractions;
using MainService.DAL.Models.CoursesModel;
using Microsoft.EntityFrameworkCore;

namespace MainService.DAL.Models.UserCourseModel
{
    public class UserCourse : IEntity<UserCourseKey>
    {
        [NotMapped]
        public UserCourseKey Id
        {
            get => new(UserId, CourseId);
            set
            {
                UserId = value.UserId;
                CourseId = value.CourseId;
            }
        }

        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }

    [Owned]
    public class UserCourseKey
    {
        public UserCourseKey(Guid userId, Guid courseId)
        {
            UserId = userId;
            CourseId = courseId;
        }

        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
    }
}