using MainService.AL.Mappers;
using MainService.DAL.Features.Courses.Models;

namespace MainService.AL.Features.Courses.DTO
{
    public class CourseDto : IMapper<Course>
    {
        public Guid LearningLanguageId { get; set; }
        public Guid BaseLanguageId { get; set; }
        public bool? Status { get; set; }
        
        public Course ToEntity()
        {
            return new Course
            {
                Id = Guid.NewGuid(),
                LearningLanguageId = LearningLanguageId,
                BaseLanguageId = BaseLanguageId,
                Status = Status ?? false
            };
        }
        
        public void MapTo(Course entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.LearningLanguageId = LearningLanguageId;
            entity.BaseLanguageId = BaseLanguageId;
            entity.Status = Status ?? entity.Status;
        }
    }
}