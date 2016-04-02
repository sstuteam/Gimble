namespace GridMoment.UI.WebSite.Models
{
    public class PhotoViewModel
    {
        /// <summary>
        /// Основная картеначка
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// Тип картинки
        /// </summary>
        public string MimeType { get; set; }
    }
}