namespace MainService.AL.Translations.DTO;

public class CreateTranslationDTO
{
        public Guid FromWordId { get; set; }
        public Guid ToWordId { get; set; }
        public Guid? CourseId { get; set; }
}