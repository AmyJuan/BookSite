namespace Book.Dao.Entity
{
    using System;
    public class UserEntity
    {
        public string ID { get; set; }
        public string Uid { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Alt { get; set; }
        public string Image { get; set; }
        public DateTime Create { get; set; }
    }
}
