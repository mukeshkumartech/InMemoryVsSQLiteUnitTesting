using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InMemoryVsSQLiteDemo.Model
{
    public partial class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [ForeignKey("Id")]
        public int CategoryId { get; set; }
        public DateTime? CreatedDate { get; set; }        
        public virtual Category Category { get; set; }
    }
}
