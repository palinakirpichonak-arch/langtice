using MainService.DAL.Abstractions;
using MainService.DAL.Models.CoursesModel;
using MainService.DAL.Models.WordsModel;

namespace MainService.DAL.Models.TranslationsModel
{
    public class Translation : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid FromWordId { get; set; }
        public Word FromWord { get; set; } = null!;
        public Guid ToWordId { get; set; }
        public Word ToWord { get; set; } = null!;
        public Guid? CourseId { get; set; }
        public Course? Course { get; set; }
    }
}