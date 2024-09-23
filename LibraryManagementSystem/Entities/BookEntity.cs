namespace LibraryManagementSystem.Entities
{
    public class BookEntity         //Kitap
    {
        
        public int Id { get; set; }
        public string Title { get; set; }               //Başlık
        public int AuthorId { get; set; }               //Yazar ıd
        public string Genre { get; set; }               //Kitap türü
        public DateTime PublishDate { get; set; }       //Yayın tarihi
        public string ISBN { get; set; }
        public int CopiesAvailable { get; set; }        //Mevcut kopya sayısı
        public bool IsDeleted { get; set; }             //silinme işleminin olup olmadığını barındıran property

    }
}
